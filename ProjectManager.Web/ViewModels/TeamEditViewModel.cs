using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManager.Domain;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class TeamEditViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        public Guid Id { get; set; }

        public List<Guid> SelectedMembers { get; set; }
        public MultiSelectList TeamMembers { get; set; }

        public TeamEditViewModel()
        {

        }

        public TeamEditViewModel(TeamModel teamModel, IEnumerable<Member> members)
        {
            Id = teamModel.Id;
            Name = teamModel.Name;
            SelectedMembers = teamModel.Members.Select(m=>m.Id).ToList();
            var orderedlist = new MultiSelectList(members, "Id", "UserName", SelectedMembers)
                .OrderBy(i => !i.Selected)
                .Select(i => new { Id = i.Value, UserName = i.Text }).ToList();
            TeamMembers = new MultiSelectList(orderedlist, "Id", "UserName", SelectedMembers);
        }

    }
}
