using DotNetCollege.EFCore.Sample10;
using DotNetCollege.EFCore.Sample10.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCollege.EFCore.Sample10
{
    class Program
    {
        static void Main(string[] args)
        {
            LazyLoadingExample();
        }

        private static void LazyLoadingExample()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var categories = new List<Category>();

                for (int i = 0; i < 5; i++)
                {
                    categories.Add(new Category() { Name = "Milk products" + i });
                }

                for (int a = 0; a < 3; a++)
                {
                    db.Products.Add(new Product() { Name = $"Milk{a}", Categories = categories });
                }

                db.SaveChanges();
            }

            using (var db = new AppDbContext())
            {
                foreach (var product in db.Products.ToList())
                {
                    foreach (var category in product.Categories)
                    {
                        Console.WriteLine($"Product {product.Name}, Category {category.Name}");
                    }
                }
            }
        }

    }
}
