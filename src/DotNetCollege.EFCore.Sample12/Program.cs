using DotNetCollege.EFCore.Sample12;
using DotNetCollege.EFCore.Sample12.Model;
using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample12
{
    class Program
    {
        static void Main(string[] args)
        {
            InitNotWorthSplitQuery();
            //InitWorthSplitQuery();

            //SplitQuery();
            //FilteredIncludes();

            //ToFunctionAndToSqlQuery();

            IncludeInsteadOfSelect();

            FromSqlInterpolated();
        }


        private static void IncludeInsteadOfSelect()
        {
            using (var db = new AppDbContext())
            {
                var products = db.Products
                    .AsNoTracking()
                    .Include(p => p.Categories)
                    .ThenInclude(c => c.Tags)
                    .ToList();

                foreach (var product in products)
                {
                    foreach (var category in product.Categories)
                    {
                        foreach (var tag in category.Tags)
                        {
                            Console.WriteLine(product.Name + " " + category.Name + " " + tag.Name);
                        }
                    }
                }
            }

            using (var db = new AppDbContext())
            {
                var query = db.Products
                    .Select(p => new
                    {
                        p.Name,
                        Categories = p.Categories.Select(c => new
                        {
                            c.Name,
                            Tags = c.Tags.Select(t => new
                            {
                                t.Name
                            })
                        })
                    });

                foreach (var product in query)
                {
                    foreach (var category in product.Categories)
                    {
                        foreach (var tag in category.Tags)
                        {
                            Console.WriteLine(product.Name + " " + category.Name + " " + tag.Name);
                        }
                    }
                }
            }
        }

        private static void ToFunctionAndToSqlQuery()
        {
            //uncomment modelBuilder configuration
            using (var db = new AppDbContext())
            {
                var products = db.Products
                    .AsTracking()
                    .ToList();
            }
        }

        private static void FilteredIncludes()
        {
            using (var db = new AppDbContext())
            {
                var milkCategories = db.Categories
                    .AsNoTracking()
                    .Include(c => c.Products.Where(p => p.Name.EndsWith("Milk")))
                    .ToList();


                var categoriesWithOneProduct = db.Categories
                    .AsNoTracking()
                    .Include(c => c.Products
                        .OrderBy(c => c.Id)
                        .Take(1))
                    .ToList();

                var orderingThroughLayers = db.Products
                    .AsNoTracking()
                    .OrderBy(p => p.Description)
                    .Include(p => p.Categories.OrderBy(c => c.Name))
                    .ThenInclude(c => c.Tags.OrderByDescending(t => t.Name))
                    .ToList();

                //Navigation fixup issue

                var nextQuery2 = db.Categories
                  .AsNoTracking()
                  .Include(c => c.Products.Where(p => p.Id < 4))
                  .ToList();

                var nextQuery = db.Categories
                    .Include(c => c.Products.Where(p => p.Id < 1))
                    .ToList();

                //stand-alone predicates only
                var productsWithSameOwner = db.Owners
                    .Include(o => o.Products.Where(p => p.Name == o.Name));
                    //.ToList();

                var productsWithSameOwnerStandAlone = db.Owners
                    .Include(o => o.Products.Where(p => p.Name == p.Owner.Name))
                    .ToList();

            }
        }


        private static void SplitQuery()
        {
            //using (var db = new AppDbContext())
            //{
            //    var products = db.Products
            //        //.AsNoTracking()
            //        .ToList();

            //    var categories = db.Categories
            //        //.AsNoTracking()
            //        .ToList();
            //}

            using (var db = new AppDbContext())
            {
                var productsWithCategories = db.Products
                    .Include(c => c.Categories)
                    .ThenInclude(p => p.Tags)
                    .AsSplitQuery()
                    .ToList();
            }

            using (var db = new AppDbContext())
            {
                var productsWithCategories2 = db.Products
                    .Include(c => c.Categories)
                    .ThenInclude(p => p.Tags)
                    .AsSingleQuery()
                    .ToList();
            }
        }

        private static void InitWorthSplitQuery()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                var tag = new Tag() { Name = StringUtils.MultiplyStringInput("Tag1", 500) };
                var tag2 = new Tag() { Name = StringUtils.MultiplyStringInput("Tag2", 500) };
                var tag3 = new Tag() { Name = StringUtils.MultiplyStringInput("Tag3", 500) };

                var milkProductCategory = new Category()
                {
                    Name = StringUtils.MultiplyStringInput("Milk Products", 500),
                    Tags = new List<Tag> { tag, tag2, tag3 }
                };
                var milkProductCategory2 = new Category()
                {
                    Name = StringUtils.MultiplyStringInput("More Milk Products", 500),
                    Tags = new List<Tag> { tag, tag2, tag3 }
                };
                var milkProductCategory3 = new Category()
                {
                    Name = StringUtils.MultiplyStringInput("Even More Milk Products", 500),
                    Tags = new List<Tag> { tag, tag2, tag3 }
                };

                db.Add(milkProductCategory);
                db.Add(milkProductCategory2);
                db.Add(milkProductCategory3);

                for (int i = 0; i < 100; i++)
                {
                    db.Add(new Product() { Name = "Milk", Description = StringUtils.MultiplyStringInput("Description", 100), Categories = new List<Category> { milkProductCategory, milkProductCategory2, milkProductCategory3 } });
                }

                db.SaveChanges();
            }
        }

        private static void InitNotWorthSplitQuery()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Console.WriteLine("Inserting");

                var tag = new Tag() { Name = "A Tag" };
                var tag2 = new Tag() { Name = "B Tag2" };

                var milkProductCategory = new Category() { Name = "A Milk Products", Tags = new List<Tag> { tag, tag2 } };
                var milkProductCategory2 = new Category() { Name = "B Milk Products", Tags = new List<Tag> { tag, tag2 } };
                var milkProductCategory3 = new Category() { Name = "Other Products", Tags = new List<Tag> { tag, tag2 } };

                db.Add(milkProductCategory);
                db.Add(milkProductCategory2);
                db.Add(milkProductCategory3);

                db.Add(new Product() { Name = "A Milk", Description = "A Milk description", Categories = new List<Category> { milkProductCategory, milkProductCategory2 } });
                db.Add(new Product() { Name = "B Milk", Description = "Milk description", Categories = new List<Category> { milkProductCategory2 } });
                db.Add(new Product() { Name = "C Milk", Description = "Milk description", Categories = new List<Category> { milkProductCategory } });


                db.SaveChanges();
            }
        }

        private static void FromSqlInterpolated()
        {
            using (var db = new AppDbContext())
            {
                var identifier = 10;

                var products = db.Products
                    .FromSqlInterpolated($"SELECT * FROM dbo.Products where Id > {identifier}")
                    .OrderBy(b => b.Name)
                    .ToList();

                var identifierAsString = "105; DROP TABLE [dbo].[Tags]";

                var dangerousQuery = db.Products
                    .FromSqlInterpolated($"SELECT * FROM dbo.Products where Name = {identifierAsString}")
                    .OrderBy(b => b.Name)
                    .ToList();

                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.fromsqlinterpolated?view=efcore-5.0

            }
        }

    }
}
