using AutoMapper;
using ProjectManager.Domain;
using ProjectManager.Web.Models;
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
            CreateMap<Person, Member>().ReverseMap();
            CreateMap<Team, TeamModel>()
                .ForMember(dest=>dest.Members, opt=>opt.MapFrom(src=>src.Persons))
                .ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();
            CreateMap<Task, TaskModel>().ReverseMap();
        }



    }
}
