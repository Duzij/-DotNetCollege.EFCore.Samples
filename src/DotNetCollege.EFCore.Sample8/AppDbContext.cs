using DotNetCollege.EFCore.Sample8.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System;

namespace DotNetCollege.EFCore.Sample8
{
    public class AppDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }

        public AppDbContext()
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=DotNetCollege.EFCore.Sample8;Trusted_Connection=True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(c => {
                c.HasOne(c => c.CarDetail).WithOne().HasForeignKey<CarDetail>(cd => cd.Id);
                c.HasData(new Car()
                {
                    Id = 1,
                    Manufacturer = "Škoda",
                    Model = "Octavia"
                });

                c.OwnsOne(cd => cd.AdditionalDetails, ad =>
                {
                    ad.WithOwner().HasForeignKey("CarDetailId");
                    ad.HasData(new AdditionalDetails()
                    {
                        CarDetailId = 1,
                        DealerCode = "VW",
                        OrderCode = "123",
                        VIN = "2MHFM75V96X604427"
                    });
                });
            });

            modelBuilder.Entity<CarDetail>(cd => {
                cd.HasData(new CarDetail()
                {
                    Id = 1,
                    Fuel = FuelType.Diesel,
                    Transmission = Transmission.Manual,
                    Model = "Octavia",
                });

                cd.OwnsMany(
                oc => oc.PreviousOwners, po =>
                {
                    po.WithOwner().HasForeignKey("CarDetailId");
                    po.HasData(
                        new OwnerInfo() { CarDetailId = 1, Id = 1, Info = "Karel Vondráček" },
                        new OwnerInfo() { CarDetailId = 1, Id = 2, Info = "Pavel Novák" });

                });

                cd.OwnsOne(cd => cd.AdditionalDetails)
                    .HasData(new AdditionalDetails()
                    {
                        CarDetailId = 1,
                        DealerCode = "VW",
                        OrderCode = "123",
                        VIN = "2MHFM75V96X604427"
                    });
            });
        }

    }
}
