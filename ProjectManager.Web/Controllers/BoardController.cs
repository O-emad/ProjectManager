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
    public class BoardController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public BoardController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index()
        {
            var viewModel = new BoardIndexViewModel();
            var sections = repository.GetBoardSections(includeTasks: true, includeTaskProject:true);
            viewModel.Sections = mapper.Map<List<BoardSectionModel>>(sections);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddSection(BoardIndexViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("index", viewModel);
            }

            var section = new BoardSectionModel
            {
                Name = viewModel.SectionCreateViewModel.Name
            };

            var sectionToAdd = mapper.Map<BoardSection>(section);
            repository.AddBoardSection(sectionToAdd);
            repository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSection(Guid id, string redirectUrl)
        {
            var section = repository.GetBoardSectionById(id,includeTasks:true);
            if(section!= null)
            {
                if (section.Tasks.Count > 0)
                {
                    ViewBag.ErrorTitle = "this section has tasks in it, please delete the tasks first";
                    return View("Error");
                }
                repository.DeleteBoardSection(section);
                repository.Save();
            }
            return LocalRedirect(redirectUrl);
        }

        public IActionResult EditSection(Guid id)
        {
            var section = repository.GetBoardSectionById(id);
            if (section == null)
            {
                ViewBag.ErrorMessage = "the given section was not found";
                return View("NotFound");
            }
            var _section = mapper.Map<BoardSectionModel>(section);
            var viewModel = new BoardSectionEditViewModel
            {
                Id = _section.Id,
                Name = _section.Name,
            };
            return View(viewModel);

        }
        [HttpPost]
        public IActionResult EditSection(BoardSectionEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var section = repository.GetBoardSectionById(viewModel.Id);
            if (section != null)
            {
                section.Name = viewModel.Name;
                repository.Save();
            }
            return RedirectToAction("Index");
        }


    }
}
