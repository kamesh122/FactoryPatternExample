using FactoryPatternExample.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FactoryPatternExample.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }

}
