using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Domain
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Team> Teams { get; set; }
    }
}
