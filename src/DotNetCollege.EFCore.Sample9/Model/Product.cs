using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCollege.EFCore.Sample9.Model
{
    public class Product
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; }

        public Category Category { get; set; }
    }
}
