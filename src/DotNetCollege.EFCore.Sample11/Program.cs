using DotNetCollege.EFCore.Sample11;
using DotNetCollege.EFCore.Sample11.Model;
using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample11
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Init();
            RestrictedClientEvaluation();

            await AsyncEnumberableAsync();
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                for (int i = 0; i < 1000; i++)
                {
                    db.Add(new Product() { Name = "Milk", Category = new Category() { Name = "Milk Products" } });
                }

                db.SaveChanges();
            }
        }

        private static void RestrictedClientEvaluation()
        {
            using (var db = new AppDbContext())
            {
                var query = db.Products
                    .AsEnumerable()
                        .Where(p => StringUtils.GetFirstWord(p.Name) == "Milk")
                        .ToList();
            }

            using (var db = new AppDbContext())
            {
                var query = db.Products
                    .Where(a => a.Name == "Milk")
                    .Select(p => StringUtils.MultiplyStringInput        (p.Name, 3));
            }

        }


        private static async Task AsyncEnumberableAsync()
        {
            using (var db = new AppDbContext())
            {
                foreach (var product in db.Products.AsEnumerable())
                {
                    Console.WriteLine(product);
                }

                await foreach (var product in db.Products.AsAsyncEnumerable())
                {
                    Console.WriteLine(product);
                }
            }
        }

    }
}
