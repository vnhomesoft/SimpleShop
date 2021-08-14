namespace SimpleShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascadedeleteproductfeature : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductFeatures", "ID", "dbo.Products");
            AddForeignKey("dbo.ProductFeatures", "ID", "dbo.Products", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFeatures", "ID", "dbo.Products");
            AddForeignKey("dbo.ProductFeatures", "ID", "dbo.Products", "ID");
        }
    }
}
