using FluentMigrator;

namespace RestaurantReviews.Api.Migrations
{
    [Migration(201901261145)]
    public class AddRestaurantTable : Migration
    {
        public override void Up()
        {
            Create.Table("Restaurant")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Name").AsString(200)
                .WithColumn("Description").AsString(2000)
                .WithColumn("City").AsString(50)
                .WithColumn("State").AsString(2);
        }

        public override void Down()
        {
            Delete.Table("Restaurant");
        }
    }
}