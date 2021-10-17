using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class SectionEditViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(200, MinimumLength = 2,
ErrorMessage = "Section name must be within a minimum length 2 and maximum length 50")]
        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }
    }
}
