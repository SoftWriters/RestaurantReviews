using RestaurantReviews.Core.Interfaces;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite.Entities
{
    /// <summary>
    /// Sqlite db implementation of IRestaurant
    /// </summary>
    [Table(TableName)]
    internal class SqliteRestaurant : PersistableBase, IRestaurant
    {
        public SqliteRestaurant() //Sqlite constructor
        {

        }

        public SqliteRestaurant(IRestaurant restaurant, SqliteAddress address)
        {
            UpdateProperties(restaurant, address);
        }

        public const string TableName = "Restaurant";

        /// <summary>
        /// Comma separated string of the SQL table name and column namess for convenience in SQL queries.
        /// Avoids the needs for "SELECT *", which may have unintended side effects (e.g. column name conflicts in a JOIN statement).
        /// Provides a bit of encapsulation and convenient reusability.
        /// </summary>
        public static string FullyQualifiedTableProperties =
            $"{TableName}.{nameof(Id)}," +
            $" {TableName}.{nameof(UniqueId)}," +
            $" {TableName}.{nameof(AddressId)}," +
            $" {TableName}.{nameof(Name)}," +
            $" {TableName}.{nameof(Description)}";

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        [Indexed(Unique = true)]
        public Guid UniqueId { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Description { get; set; }

        //Attribute is really only for documentation purposes, as the performance of Sqlitenetextensions isn't great
        [Indexed, ForeignKey(typeof(SqliteAddress))]
        public int AddressId { get; set; }

        [Ignore]
        public IAddress Address { get; internal set; } //Set by parent db on load

        public void UpdateProperties(IRestaurant restaurant, SqliteAddress address)
        {
            UniqueId = restaurant.UniqueId;
            Name = restaurant.Name;
            Description = restaurant.Description;
            Address = address;
            AddressId = address.Id;
        }
    }
}
