using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DotNetCollege.EFCore.Sample13.DAL.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
