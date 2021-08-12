namespace SimpleShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public long ID { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Branch { get; set; }

        public long? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ProductFeature ProductFeature { get; set; }

        // TODO: add virtual property
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
