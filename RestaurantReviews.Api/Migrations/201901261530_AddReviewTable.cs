using FluentMigrator;

namespace RestaurantReviews.Api.Migrations
{
    [Migration(201901261530)]
    public class AddReviewTable : Migration
    {
        public override void Up()
        {
            Create.Table("Review")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("RestaurantId").AsInt64()
                .WithColumn("ReviewerEmail").AsString(300)
                .WithColumn("RatingStars").AsDecimal(3,1)
                .WithColumn("Comments").AsString(4000)
                .WithColumn("ReviewedOn").AsDateTimeOffset();
            
            Create.ForeignKey("fk_Review_RestaurantId_Restaurant_Id")
                .FromTable("Review").ForeignColumn("RestaurantId")
                .ToTable("Restaurant").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table("Review");
        }
    }
}