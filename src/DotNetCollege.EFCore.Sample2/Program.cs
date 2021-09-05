using DotNetCollege.EFCore.Sample2.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;

namespace DotNetCollege.EFCore.Sample2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");
                db.Add(new Product { Name = "Milk", Category = new Category() { Name = "Milk Products" } } );
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying");
                var product = db.Products
                    .Include(a => a.Category)
                    .OrderBy(b => b.Id)
                    .ToList();
            }
        }
    }
}
