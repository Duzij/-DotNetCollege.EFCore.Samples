using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCollege.EFCore.Sample6
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            //EagerLoading();
            //ExplicitLoading();
            //ExplicitVsEagerLoading();
            Batching();
        }

        private static void Batching()
        {
            using (var db = new AppDbContext())
            {
                db.Products.Add(new Product() { Name = "New product" });
                db.Products.Add(new Product() { Name = "New product 2" });

                db.SaveChanges();
            }
        }

        private static void ExplicitVsEagerLoading()
        {
            using (var db = new AppDbContext())
            {
                var category = db.Categories
                   .OrderBy(c => c.Id)
                   .First();

                db.Entry(category)
                    .Collection(c => c.Products)
                    .Query()
                    .Count();
            }

            using (var db = new AppDbContext())
            {
                db.Categories
                   .Include(c => c.Products)
                   .Count();
            }
        }


        private static void ExplicitLoading()
        {
            using (var db = new AppDbContext())
            {
                var category = db.Categories
                    .OrderBy(b => b.Id)
                    .First();

                db.Entry(category)  
                    .Collection(p => p.Products)
                    .Load();


                var tag = db.Tags
                    .OrderBy(b => b.Id)
                    .First();

                db.Entry(tag)
                  .Reference(p => p.Category)
                  .Load();
            }
        }

        private static void EagerLoading()
        {
            using (var db = new AppDbContext())
            {
                Console.WriteLine("Querying");

                var product = db.Products
                    .Include(p => p.Category)
                    .ThenInclude(c => c.Tags)
                    .OrderBy(b => b.Id)
                    .ToList();

                /*
                Old EF:
                db.Products
                   .Include(p => p.Category.CategoryImages.Select(i => i.Tags))

                EF Core:
                db.Products
                    .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryImages)
                    .ThenInclude(i => i.Tags)
                */
            }
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                //Tag Id is not generated 
                var milkCategory = new Category
                {
                    Name = "Milk Products",
                    Tags = new List<Tag>() {
                        new Tag() {
                            Id = Guid.NewGuid(),
                            Name = "Milk Tag"
                        },
                        new Tag() {
                            Id = Guid.NewGuid(),
                            Name = "Cream Tag"
                        },
                        new Tag() {
                            Id = Guid.NewGuid(),
                            Name = "Yoghurt Tag"
                        },
                        new Tag() {
                            Id = Guid.NewGuid(),
                            Name = "Sour Cream Tag"
                        }
                    }
                };

                db.Add(milkCategory);
                db.Add(new Product { Name = "Milk", Category = milkCategory });
                db.SaveChanges();
            }
        }
    }
}
