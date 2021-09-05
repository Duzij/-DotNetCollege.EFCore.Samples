using DotNetCollege.EFCore.Sample14;
using DotNetCollege.EFCore.Sample14.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample14
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            AddPropertiesToPropertyBag();
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                for (int i = 0; i < 100; i++)
                {
                    db.Add(new Product() { Name = "Milk", Category = new Category() { Name = "Milk Products" } });
                }

                db.SaveChanges();
            }
        }


        private static void AddPropertiesToPropertyBag()
        {
            using (var db = new AppDbContext())
            {
                var now = DateTime.UtcNow;

                var dic = new Dictionary<string, object>()
                {
                    ["LastUpdated"] = now,
                    ["Property"] = "Test",
                };

                db.PropertyBag.Add(dic);

                var dic2 = new Dictionary<string, object>()
                {
                    ["MissingProperty"] = "",
                    ["LastUpdated"] = now,
                    ["Property"] = "Test2",
                };

                db.PropertyBag.Add(dic);
                db.PropertyBag.Add(dic2);

                db.SaveChanges();
            }

            using (var db = new AppDbContext())
            {
                var addedBag = db.PropertyBag.Find(1);

                Console.WriteLine(addedBag["Id"]);
                Console.WriteLine(addedBag["Property"]);
                Console.WriteLine(addedBag["LastUpdated"]);

                var addedBag2 = db.PropertyBag.Find(2);

                Console.WriteLine(addedBag2["Id"]);
                Console.WriteLine(addedBag2["Property"]);
                Console.WriteLine(addedBag2["LastUpdated"]);

                db.SaveChanges();
            }
        }
     

    }
}
