using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace DotNetCollege.EFCore.Sample10.Model
{
    public partial class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        private readonly AppDbContext appDbContext;

        public Category(AppDbContext appDbContext, string name, string description)
        {
            this.appDbContext = appDbContext;
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Category()
        {

        }

        public int ProductsCount => Products?.Count
           ?? appDbContext?.Set<Product>().Count(p => Id == EF.Property<int?>(p, "CategoryId"))
           ?? 0;

        public virtual ICollection<Product> Products { get; set; }
    }
}
