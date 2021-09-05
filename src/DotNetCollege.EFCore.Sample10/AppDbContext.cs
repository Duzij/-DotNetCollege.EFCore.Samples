using DotNetCollege.EFCore.Sample10.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample10
{
    public class AppDbContext : DbContext
    {

        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample10;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<ProductStatus, string>(
                                v => v.ToString(),
                                v => (ProductStatus)Enum.Parse(typeof(ProductStatus), v));

            modelBuilder
                .Entity<Product>()
                .Property(e => e.Status)
                .HasConversion(converter);

            //readonly string Name
            //Without explicit property definition, we gen an error
            modelBuilder
             .Entity<Category>()
             .Property(c => c.Name);

        }

        public DbSet<Product> Products { get; set; }


    }
}
