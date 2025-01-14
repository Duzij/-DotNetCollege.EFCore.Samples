﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace DotNetCollege.EFCore.Sample14.Model
{

    [Index(nameof(Name), nameof(Description), Name = "NameDescriptionIndex")]
    public partial class Category
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
