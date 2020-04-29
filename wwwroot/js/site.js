// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// $(document).ready(function(){
//     $(document).on('submit', '#addCategory', function(e){
//         // Prevent the default action (in the case of an "a" tag, firing off an http request)
//         e.preventDefault();
//         // Make an async get request to the href attribute and run a function
//         $.ajax({
//             url: "AddCategoryUrl",
//             method: 'POST',
//             data: $(this).serialize(),
//             success: function (res) {
//                 console.log(res)
//                 if(res.data) {
//                     $("#errorMessage").html(res.data);
//                 } else {
//                     location.reload();
//                 }
//             },
//             error: function(err) {
//                 console.log("error")
//                 console.log(err.data)
//                 alert(err.data)
//                 // $("#errorMessage").html("dasdasdasd");
//                 $("#errorMessage").append("<span>" + err.data + "</span>");
//             }
//         })
//     })
// })

// [HttpPost("AddCategoryUrl")]
//         public IActionResult addCategory(ProductsPageW formData)
//         {
//             String message = "";
//             System.Console.WriteLine("=========================FIRST===============================");
//             System.Console.WriteLine(ModelState.IsValid);
//             foreach (var item in ModelState.Values)
//             {
//                 foreach (var err in item.Errors)
//                 {
//                         System.Console.WriteLine(err);
//                         System.Console.WriteLine("--------------------------");
//                     if (err.ErrorMessage.Contains("Category"))
//                     {
//                         System.Console.WriteLine(err.ErrorMessage);
//                     }
//                 }
//             }
//             System.Console.WriteLine("=========================FIRST===============================");
//             if (ModelState.IsValid)
//             {
//                 System.Console.WriteLine("=========================VALID===============================");
//                 if (dbContext.Categories.Any(b => b.Name == formData.Category.Name))
//                 {
//                     System.Console.WriteLine("=========================SAME NAME===============================");
//                     ModelState.AddModelError("Category.Name", "You cannot create a Category with the same name as an existing Category.");
//                     foreach (var item in ModelState.Values)
//                     {
//                         foreach (var err in item.Errors)
//                         {
//                             if (err.ErrorMessage.Contains("Category"))
//                             {
//                                 message += err.ErrorMessage;
//                             }
//                         }
//                     }
//                     // ModelState.Values.ToList().ForEach(e => e.Errors.Where(m => m.ErrorMessage.Contains("Category")));

//                     return Json(new { data = message });
//                 }
//                 System.Console.WriteLine("=========================Saving to DB===============================");
//                 dbContext.Categories.Add(formData.Category);
//                 dbContext.SaveChanges();

//                 return RedirectToAction("AdminDash");
//             }
//             System.Console.WriteLine("=========================Empty===============================");
//             // ModelState.Values.ToList().ForEach(e => e.Errors.Where(m => m.ErrorMessage.Contains("Category")));
//             foreach (var item in ModelState.Values)
//             {
//                 foreach (var err in item.Errors)
//                 {
//                     if (err.ErrorMessage.Contains("Category"))
//                     {
//                         message += err.ErrorMessage;
//                     }
//                 }
//             }
//             return Json(new { data = message });
//         }