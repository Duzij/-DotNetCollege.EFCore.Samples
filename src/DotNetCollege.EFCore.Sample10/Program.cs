using DotNetCollege.EFCore.Sample10;
using DotNetCollege.EFCore.Sample10.Model;

using System;
using System.Linq;

namespace DotNetCollege.EFCore.Sample10
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            LazyLoadingExample();
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                var category = new Category("Milk Products", "This category is for milk only.");
                db.Add(category);
                db.Add(new Product() { Name = "Discount Milk", Category = category });
                db.Add(new Product() { Name = "Premium Milk", Category = category });

                db.SaveChanges();
            }
        }

        private static void LazyLoadingExample()
        {
            using (var db = new AppDbContext())
            {
                //One query
                foreach (var product in db.Products.ToList())
                {
                    //Another query
                    if (product.Category != null)
                    {
                        Console.WriteLine(product.Category.Name);
                    }
                    Console.WriteLine(product.Name);
                }
            }
        }


    }
}
