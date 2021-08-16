namespace SimpleShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	using System.Web;
	using System.Web.Mvc;
    using System.Linq;

	public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
            ProductFeature = new ProductFeature();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(20)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "ntext")]
        [AllowHtml]
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? Quantity { get; set; }

        [StringLength(50)]
        public string Branch { get; set; }

        public long? CategoryID { get; set; }

        public virtual Category Category { get; set; }

        // Không mapping field này với DB (do trong DB không có)
        [NotMapped]
        public HttpPostedFileBase UploadFile { get; set; }
        [NotMapped]
        public string FeaturedImage {
			get
			{
                var featuredImage = ProductImages.Where(it => it.IsFeatured).FirstOrDefault();
                if(ProductImages.Count == 0 || featuredImage == null) { return ""; };
                return featuredImage.ImageUrl;
			}
        }
        [NotMapped]
        public ProductImage FeaturedImageObject
		{
            get
            {
                var featuredImage = ProductImages.Where(it => it.IsFeatured).FirstOrDefault();
                if (ProductImages.Count == 0 || featuredImage == null) { return new ProductImage { ImageUrl = string.Empty}; };
                return featuredImage;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
 
        public virtual ProductFeature ProductFeature { get;set; }
    }
}
