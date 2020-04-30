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

            AdminDashW vMod = new AdminDashW();
            vMod.ListOfProducts = dbContext.Products.ToList();
            vMod.ListOfCategories = dbContext.Categories.ToList();

            return View("AdminDash", vMod);
            }
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("filterProducts")]
        public IActionResult FilterProducts(AdminDashW searchString)
        {
            AdminDashW vMod = new AdminDashW();
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
                if(formData.Product.Image != null)
                {
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
                    //saving to local folder and creatin a stream
                    var stream = new FileStream(filePath, FileMode.Create);
                    formData.Product.Image.CopyTo(stream);
                    //saving in db only needed part of path for example "Images\Hats\<FileName>.jpg"
                    formData.Product.ImageUrl = filePath.Substring(8);
                    //Closing stream to not to have an error with FileOI system
                    stream.Close();
                } 
                else
                {
                    ModelState.AddModelError("Product.Image", "Image cannot be empty");
                    return View("AddProductPage", vMod);
                }
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
            AddProductPageW vMod = new AddProductPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            vMod.Product = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            return View(vMod);
        }

        [HttpPost("editingProduct/{id}")]
        public IActionResult EditProduct(int id, AddProductPageW fromForm)
        {
            System.Console.WriteLine("============================");
            System.Console.WriteLine(id);
            System.Console.WriteLine("============================");
            int catId = fromForm.Product.CategoryId;
            if (ModelState.IsValid)
            {
                if (dbContext.Products.Any(b => b.ProductName == fromForm.Product.ProductName && b.ProductId != id))
                {
                    ModelState.AddModelError("Name", "You cannot create a Product with the same name as an existing Product.");
                    return View("ProductDetailsPage", fromForm);
                }

                if(fromForm.Product.Image != null)
                {
                    //pulling right gategory from DB
                    Category cat = dbContext.Categories.FirstOrDefault(c => c.CategoryId == catId);
                    //saving initial filename
                    var uniqueFileName = fromForm.Product.Image.FileName;
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
                    //saving to local folder and creatin a stream
                    var stream = new FileStream(filePath, FileMode.Create);
                    fromForm.Product.Image.CopyTo(stream);
                    //saving in db only needed part of path for example "Images\Hats\<FileName>.jpg"
                    fromForm.Product.ImageUrl = filePath.Substring(8);
                    //Closing stream to not to have an error with FileOI system
                    stream.Close();
                } 
                if(fromForm.Product.Image == null)
                {
                    dbContext.Entry(fromForm.Product).Property("ImageUrl").IsModified = false;

                }


                Product toUpdate = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
                toUpdate.CategoryId = fromForm.Product.CategoryId;
                toUpdate.ProductName = fromForm.Product.ProductName;
                toUpdate.Description = fromForm.Product.Description;
                toUpdate.Quantity = fromForm.Product.Quantity;
                toUpdate.ProductId = id;
                if(fromForm.Product.Image != null) {
                    toUpdate.ImageUrl = fromForm.Product.ImageUrl;
                }
                // fromForm.Product.ProductId = id;
                // dbContext.Products.Update(fromForm.Product);
                // dbContext.Entry(fromForm.Product).Property("CreatedAt").IsModified = false;
                dbContext.Products.Update(toUpdate);
                dbContext.SaveChanges();

                return RedirectToAction("AdminDash", fromForm);
            }

            AddProductPageW vMod = new AddProductPageW();
            vMod.ListOfCategories = dbContext.Categories.ToList();
            vMod.Product = dbContext.Products.FirstOrDefault(p => p.ProductId == id);
            return View("ProductDetailsPage", vMod);
        }

        [HttpGet("addCategoryPage")]
        public IActionResult AddCategoryPage()
        {
            AddCategoryPageW vMod = new AddCategoryPageW();
            vMod.ListOfCategories = dbContext.Categories
                .Include(c => c.ListOfProducts)
                .ToList();
            return View(vMod);
        }

        [HttpPost("addCategory")]
        public IActionResult AddCategory (AddCategoryPageW formData)
        {
            AddCategoryPageW vMod = new AddCategoryPageW();
            vMod.ListOfCategories = dbContext.Categories
                .Include(c => c.ListOfProducts)
                .ToList();
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

        [HttpGet("deleteCategory/{catId}")]
        public IActionResult DeleteCategory (int catId)
        {
            Category toBeDeleted = dbContext.Categories.FirstOrDefault(c => c.CategoryId == catId);
            dbContext.Categories.Remove(toBeDeleted);
            dbContext.SaveChanges();
            return RedirectToAction("AddCategoryPage");
        }


    }
}
