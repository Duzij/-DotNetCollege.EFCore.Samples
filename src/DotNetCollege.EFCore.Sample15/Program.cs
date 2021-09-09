using DotNetCollege.EFCore.Sample15;
using DotNetCollege.EFCore.Sample15.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace DotNetCollege.EFCore.Sample15
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            //SaveChangesWithoutTransaction();
            //SaveChangesInTransaction();
            //TransactionsSavePoints();
            AmbientTransaction();
        }

        private static void AmbientTransaction()
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var db = new AppDbContext())
                    {
                        db.Add(new Product() { Name = "First milk" });
                        db.SaveChanges();
                    }
                    using (var db = new AppDbContext())
                    {
                        db.Add(new Product() { Name = "Second milk" });
                        db.SaveChanges();
                    }
                    scope.Complete();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private static void TransactionsSavePoints()
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Add(new Product() { Name = "First milk" });
                        db.SaveChanges();

                        transaction.CreateSavepoint("");

                        throw new InvalidOperationException();

                        db.Add(new Product() { Name = "Third milk", });
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Do nothing and first and second milk remains
                        transaction.Commit();
                    }
                }
            }
        }

        private static void SaveChangesWithoutTransaction()
        {
            using (var db = new AppDbContext())
            {
                db.Add(new Product() { Name = "First milk" });
                db.SaveChanges();

                db.Add(new Product() { Name = "Second milk" });
                db.SaveChanges();
            }
        }

        private static void SaveChangesInTransaction()
        {
            using (var db = new AppDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Add(new Product() { Name = "First milk" });
                        db.SaveChanges();

                        db.Add(new Product() { Name = "Second milk" });
                        db.SaveChanges();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private static void Init()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }


    }
}
