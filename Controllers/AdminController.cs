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
    public class AdminController : Controller
    {

        private MyContext dbContext;
     
        public AdminController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("adminDash")]
        public ViewResult AdminDash()
        {
            return View();
        }
    }
}