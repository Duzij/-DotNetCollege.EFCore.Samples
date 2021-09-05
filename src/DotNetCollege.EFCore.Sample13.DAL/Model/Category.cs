using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample13.DAL.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TenantId { get; set; }
        public List<Product> Products { get; set; }
    }
}
