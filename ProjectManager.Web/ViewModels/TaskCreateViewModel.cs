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
    public class TaskCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Team name must be within a minimum length 2 and maximum length 50")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Description")]
        [StringLength(500, ErrorMessage = "Keep description in 500 charachter limit")]
        public string Description { get; set; }

        [Display(Name="Assignee")]
        public Guid Assigne { get; set; }
        public SelectList TeamMembers { get; set; }

        [Display(Name ="Projects")]
        public MultiSelectList Projects { get; set; }

        public List<Guid> SelectedProjects { get; set; }

        public TaskCreateViewModel()
        {

        }
        public TaskCreateViewModel(IEnumerable<Member> members, IEnumerable<ProjectModel> projects)
        {
            TeamMembers = new SelectList(members, "Id", "UserName");
            Projects = new MultiSelectList(projects, "Id", "Name");
        }
        public TaskCreateViewModel(IEnumerable<Member> members, IEnumerable<ProjectModel> projects, ProjectModel parentProject)
        {
            SelectedProjects = new List<Guid>() { parentProject.Id };
            TeamMembers = new SelectList(members, "Id", "UserName");
            Projects = new MultiSelectList(projects, "Id", "Name",SelectedProjects);
        }

    }
}
