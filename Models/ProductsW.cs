using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class ProductsW
    {
        public List<Product> Products { get; set; }
        public User User { get; set; }
        public Order Order { get; set; }
    }
}