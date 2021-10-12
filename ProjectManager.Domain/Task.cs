using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public bool CompletionStatus { get; set; }
        public Project Project { get; set; }
        [ForeignKey("Project")]
        public Guid ProjectId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public ProjectSection Section { get; set; }
    }
}
