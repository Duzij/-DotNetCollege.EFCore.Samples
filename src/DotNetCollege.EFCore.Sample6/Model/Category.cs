using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample6
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
            Tags = new HashSet<Tag>();
        }

        public short Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
