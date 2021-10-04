using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private readonly ProjectManagerContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ProjectManagerRepository(ProjectManagerContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }
        #region Create
        public void AddPerson(Person person)
        {
            if (person != null)
                context.Add<Person>(person);
        }

        public void AddProject(Project project, IEnumerable<Guid> associatedTeams = null)
        {

            if (project == null) return;

            if (associatedTeams != null)
            {
                var teamsList = new List<Team>();
                foreach (var teamId in associatedTeams)
                {
                    var team = GetTeamById(teamId);
                    if (team != null)
                        teamsList.Add(team);
                }
                project.Teams = teamsList;
            }
            context.Add<Project>(project);


        }

        public void AddTask(Domain.Task task, Guid projectId = default(Guid))
        {

            if (task == null) return;
            if(projectId != default(Guid))
            {
                var project = GetProjectById(projectId, includeTasks: true);
                if (project != null)
                {
                    project.Tasks.Add(task);
                }
            }
            else
            {
                context.Add<Domain.Task>(task);
            }
                
        }
        public void AddTask(Domain.Task task, IEnumerable<Guid> projectIds)
        {

            if (task == null) return;
            if (projectIds != null)
            {
                foreach (var projectId in projectIds)
                {
                    var project = GetProjectById(projectId, includeTasks: true);
                    if (project != null)
                    {
                        project.Tasks.Add(task);
                    }
                }
            }
            else
            {
                context.Add<Domain.Task>(task);
            }

        }
        public void AddTeam(Team team, IEnumerable<Guid> associatedUsers = null)
        {
            if (team == null) return;

            if (associatedUsers != null)
            {
                var usersList = new List<ApplicationUser>();
                foreach (var userId in associatedUsers)
                {
                    var user = GetUserById(userId);
                    if (user != null)
                        usersList.Add(user);
                }
                team.User = usersList;
            }
            context.Add<Team>(team);
        }
        #endregion

        #region Delete

        public async System.Threading.Tasks.Task DeleteUser(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);
        }
        public void DeletePerson(Person person)
        {
            context.Remove<Person>(person);
        }

        public void DeleteProject(Project project)
        {
            context.Remove<Project>(project);
        }

        public void DeleteTask(Domain.Task task)
        {
            context.Remove<Domain.Task>(task);
        }

        public void DeleteTeam(Team team)
        {
            context.Remove<Team>(team);
        }
        #endregion

        #region Read
        
        public ApplicationUser GetUserById(Guid userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return context.Users.ToList();
        }

        public Person GetPersonById(Guid personId)
        {
            return context.Persons.FirstOrDefault(p => p.Id == personId);
        }

        public Person GetPersonByEmail(string email)
        {
            return context.Persons.FirstOrDefault(p => (p.Mail.ToLower() == email.ToLower()));
        }

        public IEnumerable<Person> GetPersons()
        {
            return context.Persons.ToList();
        }

        public Project GetProjectById(Guid projectId, bool includeTasks = false, bool includeTeams = false)
        {
            var project = context.Projects as IQueryable<Project>;
            if (includeTasks)
                project = project.Include(p => p.Tasks);
            if (includeTeams)
                project = project.Include(p => p.Teams);
            return project.FirstOrDefault(p => p.Id == projectId);
        }



        public IEnumerable<Project> GetProjects()
        {
            return context.Projects.ToList();
        }

        public Domain.Task GetTaskById(Guid taskId, bool includeProject = false, bool includePerson = false)
        {
            var task = context.Tasks as IQueryable<Domain.Task>;
            if (includePerson)
                task = task.Include(t => t.Assignee);
            if (includeProject)
                task = task.Include(t => t.Projects);
            return task.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Domain.Task> GetTasks()
        {
            return context.Tasks.OrderBy(t=>t.DueDate).ToList();
        }
        public IEnumerable<Team> GetTeams()
        {
            return context.Teams.ToList();
        }

        public Team GetTeamById(Guid teamId, bool includeUsers = false)
        {
            var team = context.Teams as IQueryable<Team>;
            if (includeUsers)
                team = team.Include(t => t.User);
            return team.FirstOrDefault(t => t.Id == teamId);
        }

        #endregion

        #region Utility
        public bool PersonExists(Guid personId)
        {
            return context.Persons.Any(p => p.Id == personId);
        }
        public bool EmailExists(string email)
        {
            return context.Persons.Any(p => (p.Mail.ToLower() == email.ToLower()));
        }

        public bool ProjectExists(Guid projectId)
        {
            return context.Projects.Any(p => p.Id == projectId);
        }

        public bool Save()
        {
            return (context.SaveChanges() >= 0);
        }

        public bool TaskExists(Guid taskId)
        {
            return context.Tasks.Any(t => t.Id == taskId);
        }
        public bool TeamExists(Guid teamId)
        {
            return context.Teams.Any(t => t.Id == teamId);
        }

        #endregion

        #region Update
        public void UpdatePerson(Person person)
        {
            //does nothing
        }

        public void UpdateProject(Project project)
        {
            //does nothing
        }

        public void UpdateTask(Domain.Task task)
        {
            //does nothing
        }
        public void UpdateTeam(Team team)
        {
            //does nothing
        }

        #endregion
    }
}
