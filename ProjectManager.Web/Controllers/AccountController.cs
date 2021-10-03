using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,
                                 UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
                var user = new IdentityUser { UserName = viewModel.Input.Email, Email = viewModel.Input.Email };
                var result = await userManager.CreateAsync(user, viewModel.Input.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(viewModel.ReturnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse([FromQuery(Name ="Input.Email")] string email)
        { 
            var user = await userManager.FindByEmailAsync(email);
            return user == null ? Json(true) : Json($"Email {email} is already in use");
        }
    }
}
