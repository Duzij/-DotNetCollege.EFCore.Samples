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

            //FromSqlInterpolated();
        }


        private static void IncludeInsteadOfSelect()
        {
            using (var db = new AppDbContext())
            {
                var usedCategories = db.Products
                    .Include(p => p.Categories)
                    .ThenInclude(c => c.Tags)
                    .ToList();

                foreach (var product in usedCategories)
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
                    .AsNoTracking()
                    .ToList();
            }
        }

        private static void FilteredIncludes()
        {
            using (var db = new AppDbContext())
            {
                var milkCategories = db.Categories
                    .AsNoTracking()
                    .Include(c => c.Products.Where(p => p.Name.StartsWith("Milk")))
                    .ToList();

                var categoriesWithOneProduct = db.Categories
                    .AsNoTracking()
                    .Include(c => c.Products
                        .OrderBy(p => p.Name)
                        .ThenBy(p => p.Name)
                        .Take(1))
                    .ToList();
            }
        }


        private static void SplitQuery()
        {
            using (var db = new AppDbContext())
            {
                var products = db.Products
                    //.AsNoTracking()
                    .ToList();

                var categories = db.Categories
                    //.AsNoTracking()
                    .ToList();
            }

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

                var tag = new Tag() { Name = "Tag1" };
                var tag2 = new Tag() { Name = "Tag2" };

                var milkProductCategory = new Category() { Name = "Milk Products", Tags = new List<Tag> { tag, tag2 } };
                var milkProductCategory2 = new Category() { Name = "More Milk Products", Tags = new List<Tag> { tag } };

                db.Add(milkProductCategory);
                db.Add(milkProductCategory2);

                db.Add(new Product() { Name = "Milk", Categories = new List<Category> { milkProductCategory, milkProductCategory2 } });
                db.Add(new Product() { Name = "Milk", Categories = new List<Category> { milkProductCategory2 } });
                db.Add(new Product() { Name = "Milk", Categories = new List<Category> { milkProductCategory } });
                db.Add(new Product() { Name = "Milk", Categories = new List<Category> { milkProductCategory2 } });
                db.Add(new Product() { Name = "Milk", Categories = new List<Category> { milkProductCategory } });


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
                    .FromSqlInterpolated($"SELECT * FROM dbo.Products where Id > {identifierAsString}")
                    .OrderBy(b => b.Name)
                    .ToList();

                //https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.relationalqueryableextensions.fromsqlinterpolated?view=efcore-5.0

            }
        }

    }
}
