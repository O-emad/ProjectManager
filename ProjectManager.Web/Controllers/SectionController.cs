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
    public class SectionController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public SectionController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public IActionResult AddSection(Guid id, ProjectDetailsViewModel viewmodel)
        {

            var section = new SectionModel()
            {
                Name = viewmodel.SectionCreateViewModel.Name
            };
            var sectionToAdd = mapper.Map<ProjectSection>(section);
            repository.AddSection(id,sectionToAdd);
            repository.Save();
            return RedirectToAction("ProjectDetails", "Project", new { id = id });
        }
    }
}
