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
    public class ProjectEditViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        public Guid Id { get; set; }

        public List<Guid> SelectedTeams { get; set; }
        public MultiSelectList Teams { get; set; }

        public ProjectEditViewModel()
        {

        }

        public ProjectEditViewModel(ProjectModel projectModel, IEnumerable<TeamModel> teams)
        {
            Id = projectModel.Id;
            Name = projectModel.Name;
            SelectedTeams = projectModel.Teams.Select(m => m.Id).ToList();
            var orderedlist = new MultiSelectList(teams, "Id", "Name", SelectedTeams)
                .OrderBy(i => !i.Selected)
                .Select(i => new { Id = i.Value, Name = i.Text }).ToList();
            Teams = new MultiSelectList(orderedlist, "Id", "Name", SelectedTeams);
        }
    }
}
