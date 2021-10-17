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

        public IActionResult EditSection(Guid id)
        {
            var section = repository.GetSectionById(id);
            if (section == null)
            {
                ViewBag.ErrorMessage = "the given section was not found";
                return View("NotFound");
            }
            var _section = mapper.Map<SectionModel>(section);
            var viewModel = new SectionEditViewModel
            {
                Id = _section.Id,
                Name = _section.Name,
                ProjectId = _section.ProjectId
            };
            return View(viewModel);
         }

        [HttpPost]
        public IActionResult EditSection(SectionEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var section = repository.GetSectionById(viewModel.Id);
            if(section != null)
            {
                section.Name = viewModel.Name;
                repository.Save();
            }
            return RedirectToAction("ProjectDetails", "Project", new { id = viewModel.ProjectId });
        }

        public IActionResult DeleteSection(Guid id, string redirectUrl)
        {
            var section = repository.GetSectionById(id);
            if(section != null)
            {
                repository.DeleteSection(section);
                repository.Save();
            }
            return LocalRedirect(redirectUrl);
        }
    }
}
