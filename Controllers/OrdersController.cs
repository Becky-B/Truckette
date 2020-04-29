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
    public class OrdersController : Controller
    {

        private MyContext dbContext;

        public OrdersController(MyContext context)
        {
            dbContext = context;
        }

        // Cart Page

        // Adding to Cart    
        [HttpPost("addtocart")]
        public IActionResult AddToCart(ProductsW formdata)
        {
            // System.Console.WriteLine("==================================");
            // System.Console.WriteLine("==================================");
            // foreach (var i in ModelState.Values)
            // {
            //     foreach (var error in i.Errors)
            //     {
            //         System.Console.WriteLine(error.ErrorMessage);
            //     }
            // }
            // System.Console.WriteLine("==================================");
            // System.Console.WriteLine("==================================");
            formdata.Products = new List<Product>();
            int userid = (int)HttpContext.Session.GetInt32("UserId");
            if (ModelState.IsValid)
            {   
                formdata.Order.Amount = 32;
                formdata.Order.UserId = userid;
                dbContext.Orders.Add(formdata.Order);
                dbContext.SaveChanges();
                return RedirectToAction("Cart");               
            }
            return RedirectToAction("Cart", "Orders");
        }

        [HttpGet("cart")]
        public IActionResult Cart()
        {
            var ouruser = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            CartW ourcart = new CartW();
            ourcart.ItemsInCart =  dbContext.Orders
                .Include(o => o.Product)
                .Where(o => o.UserId == HttpContext.Session.GetInt32("UserId"))
                .ToList();
            ourcart.User = ouruser;
            return View(ourcart);
        }

        [HttpGet("hats")]
        public IActionResult Hats()
        {
            ProductsW ourguy = new ProductsW();
            ourguy.Products = dbContext.Products
                .Include(p => p.Orders)
                .Include(p => p.Category)
                .Where(p => p.Category.Name == "Hats")
                .ToList();
            ourguy.User = dbContext.Users
                .FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
            return View(ourguy);
        }
    }
}