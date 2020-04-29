using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Truckette.Models
{
    public class Category{
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name="New Category Name")]
        public string Name { get; set; }

        //nav prop
        public List<Product> ListOfProducts { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}