using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCollege.EFCore.Sample2.Model
{
    [Table("BasicProduct", Schema = "Product")]
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        //Not be included in INSERT statements
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        //Not included the property in INSERT or UPDATE
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ComputedOnServerProperty { get; set; }

        [NotMapped]
        public string ComputedOnClientProperty => Created.ToLongTimeString();

        [Column("ProductDescription", Order = 1, TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Category Category { get; set; }

    }
}
