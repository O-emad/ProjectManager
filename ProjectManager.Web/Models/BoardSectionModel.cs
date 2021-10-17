using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Models
{
    public class BoardSectionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
