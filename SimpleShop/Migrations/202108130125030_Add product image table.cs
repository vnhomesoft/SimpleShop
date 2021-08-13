namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addproductimagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        IsFeatured = c.Boolean(nullable: false),
                        Product_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.Product_ID)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "Product_ID", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_ID" });
            DropTable("dbo.ProductImages");
        }
    }
}
