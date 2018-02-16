namespace RestaurantReviews.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStatesTable : DbMigration
    {
        public override void Up()
        {
            SqlFile("../../Scripts/InsertStates.sql");
        }
        
        public override void Down()
        {
        }
    }
}
