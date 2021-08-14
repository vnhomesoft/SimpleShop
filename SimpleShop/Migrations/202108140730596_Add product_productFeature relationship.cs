namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addproduct_productFeaturerelationship : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Products", new[] { "ProductFeature_ID" });
            //DropColumn("dbo.ProductFeatures", "ID");
            //RenameColumn(table: "dbo.ProductFeatures", name: "ProductFeature_ID", newName: "ID");
            CreateIndex("dbo.ProductFeatures", "ID");
            //DropColumn("dbo.Products", "ProductFeature_ID");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Products", "ProductFeature_ID", c => c.Long());
            DropIndex("dbo.ProductFeatures", new[] { "ID" });
            //RenameColumn(table: "dbo.ProductFeatures", name: "ID", newName: "ProductFeature_ID");
            //AddColumn("dbo.ProductFeatures", "ID", c => c.Long(nullable: false));
            //CreateIndex("dbo.Products", "ProductFeature_ID");
        }
    }
}
