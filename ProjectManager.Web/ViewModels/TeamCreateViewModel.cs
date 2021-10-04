using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class TeamCreateViewModel
    {

        [Required]
        [Display(Name="Name")]
        [StringLength(50,MinimumLength = 2,
            ErrorMessage = "Team name must be within a minimum length 2 and maximum length 50")]
        public string Name { get; set; }

        public List<Guid> SelectedMembers { get; set; }
        public MultiSelectList TeamMembers { get; set; }

        public TeamCreateViewModel()
        {

        }
        public TeamCreateViewModel(IEnumerable<Member> members)
        {
            TeamMembers = new MultiSelectList(members, "Id", "UserName");
        }
    }
}
