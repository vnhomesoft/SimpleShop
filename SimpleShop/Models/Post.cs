namespace SimpleShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	using System.Web.Mvc;

	public partial class Post
    {
        public long ID { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(300)]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Content { get; set; }

        [StringLength(250)]
        public string FeaturedImage { get; set; }

        [StringLength(500)]
        public string Excerpt { get; set; }

        public int PostType { get; set; }

        public int Status { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
