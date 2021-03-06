using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManager.Domain;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ProjectManager.Web.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public TaskCreateViewModel TaskCreateViewModel { get; set; }
        public ProjectModel Project { get; set; }

        public ProjectDetailsViewModel(ProjectModel project)
        {
            TaskCreateViewModel = new TaskCreateViewModel();
            Project = project;
        }
        public ProjectDetailsViewModel(ProjectModel project, TaskCreateViewModel viewModel)
        {
            TaskCreateViewModel = viewModel;
            Project = project;
        }
        public ProjectDetailsViewModel()
        {
        }
    }
}
