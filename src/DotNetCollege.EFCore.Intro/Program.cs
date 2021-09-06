using DotNetCollege.EFCore.Intro.Model;
using System;
using System.Linq;

namespace DotNetCollege.EFCore.Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine($"Database path: {db.DbPath}.");

                Console.WriteLine("Inserting");
                db.Add(new Product { Name = "Milk" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying");
                var product = db.Products
                    .OrderBy(b => b.Id)
                    .First();

                // Update
                Console.WriteLine("Updating");
                product.Name = "Updated Milk";
                db.SaveChanges();

                // Delete
                Console.WriteLine("Delete");
                db.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
