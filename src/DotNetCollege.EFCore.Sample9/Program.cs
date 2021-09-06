using DotNetCollege.EFCore.Sample9.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;

namespace DotNetCollege.EFCore.Sample9
{
    /// <summary>
    /// Ukázka feačur pouze v EF Core 5
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Init();

            //ModelLevelQueryFilters();

            EfFunctionsExample();

        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");
                db.Add(new Product { Name = "Milk" });
                db.Add(new Product { Name = "Deleted milk", IsDeleted = true });
                db.SaveChanges();

            }
        }

        private static void ModelLevelQueryFilters()
        {
            using (var db = new AppDbContext())
            {
                Console.WriteLine("Querying");

                var nonDeletedProducts = db.Products
                    .OrderBy(b => b.Id)
                    .ToList();

                var allProducts = db.Products
                    .IgnoreQueryFilters()
                    .OrderBy(b => b.Id)
                    .ToList();
            }
        }


        private static void EfFunctionsExample()
        {
            //Adding more milk
            using (var db = new AppDbContext())
            {
                Console.WriteLine("Inserting");
                db.Add(new Product { Name = "Milk", Category = new Category() { Name = "Milk Products" } });
                db.SaveChanges();
            }


            Product milkProduct;

            //LIKE function
            using (var db = new AppDbContext())
            {
                Console.WriteLine("Querying");

                var nonDetedProducts = db.Products
                    .Where(p => EF.Functions.Like(p.Name, "%Milk%"))
                    .ToList();

                milkProduct = nonDetedProducts.Last();
            }

            //Contains in collection function
            using (var db = new AppDbContext())
            {
                var categoriesWithMilk = db.Categories
                       .Where(c => c.Products.Contains(milkProduct))
                       .ToList();
            }


            //Contains function
            //fulltext is not enabled, FREETEXT is also not available 
            using (var db = new AppDbContext())
            {
                var productCategories = db.Categories
                       .Where(c => EF.Functions.Contains(c.Name, "NEAR(Products)"))
                       .ToList();
            }

        }
    }
}
