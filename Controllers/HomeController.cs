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
            return View();
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

        [HttpGet("apparel")]
        public IActionResult Apparel()
        {
            ProductPages vMod = new ProductPages();
            vMod.ListOfProducts = dbContext.Products
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Apparel")
                .ToList();

            return View(vMod);
        }

        [HttpGet("Product/{ProductId}")]
        public IActionResult ViewProduct()
        {
            // Product ourguy = dbContext.Products
            //     .FirstOrDefault(p => p.ProductId == )
            return View("Product");
        }
    }
}
