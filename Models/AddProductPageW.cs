using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class AddProductPageW
    {

        public Product Product { get; set; }
        public List<Category> ListOfCategories { get; set; }
        public string CategoryName { get; set; }
    }
}