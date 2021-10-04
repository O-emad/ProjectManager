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
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public MemberController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var viewModel = new MemberIndexViewModel();
            var persons = repository.GetUsers();
            var members = mapper.Map<List<Member>>(persons);
            viewModel.Members = members;
            return View(viewModel);
        }

        public IActionResult CreateMember()
        {
            var viewModel = new MemberCreateViewModel();
            return View(viewModel);
        }


        [HttpPost]
        public IActionResult CreateMember(MemberCreateViewModel viewmodel)
        {
            
            return RedirectToAction("Index");
        }

        public IActionResult DeleteMember(Guid id)
        {

            return RedirectToAction("Index");
        }
        
    }
}
