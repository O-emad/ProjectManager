using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Web.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }
        
    }
}
