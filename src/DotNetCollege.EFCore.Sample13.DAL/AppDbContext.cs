using DotNetCollege.EFCore.Sample13.DAL.Model;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

using System;
using System.Security.Claims;

namespace DotNetCollege.EFCore.Sample13.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        private readonly CustomDbContextOptions options;
        protected readonly IHttpContextAccessor httpContextAccessor;

        private string UserId => httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "not-logged-in";

        public AppDbContext(DbContextOptions<AppDbContext> dbOptions, IOptions<CustomDbContextOptions> options, IHttpContextAccessor httpContextAccessor)
        : base(dbOptions)
        {
            this.options = options.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var milkProducts = new Category() { Id = 1, Name = "Milk Products", TenantId = "79260c45-f55e-40c1-b684-683f2677aea1" };
            var moreMilkProducts = new Category() { Id = 2, Name = "More Milk Products", TenantId = "eb885604-4834-4f5e-9944-a2995d047061" };

            builder.Entity<Category>(c => {
                c.HasData(milkProducts, moreMilkProducts);

                c.HasQueryFilter(p => p.TenantId == options.TenantId);
            });

            builder.Entity<Product>(p =>
            {
                p.HasData(
                    new Product { Id = 1, Name = this.UserId + "_Milk", CategoryId = milkProducts.Id },
                    new Product { Id = 2, Name = this.UserId + "_Other Milk", CategoryId = milkProducts.Id }
                    );

                p.HasQueryFilter(p => p.UserId == this.UserId);
            });

            base.OnModelCreating(builder);
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }


    }

}
