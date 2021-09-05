using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }

            await QueueTableSplittedCar();

        }


        public static async Task QueueTableSplittedCar()
        {
            using (var db = new AppDbContext())
            {
                var car = await db.Cars.FindAsync(1);
                Console.ReadKey();
            }

            using (var db = new AppDbContext())
            {
                //Inner join
                var carDetail = await db.CarDetails.FindAsync(1);
                Console.ReadKey();
            }

            using (var db = new AppDbContext())
            {
                //Left join
                var car = await db.Cars
                    .Include(c => c.CarDetail)
                    .FirstOrDefaultAsync(c => c.Id == 1);
                Console.ReadKey();
            }
        }
    }
}
