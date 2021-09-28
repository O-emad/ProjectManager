using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class MemberCreateViewModel
    {

        [Required]
        [StringLength(50,MinimumLength =2,
            ErrorMessage = "Name must be within minimum length 2 and maximum length 50")]
        [Display(Name ="Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }


        public MemberCreateViewModel()
        {

        }
    }
}
