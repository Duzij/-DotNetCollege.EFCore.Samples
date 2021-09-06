using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample3
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            DeleteAuthor();
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();

                if (db.Database.GetMigrations().Any())
                {
                    db.Database.Migrate();
                    Console.WriteLine("Migrated");
                }
                else
                {
                    db.Database.EnsureCreated();
                    Console.WriteLine("Created");
                }

            }
        }

        private static void DeleteAuthor()
        {
            using (var db = new AppDbContext())
            {
                var nemcova = db.Authors
                    //.Include(a => a.Books)
                    //.ThenInclude(ab => ab.Book)
                    .FirstOrDefault(a => a.Id == 1);
                db.Remove(nemcova);

                //var nemcovaBooks = nemcova.Books.Select(a => a.Book);
                //foreach (var book in nemcovaBooks)
                //{
                //    db.Remove(book);
                //}

                db.SaveChanges();
                Console.WriteLine("Nemcova entity removed");
            }
        }

    }
}
