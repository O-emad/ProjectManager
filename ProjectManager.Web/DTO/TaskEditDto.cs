using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.DTO
{
    public class TaskEditDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool CompletionStatus { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
