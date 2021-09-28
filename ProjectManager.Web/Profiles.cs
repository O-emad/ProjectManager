using AutoMapper;
using ProjectManager.Domain;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Person, Member>().ReverseMap();
            CreateMap<Team, TeamModel>().ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();
        }



    }
}
