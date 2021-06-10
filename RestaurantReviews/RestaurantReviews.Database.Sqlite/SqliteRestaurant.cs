using RestaurantReviews.Core;
using SQLite.Net;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    [Table(TableName)]
    public class SqliteRestaurant : PersistableBase, IRestaurant
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

        //Foreign Key to Address. TODO: Enforce this
        public int AddressId { get; set; }

        [Indexed(Unique=true)]
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

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
