using Microsoft.AspNetCore.Mvc.Rendering;
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

        public Guid Assigne { get; set; }
        public SelectList TeamMembers { get; set; }
    }
}
