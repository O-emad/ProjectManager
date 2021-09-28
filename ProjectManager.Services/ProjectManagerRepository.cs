using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Services
{
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private readonly ProjectManagerContext context;

        public ProjectManagerRepository(ProjectManagerContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
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

        public void AddTask(Task task, Guid projectId = default(Guid))
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
                context.Add<Task>(task);
            }
                
        }
        public void AddTeam(Team team, IEnumerable<Guid> associatedPersons = null)
        {
            if (team == null) return;

            if (associatedPersons != null)
            {
                var personsList = new List<Person>();
                foreach (var personId in associatedPersons)
                {
                    var person = GetPersonById(personId);
                    if (person != null)
                        personsList.Add(person);
                }
                team.Persons = personsList;
            }
            context.Add<Team>(team);
        }
        #endregion

        #region Delete

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
            context.Remove<Task>(task);
        }

        public void DeleteTeam(Team team)
        {
            context.Remove<Team>(team);
        }
        #endregion

        #region Read

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

        public Domain.Task GetTaskById(Guid taskId)
        {
            return context.Tasks.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Domain.Task> GetTasks()
        {
            return context.Tasks.ToList();
        }
        public IEnumerable<Team> GetTeams()
        {
            return context.Teams.ToList();
        }

        public Team GetTeamById(Guid teamId, bool includePersons = false)
        {
            var team = context.Teams as IQueryable<Team>;
            if (includePersons)
                team = team.Include(t => t.Persons);
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
