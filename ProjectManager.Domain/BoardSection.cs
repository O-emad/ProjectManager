using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class BoardSection
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
    }
}
