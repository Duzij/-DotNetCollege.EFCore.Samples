using DotNetCollege.EFCore.Sample12.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace DotNetCollege.EFCore.Sample12
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample12;Trusted_Connection=True;");

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().ToFunction(
            //);

            modelBuilder.Entity<Product>().ToSqlQuery(
            @" --Comment test
            SELECT TOP (1)
                [Id]
                ,[Name]
                ,[Description]
                ,[OwnerId]
            FROM [dbo].[Products]");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Owner> Owners { get; set; }

    }
}
