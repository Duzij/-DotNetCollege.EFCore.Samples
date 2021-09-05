using DotNetCollege.EFCore.Sample4.Model;
using DotNetCollege.EFCore.Utils;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample4
{
    public class AppDbContext : DbContext
    {
        public string DbPath;

        public AppDbContext()
        {
            var rootAppDirectory = PathUtils.GetApplicationPathByAssembly(typeof(Program).Assembly);
            DbPath = Path.Combine(rootAppDirectory, "Sample1.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample4;Trusted_Connection=True;");
            //optionsBuilder.UseSqlite(DbPath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
