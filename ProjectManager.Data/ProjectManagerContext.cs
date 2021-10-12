using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Domain;
using System;

namespace ProjectManager.Data
{
    public class ProjectManagerContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
    {
        public ProjectManagerContext()
        {

        }
        public ProjectManagerContext(DbContextOptions<ProjectManagerContext> dbContextOptions)
            :base(dbContextOptions)
        {

        }
        

        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ProjectSection> Sections { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(new ApplicationRole {Id= Guid.NewGuid(), Name= "Admin", NormalizedName = "Admin".ToUpper() },
                new ApplicationRole {Id= Guid.NewGuid(), Name = "User", NormalizedName = "User".ToUpper() });
            base.OnModelCreating(builder);
        }
    }
}
