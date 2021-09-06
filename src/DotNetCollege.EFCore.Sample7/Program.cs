using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();

                //Optimistic concurrency
                await LoadAndInsertNewValue();

                //TPH
                //await AddNewProducts();

            }
        }

        private static async Task AddNewProducts()
        {
            using (var db = new AppDbContext())
            {
                db.DiscountProducts.Add(new DiscountProduct() { Name = "Discounted Milk", Price = 10, Discount = 5 });
                db.PremiumProducts.Add(new PremiumProduct() { Name = "Premium Milk", Price = 100, Note = "Very fresh!" });
                await db.SaveChangesAsync();
            }

            using (var db = new AppDbContext())
            {
                var discountedProducts = db.Products.OfType<DiscountProduct>().ToList();
                var allProducts = db.Products.ToList();
            }
        }

        public static async Task LoadAndInsertNewValue()
        {
            using (var db = new AppDbContext())
            {
                var order = db.Orders.Find(1);
                Console.WriteLine("Loaded order #1");
                Console.ReadKey();

                Console.WriteLine("Changing order summary...");

                order.Summary = 1000;

                var orderEntity = db.Orders.

                await db.SaveChangesAsync();
            }
        }
    }
}
