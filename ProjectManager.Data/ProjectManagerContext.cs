using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain;
using System;

namespace ProjectManager.Data
{
    public class ProjectManagerContext :DbContext
    {
        public ProjectManagerContext()
        {

        }
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> dbContextOptions)
            :base(dbContextOptions)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
