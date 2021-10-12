using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.Services;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
   // [Route("Project/[controller]/{projectId}",Name ="ProjectDetails")]
    public class ProjectDetailsController:Controller
    {
        private readonly IProjectManagerRepository repository;
        private readonly IMapper mapper;

        public ProjectDetailsController(IProjectManagerRepository repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IActionResult Index(Guid projectId)
        {
            var project = mapper.Map<ProjectModel>(repository.GetProjectById(projectId));
            if (project == null)
            {
                return View("NotFound");
            }
            return View(project);        
        
        }
        
    }
}
