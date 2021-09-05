namespace DotNetCollege.EFCore.Sample5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DotNetCollege.EFCore.Sample5.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DotNetCollege.EFCore.Sample5.AppDbContext";
        }

        protected override void Seed(DotNetCollege.EFCore.Sample5.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Products.AddOrUpdate(new Model.Product() { Name = "Milk", Category = new Model.Category() { Name = "Milk Products" } });
        }
    }
}
