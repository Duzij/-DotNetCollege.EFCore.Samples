using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCollege.EFCore.Sample2.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> P { get; set; }

    }
}