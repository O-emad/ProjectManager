using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Data;
using ProjectManager.Domain;
using ProjectManager.Services.Helpers;
using ProjectManager.Services.ResourceParameters;
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

        public void AddSection(Guid projectId ,ProjectSection section)
        {
            if (section == null)
                return;
            var project = context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
                return;
            section.ProjectId = projectId;
            context.Add<ProjectSection>(section);

        }

        public void AddBoardSection(BoardSection section)
        {
            if (section == null)
                return;

            context.Add<BoardSection>(section);

        }

        public void AddTask(Domain.Task task, Guid sectionId = default(Guid), Guid boardSectionId = default(Guid))
        {

            if (task == null) return;
            if(sectionId != default(Guid))
            {
                var section = GetSectionById(sectionId, includeTasks: true);
                if (section != null)
                {
                    task.ProjectId = section.ProjectId;
                    section.Tasks.Add(task);
                }
            }
            if (boardSectionId != default(Guid))
            {
                var section = GetBoardSectionById(boardSectionId, includeTasks: true);
                if (section != null)
                {
                    section.Tasks.Add(task);
                }
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

        public void DeleteBoardSection(BoardSection section)
        {
            context.Remove<BoardSection>(section);
        }

        public async System.Threading.Tasks.Task DeleteUser(ApplicationUser user)
        {
            await userManager.DeleteAsync(user);
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
        
        public IEnumerable<BoardSection> GetBoardSections(bool includeTasks = false, bool includeTaskProject = false)
        {
            var sections = context.BoardSections as IQueryable<BoardSection>;
            if (includeTasks && includeTaskProject)
                sections = sections.Include(s => s.Tasks)
                    .ThenInclude(t => t.Project);
            if (includeTasks && !includeTaskProject)
                sections = sections.Include(s => s.Tasks);
            
            return sections.ToList();
        }
        public BoardSection GetBoardSectionById(Guid sectionId, bool includeTasks = false )
        {
            var section = context.BoardSections as IQueryable<BoardSection>;
            if (includeTasks)
                section = section.Include(s => s.Tasks);
            return section.FirstOrDefault(s => s.Id == sectionId);
            
        }

        public ApplicationUser GetUserById(Guid userId)
        {
            return context.Users.FirstOrDefault(u => u.Id == userId);
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return context.Users.ToList();
        }
        public Project GetProjectById(Guid projectId, bool includeTasks = false, bool includeTeams = false, bool includeSections = false)
        {
            var project = context.Projects as IQueryable<Project>;
            if (includeTeams)
                project = project.Include(p => p.Teams);
            if (includeSections&&includeTasks)
            {
                project = project.Include(p => p.Sections)
                    .ThenInclude(s => s.Tasks);
                
            }
            else
            {
                if (includeTasks)
                    project = project.Include(p => p.Tasks);
                if (includeSections)
                    project = project.Include(p => p.Sections);
            }
            var _project = project.FirstOrDefault(p => p.Id == projectId);
            return _project;
        }

        public IEnumerable<ApplicationUser> GetUsersForProject(Guid projectId)
        {
            var projects = context.Projects as IQueryable<Project>;
            var query = (from project in projects
                         let usersLists = from team in project.Teams
                                          select team.User
                         where project.Id == projectId
                         select usersLists);
            var result = query.FirstOrDefault();
            var _result = result.SelectMany(x => x).Distinct().ToList();

            //var query2 = from project in projects
            //             where project.Id == projectId
            //             let usersLists = from team in project.Teams
            //                              select team.User
            //             from userList in usersLists
            //             from user in userList
            //             select user as IEnumerable<ApplicationUser>;
            //var result2 = query2.FirstOrDefault();
            return _result;
        }


        //public PagedList<Project> GetProjects(ProjectResourceParameters resourceParameters)
        //{
        //    return new PagedList<Project>();
        //}

        public IEnumerable<Project> GetProjects()
        {
            return context.Projects.ToList();
        }

        public ProjectSection GetSectionById(Guid sectionId, bool includeTasks = false)
        {
            var section = context.Sections as IQueryable<ProjectSection>;
            if (includeTasks)
                section = section.Include(s => s.Tasks);
            return section.FirstOrDefault(s => s.Id == sectionId);
        }
        public IEnumerable<ProjectSection> GetSectionForProject(Guid projectId)
        {
            return context.Sections.Where(s => s.ProjectId == projectId).ToList();
        }

        public Domain.Task GetTaskById(Guid taskId, 
            bool includeProject = false, bool includeUser = false, bool includeSection = false, bool includeMainSection = false)
        {
            var task = context.Tasks as IQueryable<Domain.Task>;
            if (includeUser)
                task = task.Include(t => t.User);
            if (includeProject)
                task = task.Include(t => t.Project);
            if (includeSection)
                task = task.Include(t => t.Section);
            if (includeMainSection)
                task = task.Include(t => t.BoardSection);
            return task.FirstOrDefault(t => t.Id == taskId);
        }

        public IEnumerable<Domain.Task> GetTasks()
        {
            return context.Tasks.OrderBy(t=>t.DueDate).ToList();
        }

        public async Task<IEnumerable<Domain.Task>> GetHighPriorityTasks(int size = 0, string userName = "", bool isAdmin = false)
        {
            var task = context.Tasks as IQueryable<Domain.Task>;
            task = task.Where(t=>!t.CompletionStatus)
                .Include(t=>t.Project).OrderBy(t => t.DueDate);
            if (isAdmin)
                return task.Take(size).ToList();

            //not an admin ... get the user specific tasks
            if (!string.IsNullOrWhiteSpace(userName)) {
                var user = await userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return new List<Domain.Task>();
                }
                task = task.Where(t => t.UserId == user.Id);
            }
            return task.Take(size).ToList();

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
        
        public Dictionary<string,string> Summary()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("team", context.Teams.Count().ToString());
            dic.Add("project", context.Projects.Count().ToString());
            dic.Add("task", context.Tasks.Count().ToString());
            dic.Add("user", context.Users.Count().ToString());
            dic.Add("completedtasks", context.Tasks.Where(t => t.CompletionStatus).Count().ToString());
            return dic;
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
