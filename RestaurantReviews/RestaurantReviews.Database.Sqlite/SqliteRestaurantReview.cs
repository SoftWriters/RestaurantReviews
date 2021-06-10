using RestaurantReviews.Core;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    //TODO: maybe these should all be internal
    [Table(TableName)]
    public class SqliteRestaurantReview : PersistableBase, IRestaurantReview
    {
        public const string TableName = "Review";

        public SqliteRestaurantReview()
        {

        }

        public SqliteRestaurantReview(IRestaurantReview review, SqliteRestaurant restaurant, SqliteUser reviewer)
        {
            UniqueId = review.UniqueId;
            Restaurant = restaurant;
            RestaurantId = restaurant?.Id ?? 0;
            Reviewer = reviewer;
            ReviewerId = reviewer?.Id ?? 0;
            FiveStarRating = review.FiveStarRating;
            ReviewText = review.ReviewText;
            Date = review.Date;
        }

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        //ForeignKey to SqliteRestaurant.Id
        [Indexed]
        public int RestaurantId { get; set; }

        //ForeignKey to SqliteUser.Id
        public int ReviewerId { get; set; }

        [Indexed(Unique = true)]
        public Guid UniqueId { get; set; }

        [Ignore]
        public IRestaurant Restaurant { get; internal set; } //Set by the parent DB controller

        [Ignore]
        public IUser Reviewer { get; internal set; } //Set by the parent DB controller

        public int FiveStarRating { get; set; }
        
        public string ReviewText { get; set; }

        public DateTime Date { get; set; }
    }
}
