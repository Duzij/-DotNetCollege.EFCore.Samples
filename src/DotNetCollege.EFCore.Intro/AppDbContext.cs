using DotNetCollege.EFCore.Intro.Model;
using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Intro
{
    public class AppDbContext : DbContext
    {
        public string DbPath;

        public AppDbContext()
        {
            var rootAppDirectory = PathUtils.GetApplicationPathByAssembly(typeof(Program).Assembly);
            DbPath = Path.Combine(rootAppDirectory, "Intro.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
