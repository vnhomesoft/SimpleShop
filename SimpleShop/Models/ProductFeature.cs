namespace SimpleShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductFeature
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ID { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        public virtual Product Product { get; set; }
    }
}
