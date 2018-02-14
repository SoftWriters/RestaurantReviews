namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplyUniqueIndexToUsername : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Users", "Username", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Username" });
        }
    }
}
