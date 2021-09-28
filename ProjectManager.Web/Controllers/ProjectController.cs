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
using System.Threading.Tasks;

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
            var teamList = new List<Team>();
            foreach (var teamId in viewModel.SelectedTeams)
            {
                var team = repository.GetTeamById(teamId,includePersons: true);
                if (team != null)
                    teamList.Add(team);
            }

            var project = new Project()
            {
                Name = viewModel.Name,
                Teams = teamList
            };

            repository.AddProject(project);
            repository.Save();

            return RedirectToAction("Index");
        }

        public IActionResult EditProject()
        {
            return View();
        }

        
    }
}
