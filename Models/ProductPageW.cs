using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class ProductsPageW
    {
        public Product Product { get; set; }
        public List<Product> ListOfProducts { get; set; }
        public FilterForm FilterForm { get; set; }
    }
}