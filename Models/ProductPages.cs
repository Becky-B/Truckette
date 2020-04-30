using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class ProductPages
    {
        public Product Product { get; set; }
        public List<Product> ListOfProducts { get; set; }
        public List<Category> ListOfCategories { get; set; }
        public FilterForm FilterForm { get; set; }
        public Category Category { get; set;}
        public User User { get; set; }
    }
}