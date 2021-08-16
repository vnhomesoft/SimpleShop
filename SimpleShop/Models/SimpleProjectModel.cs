using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SimpleShop.Models
{
	public partial class SimpleProjectModel : DbContext
	{
		public SimpleProjectModel()
			: base("name=SimpleProjectModel")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<ProductFeature> ProductFeatures { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductFeature>()
				.Property(e => e.Weight)
				.HasPrecision(10, 0);

			modelBuilder.Entity<ProductFeature>()
				.HasRequired(pf => pf.Product)
				.WithOptional(p => p.ProductFeature)
				.WillCascadeOnDelete(true)
				;

			modelBuilder.Entity<Product>()
				.Property(e => e.Price)
				.HasPrecision(18, 0);

			//modelBuilder.Entity<Product>()
			//	.HasMany(e => e.ProductImages)
			//	.WithOptional(e => e.Product)
			//	.HasForeignKey(e => e.Product_ID);

		}
	}
}
