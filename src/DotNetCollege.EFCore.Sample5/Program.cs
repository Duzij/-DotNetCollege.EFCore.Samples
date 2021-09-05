using DotNetCollege.EFCore.Sample5.Model;
using DotNetCollege.EFCore.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample5
{
    class Program
    {
        static void Main(string[] args)
        {
            Inheritance();
            //LazyLoadingExample();
            //ClientEvaluation();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void Inheritance()
        {
            using (var db = new AppDbContext())
            {
                db.DiscountProducts.Add(new DiscountProduct() { Name = "Discounted Milk", Discount = 5 });
                db.PremiumProducts.Add(new PremiumProduct() { Name = "Premium Milk",  Note = "Very fresh!" });
                db.SaveChanges();
            }

            using (var db = new AppDbContext())
            {
                var discountedProducts = db.Products.OfType<DiscountProduct>().ToList();
                var allProducts = db.Products.ToList();
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

        private static void ClientEvaluation()
        {
            using (var db = new AppDbContext())
            {
                var productList = db.Products
                        .Where(p => StringUtils.GetFirstWord(p.Name) == "Test")
                        .ToList();
            }
        }
    }
}
