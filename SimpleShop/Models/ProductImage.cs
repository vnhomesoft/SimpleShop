namespace SimpleShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        public int ID { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        //public long? Product_ID { get; set; }

        public virtual Product Product { get; set; }
    }
}
