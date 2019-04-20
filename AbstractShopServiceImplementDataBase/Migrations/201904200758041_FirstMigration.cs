namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SClientId = c.Int(nullable: false),
                        CookId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cooks", t => t.CookId, cascadeDelete: true)
                .ForeignKey("dbo.SClients", t => t.SClientId, cascadeDelete: true)
                .Index(t => t.SClientId)
                .Index(t => t.CookId);
            
            CreateTable(
                "dbo.Cooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CookName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CookIngridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CookId = c.Int(nullable: false),
                        IngridientsId = c.Int(nullable: false),
                        IngridientsName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingridients", t => t.IngridientsId, cascadeDelete: true)
                .Index(t => t.IngridientsId);
            
            CreateTable(
                "dbo.Ingridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngridientsName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockIngridients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SStockId = c.Int(nullable: false),
                        IngridientsId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingridients", t => t.IngridientsId, cascadeDelete: true)
                .ForeignKey("dbo.SStocks", t => t.SStockId, cascadeDelete: true)
                .Index(t => t.SStockId)
                .Index(t => t.IngridientsId);
            
            CreateTable(
                "dbo.SStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SStockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockIngridients", "SStockId", "dbo.SStocks");
            DropForeignKey("dbo.StockIngridients", "IngridientsId", "dbo.Ingridients");
            DropForeignKey("dbo.CookIngridients", "IngridientsId", "dbo.Ingridients");
            DropForeignKey("dbo.SOrders", "SClientId", "dbo.SClients");
            DropForeignKey("dbo.SOrders", "CookId", "dbo.Cooks");
            DropIndex("dbo.StockIngridients", new[] { "IngridientsId" });
            DropIndex("dbo.StockIngridients", new[] { "SStockId" });
            DropIndex("dbo.CookIngridients", new[] { "IngridientsId" });
            DropIndex("dbo.SOrders", new[] { "CookId" });
            DropIndex("dbo.SOrders", new[] { "SClientId" });
            DropTable("dbo.SStocks");
            DropTable("dbo.StockIngridients");
            DropTable("dbo.Ingridients");
            DropTable("dbo.CookIngridients");
            DropTable("dbo.Cooks");
            DropTable("dbo.SOrders");
            DropTable("dbo.SClients");
        }
    }
}
