using ProjectManager.Domain;
using ProjectManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.ViewModels
{
    public class MemberIndexViewModel
    {
        public List<Member> Members { get; set; }
        public MemberIndexViewModel()
        {
        }
    }
}
