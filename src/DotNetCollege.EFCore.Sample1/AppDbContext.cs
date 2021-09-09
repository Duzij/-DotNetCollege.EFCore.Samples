using DotNetCollege.EFCore.Sample01;
using DotNetCollege.EFCore.Sample1.Model;
using DotNetCollege.EFCore.Sample1.ModelConfiguration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
         
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ReplaceService<IProviderConventionSetBuilder, CustomSetBuilder>();

            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample1;Trusted_Connection=True;");
        }


        [Keyless]
        public class GetProductsByPercentageResult
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }

        [DbFunction("GetProductsByPercentage", "dbo")]
        public IQueryable<GetProductsByPercentageResult> GetProductsByPercentage(int percentValue) => FromExpression(() => GetProductsByPercentage(percentValue));


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data annotation override
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired(false);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryEntityConfiguration).Assembly);

            modelBuilder.Entity<ProductView>()
                .HasNoKey()
                .ToView("ProductView");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductView> ProductView { get; set; }

      
    }
}
