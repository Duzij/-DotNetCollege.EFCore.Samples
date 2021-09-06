using DotNetCollege.EFCore.Sample3.Model;
using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample3
{
    public class AppDbContext : DbContext
    {
        public string DbPath;

        public AppDbContext()
        {
            var rootAppDirectory = PathUtils.GetApplicationPathByAssembly(typeof(Program).Assembly);
            DbPath = Path.Combine(rootAppDirectory, "Sample3.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite($"Data Source={DbPath}");
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample3;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var nemcova = new Author() { Id = 1, Name = "Božena", Surname = "Němcová" };
            var babicka = new Book() { Id = 1, Name = "Babička" };
            var divaBara = new Book() { Id = 2, Name = "Divá Bára" };
            modelBuilder.Entity<Author>().HasData(nemcova);
            modelBuilder.Entity<Book>().HasData(babicka);
            modelBuilder.Entity<Book>().HasData(divaBara);

            modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook() { Id = 1, AuthorId = nemcova.Id, BookId = babicka.Id });
            modelBuilder.Entity<AuthorBook>().HasData(new AuthorBook() { Id = 2, AuthorId = nemcova.Id, BookId = divaBara.Id });

            //modelBuilder.Entity<Author>()
            //    .HasMany(a => a.Books)
            //    .WithOne(b => b.Author)
            //    .OnDelete(DeleteBehavior.Cascade);

            //EF Core 5 feature
            modelBuilder.Entity<Review>().ToTable(nameof(Reviews), t => t.ExcludeFromMigrations());

        }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}

