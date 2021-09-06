using DotNetCollege.EFCore.Sample2.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetCollege.EFCore.Sample2
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample2;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(b => b.ComputedOnServerProperty)
                .HasComputedColumnSql("isnull(N'Name is '+CONVERT([nvarchar](200),LEN(Name)) + ' chars long',N'*** ERROR ***')");

            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Category)
            //    .WithMany(c => c.P)
            //    .OnDelete(DeleteBehavior.)
        }

        public DbSet<Product> Products { get; set; }
    }
}
