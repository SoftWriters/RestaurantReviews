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

        /// <summary>
        /// Comma separated string of the SQL table name and column namess for convenience in SQL queries.
        /// Avoids the needs for "SELECT *", which may have unintended side effects (e.g. column name conflicts in a JOIN statement).
        /// Provides a bit of encapsulation and convenient reusability.
        /// </summary>
        public static string FullyQualifiedTableProperties =
            $"{TableName}.{nameof(Id)}," +
            $" {TableName}.{nameof(UniqueId)}," +
            $" {TableName}.{nameof(Date)}," +
            $" {TableName}.{nameof(FiveStarRating)}," +
            $" {TableName}.{nameof(RestaurantId)}," +
            $" {TableName}.{nameof(ReviewerId)}," +
            $" {TableName}.{nameof(ReviewText)}";

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
