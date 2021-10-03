using Microsoft.AspNetCore.Mvc;
using ProjectManager.Web.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class AccountRegisterationViewModel
    {
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }


        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            //[ValidEmailDomain(allowedDomain:"offbeat.com",
            //    ErrorMessage = "email domain must be offbeat.com")]
            [Remote(action:"IsEmailInUse",controller:"Account")]
            
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }

    
}
