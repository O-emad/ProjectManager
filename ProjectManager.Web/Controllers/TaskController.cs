using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Domain;
using ProjectManager.Services;
using ProjectManager.Web.DTO;
using ProjectManager.Web.Models;
using ProjectManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public TaskController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpPost]
        public IActionResult AddTask(Guid id, ProjectDetailsViewModel viewmodel)
        {

            var task = new TaskModel()
            {
                UserId = viewmodel.TaskCreateViewModel.Assigne,
                Name = viewmodel.TaskCreateViewModel.Name,
                Description = viewmodel.TaskCreateViewModel.Description,
                DueDate = viewmodel.TaskCreateViewModel.DueDate,

            };
            var taskToAdd = mapper.Map<Task>(task);
            repository.AddTask(taskToAdd, viewmodel.TaskCreateViewModel.Section, viewmodel.TaskCreateViewModel.BoardSection);
            repository.Save();
            return RedirectToAction("ProjectDetails", "Project", new { id = id });
        }

        public IActionResult ToggleTaskCompletion(Guid id, string redirectUrl)
        {
            var task = repository.GetTaskById(id);
            if (task != null)
            {
                task.CompletionStatus = !task.CompletionStatus;
                repository.Save();
            }

            return LocalRedirect(redirectUrl);
        }

        public IActionResult UpdateTaskSection(Guid id, Guid sectionId, string type)
        {
            object section = null;
            bool isBoardSection = false;
            var task = repository.GetTaskById(id, includeSection: true, includeMainSection: true);
            if (!string.IsNullOrWhiteSpace(type))
            {
                if (type.ToLower() == "project")
                {
                    section = repository.GetSectionById(sectionId);
                }
                else if (type.ToLower() == "board")
                {
                    section = repository.GetBoardSectionById(sectionId);
                    isBoardSection = true;
                }
            }
            else
            {
                return BadRequest("request was made from unknown controller");
            }

            if (task != null && section != null)
            {
                if (isBoardSection)
                {
                    var _section = section as BoardSection;
                    task.BoardSection = _section;
                }
                else
                {
                    var _section = section as ProjectSection;
                    task.Section = _section;
                }
                repository.Save();
                return Ok(new { statuscode = StatusCodes.Status200OK, message = "task section updated successfully" });
            }
            return NotFound(new { statuscode = StatusCodes.Status404NotFound, message = "task or section not found" });
        }

        public IActionResult DeleteTask(Guid id, string redirectUrl)
        {
            var task = repository.GetTaskById(id);
            if (task != null)
            {
                repository.DeleteTask(task);
                repository.Save();
            }
            return LocalRedirect(redirectUrl);
        }

        public IActionResult EditTask(Guid id, string requestUrl = default)
        {
            var _a = Request;
            var task = mapper.Map<TaskModel>(repository.GetTaskById(id, includeProject: true, includeUser: true
                , includeSection: true, includeMainSection: true));
            var members = mapper.Map<List<Member>>(repository.GetUsers());
            var sections = mapper.Map<List<SectionModel>>(repository.GetSectionForProject(task.Project.Id));
            var boardSections = mapper.Map<List<BoardSectionModel>>(repository.GetBoardSections());
            var viewModel = new TaskEditViewModel(task, members,sections, boardSections, task.Project.Id);
            viewModel.ReturnUrl = requestUrl;
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditTask(TaskEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
               
                var members = mapper.Map<List<Member>>(repository.GetUsers());
                var sections = mapper.Map<List<SectionModel>>(repository.GetSectionForProject(viewModel.TaskProjectId));
                var boardSections = mapper.Map<List<BoardSectionModel>>(repository.GetBoardSections());
                viewModel.Generate(members,sections,boardSections);
                return View(viewModel);
            }
            var task = mapper.Map<TaskEditDto>(viewModel);
            var _task = repository.GetTaskById(viewModel.Id, includeProject: true,
                includeSection: true, includeMainSection: true);
            mapper.Map(task, _task);
            var section = repository.GetSectionById(viewModel.Section);
            var boardSection = repository.GetBoardSectionById(viewModel.BoardSection);
            _task.Section = section;
            _task.BoardSection = boardSection;
            repository.Save();


            if (viewModel.ReturnUrl != default)
                return LocalRedirect(viewModel.ReturnUrl);
            return RedirectToAction("Index", "Home");
        }


    }
}
