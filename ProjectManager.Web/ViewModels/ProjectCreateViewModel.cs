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
    public class ProjectCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Team name must be within a minimum length 2 and maximum length 50")]
        public string Name { get; set; }

        public List<Guid> SelectedTeams { get; set; }
        public MultiSelectList Teams { get; set; }

        public ProjectCreateViewModel(List<TeamModel> teams)
        {
            Teams = new MultiSelectList(teams, "Id", "Name");
        }
        public ProjectCreateViewModel()
        {
            
        }

    }
}
