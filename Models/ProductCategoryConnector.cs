using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Truckette.Models
{
    public class ProductCategoryConnector
    {
        [Key]
        public int PCContectorId { get; set; }

        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        //Navigation properties
        public Product Product { get; set; }
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}