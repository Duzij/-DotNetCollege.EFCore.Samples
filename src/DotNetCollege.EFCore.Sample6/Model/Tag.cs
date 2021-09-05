using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DotNetCollege.EFCore.Sample6
{
    public partial class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
