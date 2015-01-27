namespace RestaurantReviews.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StateId = c.Int(),
                        Name = c.String(),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.States", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abbr = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CityId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ReviewerId = c.Long(),
                        RestaurantId = c.Long(),
                        Rating = c.Single(nullable: false),
                        Description = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.RestaurantId)
                .ForeignKey("dbo.Users", t => t.ReviewerId)
                .Index(t => t.ReviewerId)
                .Index(t => t.RestaurantId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "ReviewerId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "RestaurantId", "dbo.Restaurants");
            DropForeignKey("dbo.Restaurants", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Reviews", new[] { "RestaurantId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.Restaurants", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.Users");
            DropTable("dbo.Reviews");
            DropTable("dbo.Restaurants");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
        }
    }
}
