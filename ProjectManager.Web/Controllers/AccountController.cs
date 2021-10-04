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
                var user = await userManager.FindByEmailAsync(viewModel.Input.Email);
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
                var result = await signInManager.PasswordSignInAsync(viewModel.Input.Email, viewModel.Input.Password,
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
                var user = new ApplicationUser { UserName = viewModel.Input.Email, Email = viewModel.Input.Email };
                var result = await userManager.CreateAsync(user, viewModel.Input.Password);

                if (result.Succeeded)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    await emailSender.SendEmailAsync(user.Email, "Email Confirmation Required", confirmationLink);

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
    }
}
