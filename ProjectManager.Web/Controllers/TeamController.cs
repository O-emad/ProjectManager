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
    public class TeamController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public TeamController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var viewModel = new TeamIndexViewModel();
            var teams = mapper.Map<List<TeamModel>>(repository.GetTeams());
            viewModel.Teams = teams;
            return View(viewModel);
        }

        public IActionResult CreateTeam()
        {
            var members = mapper.Map<List<Member>>(repository.GetPersons());
            var viewModel = new TeamCreateViewModel(members);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CreateTeam(TeamCreateViewModel viewModel)
        {
            var membersList = new List<Person>();
            foreach (var memberId in viewModel.SelectedMembers)
            {
                var member = repository.GetPersonById(memberId);
                if (member != null)
                    membersList.Add(member);
            }

            var team = new Team()
            {
                Name = viewModel.Name,
                Persons = membersList
            };

            repository.AddTeam(team);
            repository.Save();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteTeam(Guid id)
        {
            return RedirectToAction("Index");
        }

        public IActionResult EditTeam(Guid id)
        {
            var team = repository.GetTeamById(id,includePersons: true);
            var viewModel = new TeamEditViewModel();
            viewModel.Team = team;
            return View(viewModel);
        }

    }
}
