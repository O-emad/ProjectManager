using AutoMapper;
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


        
        public IActionResult CreateProject()
        {
            
            var teams = repository.GetTeams();
            var teamsList = mapper.Map<List<TeamModel>>(teams);
            var viewModel = new ProjectCreateViewModel(teamsList);
            return View(viewModel);
        }

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

        public IActionResult EditProject()
        {
            return View();
        }

        public IActionResult ProjectDetails(Guid id)
        {
            
            var project = mapper.Map<ProjectModel>(repository.GetProjectById(id,includeTasks: true));
            if(project == null)
            {
                return View("NotFound");
            }
            project.Tasks = project.Tasks.OrderBy(p => p.DueDate).ToList();
            var projects = mapper.Map<List<ProjectModel>>(repository.GetProjects());
            var members = mapper.Map<List<Member>>( repository.GetPersons());
            var creatTaskViewModel = new TaskCreateViewModel(members, projects, project);
            var viewModel = new ProjectDetailsViewModel(project,creatTaskViewModel);
            return View(viewModel);
        }

       

    }
}
