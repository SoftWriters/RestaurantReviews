namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewSchemaAdditions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Stars = c.Int(nullable: false),
                        Comments = c.String(maxLength: 1000),
                        Author_Id = c.Long(nullable: false),
                        Restaurant_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Restaurant_Id", "dbo.Restaurants");
            DropForeignKey("dbo.Reviews", "Author_Id", "dbo.Users");
            DropIndex("dbo.Reviews", new[] { "Restaurant_Id" });
            DropIndex("dbo.Reviews", new[] { "Author_Id" });
            DropTable("dbo.Reviews");
        }
    }
}
