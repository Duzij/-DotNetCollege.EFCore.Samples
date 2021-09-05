using DotNetCollege.EFCore.Sample14.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample14
{
    public static class StaticNames
    {
        public static string PropertyBagTableName = "PropertyBagTable";
    }
    public class AppDbContext : DbContext
    {

        //EF Core 5 feature
        public DbSet<Dictionary<string, object>> PropertyBag => Set<Dictionary<string, object>>(StaticNames.PropertyBagTableName);

        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample14;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SharedTypeEntity<Dictionary<string, object>>(
            StaticNames.PropertyBagTableName, bb =>
            {
                bb.Property<int>("Id");
                bb.Property<string>("Property");
                bb.Property<DateTime>("LastUpdated");
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
