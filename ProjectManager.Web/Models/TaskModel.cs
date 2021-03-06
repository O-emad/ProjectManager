using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Models
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool CompletionStatus { get; set; }
        public Guid AssigneeId { get; set; }
        public List<ProjectModel> Projects { get; set; }
    }
}
