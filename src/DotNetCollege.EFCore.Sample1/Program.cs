using DotNetCollege.EFCore.Sample1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DotNetCollege.EFCore.Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            QueryFunction();
            QueryView();
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                Console.WriteLine("Inserting products");

                var milkProductsCategory = new Category() { Name = "Milk Products" };
                db.Categories.Add(milkProductsCategory);

                db.Add(new Product { Name = "Milk", Category = milkProductsCategory });
                db.Add(new Product { Name = "Second Milk", Category = milkProductsCategory });
                db.Add(new Product { Name = "Third Milk", Category = milkProductsCategory });

                db.SaveChanges();
            }
        }

        private static void QueryFunction()
        {
            using (var db = new AppDbContext())
            {
                var top1Percent = db.GetProductsByPercentage(1).ToList();
                foreach (var product in top1Percent)
                {
                    Console.WriteLine($"{product.ProductId}: {product.ProductName}");
                }
            }
        }

        private static void QueryView()
        {
            using (var db = new AppDbContext())
            {
                var products = db.ProductView.ToList();

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductId}: {product.ProductName}");
                }

                Console.WriteLine("DbFunction: Top One Percent Products");
            }
        }
    }
}
