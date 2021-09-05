using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample6
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
