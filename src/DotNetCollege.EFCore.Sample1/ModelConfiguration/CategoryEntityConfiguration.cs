using DotNetCollege.EFCore.Sample1.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCollege.EFCore.Sample1.ModelConfiguration
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                    .Property(category => category.Name)
                    .HasColumnType("nvarchar(100)");
        }
    }
}
