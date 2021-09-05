using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample12.Model
{
    public class Category
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public List<Product> Products { get; set; }

        public List<Tag> Tags { get; set; }

    }
}
