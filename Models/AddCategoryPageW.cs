using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class AddCategoryPageW
    {

        public Category Category { get; set; }
        public List<Category> ListOfCategories { get; set; }
    }
}