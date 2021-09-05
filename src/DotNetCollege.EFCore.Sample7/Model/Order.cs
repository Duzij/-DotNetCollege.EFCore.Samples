using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample7.Model
{
    public class Order
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public int Summary { get; set; }

        public byte[] Timestamp { get; set; }

        public List<ProductBase> Products { get; set; }
    }
}
