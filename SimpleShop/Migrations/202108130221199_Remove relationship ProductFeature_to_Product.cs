namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoverelationshipProductFeature_to_Product : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ID", "dbo.ProductFeatures");
            DropIndex("dbo.Products", new[] { "ID" });
            AddColumn("dbo.Products", "ProductFeature_ID", c => c.Long());
            CreateIndex("dbo.Products", "ProductFeature_ID");
            AddForeignKey("dbo.Products", "ProductFeature_ID", "dbo.ProductFeatures", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductFeature_ID", "dbo.ProductFeatures");
            DropIndex("dbo.Products", new[] { "ProductFeature_ID" });
            DropColumn("dbo.Products", "ProductFeature_ID");
            CreateIndex("dbo.Products", "ID");
            AddForeignKey("dbo.Products", "ID", "dbo.ProductFeatures", "ID");
        }
    }
}
