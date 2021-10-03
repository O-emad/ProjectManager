using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public TaskController(IProjectManagerRepository repository, IMapper mapper )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(repository));
        }

       
        [HttpPost]
        public IActionResult AddTask(Guid id, ProjectDetailsViewModel viewmodel)
        {

            var task = new TaskModel()
            {
                AssigneeId = viewmodel.TaskCreateViewModel.Assigne,
                Name = viewmodel.TaskCreateViewModel.Name,
                Description = viewmodel.TaskCreateViewModel.Description,
                DueDate = viewmodel.TaskCreateViewModel.DueDate,
            };
            var taskToAdd = mapper.Map<Task>(task);
            repository.AddTask(taskToAdd, viewmodel.TaskCreateViewModel.SelectedProjects);
            repository.Save();
            return RedirectToAction("ProjectDetails","Project", new { id = id });
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

        public IActionResult EditTask(Guid id, Guid callingProjectId = default)
        {
            var task = mapper.Map<TaskModel>(repository.GetTaskById(id, includeProject: true, includePerson: true));
            var projects = mapper.Map<List<ProjectModel>>(repository.GetProjects());
            var members = mapper.Map<List<Member>>(repository.GetPersons());
            var viewModel = new TaskEditViewModel(task, members, projects, callingProjectId);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult EditTask(TaskEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var projects = mapper.Map<List<ProjectModel>>(repository.GetProjects());
                var members = mapper.Map<List<Member>>(repository.GetPersons());
                viewModel.Generate(members, projects);
                return View(viewModel);
            }
            var task = mapper.Map<TaskEditDto>(viewModel);
            var _task = repository.GetTaskById(viewModel.Id,includeProject: true);
            //var projectsList = new List<Project>(task.ProjectIds.Select(t => new Project { Id = t }));
            //var query = from project in taskToEdit.Projects
            //            join projectid in projectsList
            //            on project.Id equals projectid.Id into gj
            //            from subpro in gj.DefaultIfEmpty()
            //            select subpro;
            //taskToEdit.Projects = query.ToList();

            //var editedTask = mapper.Map<Task>(task);
            //repository.UpdateTask(editedTask);
            mapper.Map(task, _task);
            var newProjects = new List<Project>();
            foreach (var proj in task.ProjectIds)
            {
                newProjects.Add(repository.GetProjectById(proj));
            }
            _task.Projects = newProjects;
            repository.Save();


            if (viewModel.CallingProjectId != default)
                return RedirectToAction("ProjectDetails", "Project", new { id = viewModel.CallingProjectId });
            return RedirectToAction("Index");
        }


    }
}
