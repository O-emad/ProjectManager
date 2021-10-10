using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManager.Domain;
using ProjectManager.Services;
using ProjectManager.Web.Models;
using ProjectManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager,
            IProjectManagerRepository repository, IMapper mapper)
        {
            _logger = logger;
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new HomeIndexViewModel();
            var summary = repository.Summary();
            var user = User.Identity.Name;
            var tasks = await repository.GetHighPriorityTasks(5, user, User.IsInRole("Admin"));
            viewModel.Summary = summary;
            viewModel.Tasks = mapper.Map<List<TaskModel>>(tasks);
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
