using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DotNetCollege.EFCore.Sample7
{

    //TPH - Table per hierarchy

    [Table("Products")]
    public class ProductBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }

    public class DiscountProduct : ProductBase
    {
        public decimal Discount { get; set; }
    }

    public class PremiumProduct : ProductBase
    {
        public string Note { get; set; }
    }
}
