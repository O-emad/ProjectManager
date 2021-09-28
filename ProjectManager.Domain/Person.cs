using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Mail { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Team> Teams { get; set; }
    }
}
