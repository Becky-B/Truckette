using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class CartW
    {
        public User User { get; set; }

        public List<Order> ItemsInCart { get; set; }
    }
}