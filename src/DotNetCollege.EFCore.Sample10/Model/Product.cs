using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DotNetCollege.EFCore.Sample10.Model
{
    public partial class Product
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public ProductStatus Status { get; set; }

        public virtual Category Category { get; set; }
    }

    public enum ProductStatus
    {
        InStock,
        Sold
    }
}
