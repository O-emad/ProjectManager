﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Models
{
    public class SectionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Task> Tasks { get; set; }
        public Guid ProjectId { get; set; }
    }
}
