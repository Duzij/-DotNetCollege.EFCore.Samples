using DotNetCollege.EFCore.Sample13.BL.DTO;
using DotNetCollege.EFCore.Sample13.DAL;
using DotNetCollege.EFCore.Sample13.DAL.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample13.BL.Services
{
    public class CategoryFacade
    {
        private readonly IDbContextFactory<AppDbContext> dbContextFactory;
        private readonly ProductRepository productRepository;

        public CategoryFacade(IDbContextFactory<AppDbContext> dbContextFactory, ProductRepository productRepository)
        {
            this.dbContextFactory = dbContextFactory;
            this.productRepository = productRepository;
        }


        public List<CategoryDTO> GetCategories()
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var list = db.Categories
                    .Include(c => c.Products)
                    .ToList();

                return db.Categories
                    .Include(c => c.Products)
                    .ToList()
                    .Select(c => new CategoryDTO()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Products = c.Products.Select(p => productRepository.MapProductToDTO(p)).ToList()
                    }).ToList();
            }
        }

        public void AlterProductsInCategory(CategoryDTO category)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var dbCategory = db.Categories
                    .AsTracking()
                    .Include(c => c.Products)
                    .Single(c => c.Id == category.Id);

                dbCategory.Name += "_edit";

                //altering
                category.Products.RemoveAt(0);

                dbCategory.Products = category.Products
                    .Select(p => productRepository.MapProductToEntity(p,category.Id))
                    .ToList();

                db.SaveChanges();
            }
        }
    }
}
