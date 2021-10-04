
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Domain
{
    public class ApplicationUser :IdentityUser
    {
        public List<Task> Tasks { get; set; }
        public List<Team> Teams { get; set; }
    }
}
