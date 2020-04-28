using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Product Name: ")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Categories: ")]
        public string Category { get; set; } 

        [Required]
        [Display(Name = "Image (url): ")]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; } = 0;

        // [Required]
        public string DistributionNumber { get; set; }
        //many to many
        public List<Order> Orders { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}