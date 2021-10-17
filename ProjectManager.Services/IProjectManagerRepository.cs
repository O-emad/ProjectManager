﻿using ProjectManager.Domain;
using System;
using System.Collections.Generic;

namespace ProjectManager.Services
{
    public interface IProjectManagerRepository
    {
        #region Task
        bool TaskExists(Guid taskId);
        IEnumerable<Task> GetTasks();
        System.Threading.Tasks.Task<IEnumerable<Task>> GetHighPriorityTasks(int size = 0, string userName = "", bool isAdmin = false);
        Task GetTaskById(Guid taskId, bool includeProject = false, bool includeUser = false, bool includeSection = false, bool includeMainSection = false);
        void AddTask(Task task, Guid sectionId = default(Guid), Guid boardSectionId = default(Guid));
        void AddTask(Task task, IEnumerable<Guid> projectIds);
        void DeleteTask(Task task);
        void UpdateTask(Task task);
        #endregion

        #region Team
        bool TeamExists(Guid teamId);
        IEnumerable<Team> GetTeams();
        Team GetTeamById(Guid teamId, bool includeUsers = false);
        void AddTeam(Team team, IEnumerable<Guid> associatedUsers = null);
        void DeleteTeam(Team team);
        void UpdateTeam(Team team);
        #endregion

        #region Project
        bool ProjectExists(Guid projectId);
        IEnumerable<Project> GetProjects();
        Project GetProjectById(Guid projectId, bool includeTasks = false, bool includeTeams = false, bool includeSections = false);
        void AddProject(Project project, IEnumerable<Guid> associatedTeams = null);
        void DeleteProject(Project project);
        void UpdateProject(Project project);

        public IEnumerable<ApplicationUser> GetUsersForProject(Guid projectId);
        #endregion

        #region ProjectSection
        public ProjectSection GetSectionById(Guid sectionId, bool includeTasks = false);
        public IEnumerable<ProjectSection> GetSectionForProject(Guid projectId);
        public void AddSection(Guid projectId, ProjectSection section);
        public void DeleteSection(ProjectSection section);
        #endregion

        #region BoardSection
        public void AddBoardSection(BoardSection section);
        public IEnumerable<BoardSection> GetBoardSections(bool includeTasks = false, bool includeTaskProject = false);
        public BoardSection GetBoardSectionById(Guid sectionId, bool includeTasks = false);
        public void DeleteBoardSection(BoardSection section);
        #endregion

        #region User
        System.Threading.Tasks.Task DeleteUser(ApplicationUser user);
        public ApplicationUser GetUserById(Guid userId);
        public IEnumerable<ApplicationUser> GetUsers();
        #endregion

        bool Save();
        public Dictionary<string, string> Summary();

    }
}
