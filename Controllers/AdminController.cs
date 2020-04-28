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
        public IActionResult AdminDash(ProductsPageW searchString)
        {
            ProductsPageW vMod = new ProductsPageW();
            vMod.ListOfProducts = dbContext.Products.ToList();

            if (searchString != null && searchString.FilterForm != null)
            {
                if (!String.IsNullOrEmpty(searchString.FilterForm.ToLookFor))
                {
                    vMod.ListOfProducts = dbContext.Products
                        .Where(s => s.ProductName.ToLower().Contains(searchString.FilterForm.ToLookFor.ToLower()))
                        .ToList();
                }
            }
            return View("AdminDash", vMod);
        }

        [HttpPost("addProduct")]
        public IActionResult addProduct(ProductsPageW formData)
        {
            if (ModelState.IsValid)
            {
                formData.Product.ImageUrl = formData.Product.Category + "/" + formData.Product.ImageUrl;
                dbContext.Products.Add(formData.Product);
                dbContext.SaveChanges();

                return RedirectToAction("AdminDash");
            }
            // only if there is an error
            return View("AdminDash");
        }

        [HttpGet("edit/{id}")]
        public IActionResult ProductDetailsPage(int id)
        {
            Product vMod = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            return View(vMod);
        }

        [HttpPost("editingProduct/{id}")]
        public IActionResult EditProduct(int id, Product fromForm)
        {
            System.Console.WriteLine($"What's the Product Id? {id}");
            System.Console.WriteLine("------------------------------------");
            if (ModelState.IsValid)
            {
                if (dbContext.Products.Any(b => b.ProductName == fromForm.ProductName && b.ProductId != id))
                {
                    ModelState.AddModelError("Name", "You cannot create a burrito with the same name as an existing burrito.");
                    return View("EditRito", fromForm);
                }
                System.Console.WriteLine("======================================");
                fromForm.ProductId = id;
                dbContext.Update(fromForm);
                dbContext.Entry(fromForm).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();

                return RedirectToAction("AdminDash");
            }

            return View("ProductDetailsPage", fromForm);
        }
    }
}