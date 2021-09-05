using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCollege.EFCore.Sample9.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}