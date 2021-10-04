using AutoMapper;
using ProjectManager.Domain;
using ProjectManager.Web.DTO;
using ProjectManager.Web.Models;
using ProjectManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectManager.Web
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<ApplicationUser, Member>().ReverseMap();
            CreateMap<Team, TeamModel>()
                .ForMember(dest=>dest.Members, opt=>opt.MapFrom(src=>src.User))
                .ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();
            CreateMap<Task, TaskModel>().ReverseMap();
            CreateMap<TaskEditDto, Task>()
                .ForMember(dest => dest.Projects,
                opt => opt.Ignore()
                //opt.MapFrom(src => src.ProjectIds.Select(p => new Project { Id = p }))
                );
            CreateMap<TaskEditViewModel, TaskEditDto>()
                .ForMember(dest => dest.ProjectIds,
                opt =>
                opt.MapFrom(src => src.SelectedProjects));
        }



    }
}
