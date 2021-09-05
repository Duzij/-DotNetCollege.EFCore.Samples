using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample5.Model
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }

    }

    public class DiscountProduct : Product
    {
        public decimal Discount { get; set; }
    }

    public class PremiumProduct : Product
    {
        public string Note { get; set; }
    }
}
