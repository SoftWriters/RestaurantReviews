using RestaurantReviews.Core.Interfaces;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite.Entities
{
    /// <summary>
    /// Sqlite db representation of IUser
    /// </summary>
    [Table(TableName)]
    internal class SqliteUser : PersistableBase, IUser
    {
        public SqliteUser() //Sqlite constructor
        {

        }

        public SqliteUser(IUser user)
        {
            UniqueId = user.UniqueId;
            DisplayName = user.DisplayName;
        }

        public const string TableName = "User";

        /// <summary>
        /// Comma separated string of the SQL table name and column namess for convenience in SQL queries.
        /// Avoids the needs for "SELECT *", which may have unintended side effects (e.g. column name conflicts in a JOIN statement).
        /// Provides a bit of encapsulation and convenient reusability.
        /// </summary>
        public static string FullyQualifiedTableProperties =
            $"{TableName}.{nameof(Id)}," +
            $" {TableName}.{nameof(UniqueId)}," +
            $" {TableName}.{nameof(DisplayName)}";

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        [Indexed(Unique = true)]
        public Guid UniqueId { get; set; }

        [NotNull]
        public string DisplayName { get; set; }
    }
}
