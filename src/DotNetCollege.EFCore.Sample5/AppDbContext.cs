using DotNetCollege.EFCore.Sample5.Migrations;
using DotNetCollege.EFCore.Sample5.Model;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample5
{
    public class AppDbContext : DbContext
    {
        public DbSet<DiscountProduct> DiscountProducts { get; set; }
        public DbSet<PremiumProduct> PremiumProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public AppDbContext() : base("name=DB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<AppDbContext>());
            //Database.SetInitializer(new CustomDbInitializer());
        }
    }
}
