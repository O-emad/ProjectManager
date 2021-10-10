using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class HomeIndexViewModel
    {
        public Dictionary<string,string> Summary { get; set; }
        public List<TaskModel> Tasks { get; set; }


    }
    public static class Section
    {
        public const string team = "team";
        public const string project = "project";
        public const string task = "task";
        public const string user = "user";
    }

}
