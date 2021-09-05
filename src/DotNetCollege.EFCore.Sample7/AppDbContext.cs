using DotNetCollege.EFCore.Sample7.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;

namespace DotNetCollege.EFCore.Sample7
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductBase> Products { get; set; }
        public DbSet<DiscountProduct> DiscountProducts { get; set; }
        public DbSet<PremiumProduct> PremiumProducts { get; set; }


        public AppDbContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample7;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(new Order() { Id = 1, Summary = 0 });

            //TPT EF Core 5 feature
            //modelBuilder.Entity<DiscountProduct>().ToTable(nameof(DiscountProduct));
            //modelBuilder.Entity<PremiumProduct>().ToTable(nameof(PremiumProduct));
        }

    }
}
