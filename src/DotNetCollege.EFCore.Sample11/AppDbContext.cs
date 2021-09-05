using DotNetCollege.EFCore.Sample11.Interceptors;
using DotNetCollege.EFCore.Sample11.Model;

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

namespace DotNetCollege.EFCore.Sample11
{
    public class AppDbContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.AddInterceptors(
                new EFConnectionInterceptor(),
                new EFTransactionInterceptor(),
                new EFCommandInterceptor(),
                new EFSaveChangesInterceptor()
           );

            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample11;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
