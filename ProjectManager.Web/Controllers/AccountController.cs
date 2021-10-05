using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Domain;
using ProjectManager.Web.Models;
using ProjectManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public AccountController(SignInManager<ApplicationUser> signInManager,
                                 UserManager<ApplicationUser> userManager,
                                 IEmailSender emailSender)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login(string ReturnUrl)
        {
            var viewModel = new AccountLoginViewModel();
            viewModel.ReturnUrl = ReturnUrl ?? Url.Content("~/");
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(viewModel.Input.UserName);
                if (user != null) {
                    if (await userManager.CheckPasswordAsync(user, viewModel.Input.Password))
                    {
                        if(! await userManager.IsEmailConfirmedAsync(user))
                        {
                            ViewBag.ErrorTitle = "Please Confirm your Email";
                            return View("Error");
                        }
                            
                    }
                }
                var result = await signInManager.PasswordSignInAsync(viewModel.Input.UserName, viewModel.Input.Password,
                                                                     isPersistent: viewModel.Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return LocalRedirect(viewModel.ReturnUrl);
                }


                ModelState.AddModelError(string.Empty, "Invalid login attempt");

            }

            return View(viewModel);
        }
        public IActionResult Register(string ReturnUrl)
        {
            var viewModel = new AccountRegisterationViewModel();
            viewModel.ReturnUrl = ReturnUrl ?? Url.Content("~/");
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = viewModel.Input.UserName, Email = viewModel.Input.Email };
                var result = await userManager.CreateAsync(user, viewModel.Input.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    try
                    {
                        await emailSender.SendEmailAsync(user.Email, "Email Confirmation Required", confirmationLink);
                    }catch(Exception e)
                    {
                        //probably network is down.. redirect to error page
                        ViewBag.ErrorTitle = "Couldn't send confirmation email";
                        return View("Error");
                    }
                    ViewBag.ErrorTitle = "Please Confirm your Email";
                    return View("Error");
                    //await signInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(viewModel.ReturnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"The user Id {userId} is invalid";
                return View("NotFound");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse([FromQuery(Name ="Input.Email")] string email)
        { 
            var user = await userManager.FindByEmailAsync(email);
            return user == null ? Json(true) : Json($"Email {email} is already in use");
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsUserNameInUse([FromQuery(Name = "Input.UserName")] string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            return user == null ? Json(true) : Json($"User Name {userName} is already in use");
        }

        public async System.Threading.Tasks.Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await userManager.DeleteAsync(user);       
            }
            return RedirectToAction("Index","Member");
        }

        public async System.Threading.Tasks.Task<IActionResult> PromoteToAdmin([FromQuery] string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                if (key == "QWERTY-12")
                {
                    var userName = User.Identity.Name;
                    var user = await userManager.FindByNameAsync(userName);
                    await userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Logout", "Account");
                }
            }
            ViewBag.ErrorTitle = "Wrong Key";
            return View("Error");
        }
    }
}
