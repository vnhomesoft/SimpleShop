namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetproductPKidentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductImages", "Product_ID", "dbo.Products");
            DropIndex("dbo.Products", new[] { "ID" });
            DropPrimaryKey("dbo.Products", "PK_Products");
            AlterColumn("dbo.Products", "ID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Products", "ID");
            CreateIndex("dbo.Products", "ID");
            AddForeignKey("dbo.ProductImages", "Product_ID", "dbo.Products", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "Product_ID", "dbo.Products");
            DropIndex("dbo.Products", new[] { "ID" });
            DropPrimaryKey("dbo.Products", "PK_Products");
            AlterColumn("dbo.Products", "ID", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Products", "ID");
            CreateIndex("dbo.Products", "ID");
            AddForeignKey("dbo.ProductImages", "Product_ID", "dbo.Products", "ID");
        }
    }
}
