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
    public class ProductRepository
    {
        private IDbContextFactory<AppDbContext> dbContextFactory;
        private string userId;

        public ProductRepository(IDbContextFactory<AppDbContext> dbContextFactory, IHttpContextAccessor contextAccessor)
        {
            this.dbContextFactory = dbContextFactory;
            userId = contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public void AddProductForCategory(ProductDTO productDTO, int categoryId)
        {
            using (var db = dbContextFactory.CreateDbContext())
            {
                var productEntity = MapProductToEntity(productDTO, categoryId);
                db.Products.Add(productEntity);
                db.SaveChanges();
            }
        }

        public Product MapProductToEntity(ProductDTO productDTO, int categoryId)
        {
            var productEntity = new Product();
            //productEntity.Id = productDTO.Id;
            productEntity.Name = productDTO.Name;
            productEntity.CategoryId = categoryId;
            productEntity.UserId = userId;
            return productEntity;
        }

        public void Sync(List<Product> entityProducts, List<ProductDTO> dtoProducts)
        {
            //get new added,
            //get removed,
            //get updated

            //https://github.com/riganti/infrastructure/blob/master/src/Infrastructure/Riganti.Utils.Infrastructure.AutoMapper/SyncByKeyCollectionResolver.cs
        }

        public ProductDTO MapProductToDTO(Product entity)
        {
            var productDTO = new ProductDTO();
            productDTO.Name = entity.Name;
            productDTO.Id = entity.Id;
            return productDTO;
        }
    }
}
