using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truckette.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Truckette.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

 

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
            return View();
            }
            User ouruser = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return View(ouruser);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
