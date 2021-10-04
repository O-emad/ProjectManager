using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
        public List<Person> Persons { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}
