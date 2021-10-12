using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Domain;
using ProjectManager.Services;
using ProjectManager.Web.Models;
using ProjectManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjectManager.Web.Controllers
{
    
    public class ProjectController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public ProjectController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        public IActionResult Index()
        {
            var projects = repository.GetProjects().ToList();
            var viewModel = new ProjectIndexViewModel()
            {
                Projects = mapper.Map<List<ProjectModel>>(projects)
            };
            return View(viewModel);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult CreateProject()
        {
            
            var teams = repository.GetTeams();
            var teamsList = mapper.Map<List<TeamModel>>(teams);
            var viewModel = new ProjectCreateViewModel(teamsList);
            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateProject(ProjectCreateViewModel viewModel)
        {
   
            var project = new ProjectModel()
            {
                Name = viewModel.Name,
            };

            var projectToAdd = mapper.Map<Project>(project);
            repository.AddProject(projectToAdd, viewModel.SelectedTeams);
            repository.Save();

            return RedirectToAction("Index");
        }

        [Authorize( Roles = "Admin")]
        public IActionResult EditProject(Guid id)
        {
            var project = repository.GetProjectById(id, includeTeams: true);
            if (project == null)
            {
                ViewBag.ErrorMessage = "Project Not Found";
                return View("NotFound");
            }
            var projectModel = mapper.Map<ProjectModel>(project);
            var teams = mapper.Map<List<TeamModel>>(repository.GetTeams());
            var viewModel = new ProjectEditViewModel(projectModel, teams);

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditProject(Guid id, ProjectEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Id = id;
                return View(viewModel);
            }
            var project = repository.GetProjectById(id, includeTeams: true);
            if (project == null)
            {
                ViewBag.ErrorMessage = "Project Not Found";
                return View("NotFound");
            }
            project.Name = viewModel.Name;
            var teams = new List<Team>();
            foreach (var teamId in viewModel.SelectedTeams)
            {
                teams.Add(repository.GetTeamById(teamId));
            }
            project.Teams = teams;
            repository.UpdateProject(project);
            repository.Save();
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult DeleteProject(Guid id)
        {
            var project = repository.GetProjectById(id);
            if (project != null)
            {
                repository.DeleteProject(project);
                repository.Save();
            }
            return RedirectToAction("Index");
        }


        public IActionResult ProjectDetails(Guid id)
        {
            
            var project = mapper.Map<ProjectModel>(repository.GetProjectById(id,includeTasks: true, includeTeams: true, includeSections: true));
            if(project == null)
            {
                return View("NotFound");
            }
            project.Tasks = project.Tasks.OrderBy(p => p.DueDate).ToList();
            var projects = mapper.Map<List<ProjectModel>>(repository.GetProjects());
            //var members = mapper.Map<List<Member>>( repository.GetUsers());
            var members = mapper.Map<List<Member>>( repository.GetUsersForProject(id));
            var creatTaskViewModel = new TaskCreateViewModel(members, projects, project);
            var viewModel = new ProjectDetailsViewModel(project,creatTaskViewModel);
            return View(viewModel);
        }

       

    }
}
