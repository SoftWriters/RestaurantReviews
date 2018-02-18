namespace RestaurantRating.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblRestaurant",
                c => new
                    {
                        RestaurantName = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.RestaurantName);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfRest = c.String(nullable: false, maxLength: 40),
                        UserName = c.String(nullable: false, maxLength: 40),
                        Reviews = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        RestaurantName = c.Int(nullable: false),
                        Id = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.RestaurantName, t.Id })
                .ForeignKey("dbo.tblUsers", t => t.RestaurantName, cascadeDelete: true)
                .ForeignKey("dbo.tblRestaurant", t => t.Id, cascadeDelete: true)
                .Index(t => t.RestaurantName)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "RestaurantName" });
            DropForeignKey("dbo.Users", "Id", "dbo.tblRestaurant");
            DropForeignKey("dbo.Users", "RestaurantName", "dbo.tblUsers");
            DropTable("dbo.Users");
            DropTable("dbo.tblUsers");
            DropTable("dbo.tblRestaurant");
        }
    }
}
