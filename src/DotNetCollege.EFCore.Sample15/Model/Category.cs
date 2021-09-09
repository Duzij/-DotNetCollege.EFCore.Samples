using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample15.Model
{
    public partial class Category
    {

        public int Id { get; set; }
        public  string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
