using Newtonsoft.Json;
using RestaurantReviews.Core.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite.Entities
{
    /// <summary>
    /// Sqlite db implementation of IRestaurantReview
    /// </summary>
    [Table(TableName)]
    internal class SqliteRestaurantReview : PersistableBase, IRestaurantReview
    {
        public const string TableName = "Review";

        public SqliteRestaurantReview()
        {

        }

        public SqliteRestaurantReview(IRestaurantReview review, SqliteUser reviewer)
        {
            UniqueId = review.UniqueId;
            RestaurantUniqueId = review.RestaurantUniqueId;
            Reviewer = reviewer;
            ReviewerId = reviewer?.Id ?? 0;
            FiveStarRating = review.FiveStarRating;
            ReviewText = review.ReviewText;
            Timestamp = review.Timestamp;
        }

        /// <summary>
        /// Comma separated string of the SQL table name and column namess for convenience in SQL queries.
        /// Avoids the needs for "SELECT *", which may have unintended side effects (e.g. column name conflicts in a JOIN statement).
        /// Provides a bit of encapsulation and convenient reusability.
        /// </summary>
        public static string FullyQualifiedTableProperties =
            $"{TableName}.{nameof(Id)}," +
            $" {TableName}.{nameof(UniqueId)}," +
            $" {TableName}.{nameof(RestaurantUniqueId)}," +
            $" {TableName}.{nameof(ReviewerId)}," +
            $" {TableName}.{nameof(FiveStarRating)}," +
            $" {TableName}.{nameof(ReviewText)}," +
            $" {TableName}.{nameof(Timestamp)}";

        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        [Indexed(Unique = true)]
        public Guid UniqueId { get; set; }

        [Indexed]
        public Guid RestaurantUniqueId { get; set; }

        [JsonIgnore]
        [Indexed, ForeignKey(typeof(SqliteRestaurantReview))]
        public int ReviewerId { get; set; }

        [Ignore]
        public IUser Reviewer { get; internal set; } //Set by the parent DB controller

        /// <remarks>
        /// This should probably have a CHECK constraint for 1 <= FiveStarRating <= 5,
        /// but Sqlite.NET doesn't have an attribute for it. Should be checked by the parent db.
        /// </remarks>
        public int FiveStarRating { get; set; } 

        [NotNull]
        public string ReviewText { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
