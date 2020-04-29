using Microsoft.EntityFrameworkCore;
using Truckette.Models;
using System.Collections.Generic;

namespace Truckette.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
      
        public DbSet<Category> Categories { get; set; }

    }
}