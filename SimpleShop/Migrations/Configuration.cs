namespace SimpleShop.Migrations
{
	using SimpleShop.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleShop.Models.SimpleProjectModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SimpleShop.Models.SimpleProjectModel context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//Console.WriteLine("Begin seed");
			//context.Users.Add(
			//	   new User
			//	   {
			//		   LoginName = "admin",
			//		   Email = "admin@example.net",
			//		   Password = "admin"
			//	   });
			//context.SaveChanges();
			//Console.WriteLine("Create user");

			//// Create default categories
			//context.Categories.AddOrUpdate(
			//	c => c.ID,
			//	new Category
			//	{
			//		ID = 1,
			//		CategoryName = "Uncategorized"
			//	});
			//context.SaveChanges();
			//Console.WriteLine("Create category");

			//// Create default page
			//var posts = new List<Post>
			//	{
			//		new Post
			//		{
			//			Name = "about",
			//			Title = "About",
			//			Content = "<strong>Simple shop provide you best products</strong>",
			//			PublishDate = DateTime.Now,
			//			Status = 1
			//		},
			//		new Post
			//		{
			//			Name =  "contact",
			//			Title = "Contact",
			//			Content = "Email: sell@example.net",
			//			PublishDate = DateTime.Now,
			//			Status = 1
			//		}
			//	};
			//posts.ForEach(post =>
			//	context.Posts.AddOrUpdate(
			//		p => p.Name,
			//		post
			//	)
			//);
			//context.SaveChanges();
			//Console.WriteLine("Create post");

			//// Create sample products
			//var products = new List<Product>
			//	{
			//		new Product
			//		{
			//			ProductCode = "P001",
			//			ProductName = "Product 1",
			//			CategoryID = 1,
			//			Description = "<p>Description of product 1</p>",
   //                     //FeatureImage = "~/Upload/Product/sample-product-image.png",
   //                     Price = 100000
			//		},
			//		new Product
			//		{
			//			ProductCode = "P002",
			//			ProductName = "Product 2",
			//			CategoryID = 1,
			//			Description = "<p>Description of product 2</p>",
   //                     //FeatureImage = "~/Upload/Product/sample-product-image.png",
   //                     Price = 150000
			//		}
			//	};
			//products.ForEach(product =>
			//	context.Products.AddOrUpdate(p => p.ProductCode, product));
			//context.SaveChanges();
			//Console.WriteLine("Create products");
		}
    }
}
