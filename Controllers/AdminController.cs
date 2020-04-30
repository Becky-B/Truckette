using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Truckette.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Truckette.Controllers
{
    public class AdminController : Controller
    {

        private MyContext dbContext;

        public AdminController(MyContext context, IConfiguration configuration, IHostingEnvironment environment)
        {
            dbContext = context;
            Configuration = configuration;
            hostingEnvironment = environment;
        }
        public IConfiguration Configuration { get; }

        private readonly IHostingEnvironment hostingEnvironment;

        [HttpGet("adminDash")]
        public IActionResult AdminDash()
        {
            if (HttpContext.Session.GetInt32("Admin") == 1411)
            {

            ProductsPageW vMod = new ProductsPageW();
            vMod.ListOfProducts = dbContext.Products.ToList();
            vMod.ListOfCategories = dbContext.Categories.ToList();

            return View("AdminDash", vMod);
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("filterProducts")]
        public IActionResult FilterProducts(ProductsPageW searchString)
        {
            ProductsPageW vMod = new ProductsPageW();
            vMod.ListOfProducts = dbContext.Products.ToList();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            vMod.Product = new Product();
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

        [HttpGet("addProductPage")]
        public IActionResult AddProductPage()
        {
            AddProductPageW vMod = new AddProductPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            return View(vMod);
        }

        [HttpPost("addProduct")]
        public IActionResult addProduct(AddProductPageW formData)
        {            
            AddProductPageW vMod = new AddProductPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            if (ModelState.IsValid)
            {
                if (dbContext.Products.Any(p => p.ProductName == formData.Product.ProductName))
                {
                    ModelState.AddModelError("Product.ProductName", "You cannot create a Product with the same name as an existing Product.");
                    return View("AddProductPage", vMod);
                }
                //getting category id
                int catId = Int32.Parse(formData.CategoryName);
                //pulling right gategory from DB
                Category cat = dbContext.Categories.FirstOrDefault(c => c.CategoryId == catId);
                //saving initial filename
                var uniqueFileName = formData.Product.Image.FileName;
                //Setting my path to start from wwwroot
                string myPath = hostingEnvironment.WebRootPath.Substring(hostingEnvironment.WebRootPath.Length - 7);
                //Setting path to images folder and right category folder
                var uploads = Path.Combine(myPath, "Images", cat.Name);
                //if there is no such folder, crate it
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                //finalizing path and filename
                var filePath = Path.Combine(uploads, uniqueFileName);
                //saving to local folder
                formData.Product.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                //saving in db only needed part of path for example "Images\Hats\<FileName>.jpg"
                formData.Product.ImageUrl = filePath.Substring(8);
                formData.Product.CategoryId = catId;
                dbContext.Products.Add(formData.Product);
                dbContext.SaveChanges();

                return RedirectToAction("AdminDash");
            }
            System.Console.WriteLine("================================");
            // only if there is an error

            return View("AddProductPage", vMod);
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
            if (ModelState.IsValid)
            {
                if (dbContext.Products.Any(b => b.ProductName == fromForm.ProductName && b.ProductId != id))
                {
                    ModelState.AddModelError("Name", "You cannot create a Product with the same name as an existing Product.");
                    return View("ProductDetailsPage", fromForm);
                }
                fromForm.ProductId = id;
                dbContext.Update(fromForm);
                dbContext.Entry(fromForm).Property("CreatedAt").IsModified = false;
                dbContext.SaveChanges();

                return RedirectToAction("AdminDash");
            }

            return View("ProductDetailsPage", fromForm);
        }

        [HttpGet("addCategoryPage")]
        public IActionResult AddCategoryPage()
        {
            AddCategoryPageW vMod = new AddCategoryPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            return View(vMod);
        }

        [HttpPost("addCategoryUrl")]
        public IActionResult AddCategoryUrl (AddCategoryPageW formData)
        {
            AddCategoryPageW vMod = new AddCategoryPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            if (ModelState.IsValid)
            {

                if (dbContext.Categories.Any(b => b.Name == formData.Category.Name))
                {
                    ModelState.AddModelError("Category.Name", "You cannot create a Category with the same name as an existing Category.");

                    return View("AddCategoryPage", vMod);
                }
                dbContext.Categories.Add(formData.Category);
                dbContext.SaveChanges();

                return RedirectToAction("AddProductPage");
            }
            return View("AddCategoryPage", vMod);
        }

    }
}
