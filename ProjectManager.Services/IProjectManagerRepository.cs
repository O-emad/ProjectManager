using ProjectManager.Domain;
using System;
using System.Collections.Generic;

namespace ProjectManager.Services
{
    public interface IProjectManagerRepository
    {
        #region Task
        bool TaskExists(Guid taskId);
        IEnumerable<Task> GetTasks();
        Task GetTaskById(Guid taskId);
        void AddTask(Task task);
        void DeleteTask(Task task);
        void UpdateTask(Task task);
        #endregion

        #region Team
        bool TeamExists(Guid teamId);
        IEnumerable<Team> GetTeams();
        Team GetTeamById(Guid teamId, bool includePersons = false);
        void AddTeam(Team team);
        void DeleteTeam(Team team);
        void UpdateTeam(Team team);
        #endregion

        #region Project
        bool ProjectExists(Guid projectId);
        IEnumerable<Project> GetProjects();
        Project GetProjectById(Guid projectId);
        void AddProject(Project project);
        void DeleteProject(Project project);
        void UpdateProject(Project project);
        #endregion

        #region Person
        bool PersonExists(Guid personId);
        bool EmailExists(string email);
        IEnumerable<Person> GetPersons();
        Person GetPersonById(Guid personId);
        Person GetPersonByEmail(string email);
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void UpdatePerson(Person person);
        #endregion


        bool Save();


    }
}
