using RestaurantReviews.Core;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    /// <summary>
    /// Sqlite db representation of IAddress
    /// </summary>
    [Table(TableName)]
    public class SqliteAddress : PersistableBase, IAddress
    {
        public SqliteAddress() //Sqlite constructor
        {

        }

        public SqliteAddress(IAddress address)
        {
            UpdateProperties(address);
        }

        public const string TableName = "Address";

        /// <summary>
        /// Comma separated string of the SQL table name and column namess for convenience in SQL queries.
        /// Avoids the needs for "SELECT *", which may have unintended side effects (e.g. column name conflicts in a JOIN statement).
        /// Provides a bit of encapsulation and convenient reusability.
        /// </summary>
        public static string FullyQualifiedTableProperties =
            $"{TableName}.{nameof(Id)}," +
            $" {TableName}.{nameof(UniqueId)}," +
            $" {TableName}.{nameof(StreetLine1)}," +
            $" {TableName}.{nameof(StreetLine2)}," +
            $" {TableName}.{nameof(BuildingNumber)}," +
            $" {TableName}.{nameof(City)}," +
            $" {TableName}.{nameof(CountryOrRegion)}," +
            $" {TableName}.{nameof(StateOrProvince)}," +
            $" {TableName}.{nameof(PostalCode)}";

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        [Indexed(Unique = true)]
        public Guid UniqueId { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string BuildingNumber { get; set; }

        public string City { get; set; }

        public string CountryOrRegion { get; set; }

        public string StateOrProvince { get; set; }

        [MaxLength(10)]
        public string PostalCode { get; set; }

        public void UpdateProperties(IAddress address)
        {
            UniqueId = address.UniqueId;
            StreetLine1 = address.StreetLine1;
            StreetLine2 = address.StreetLine2;
            BuildingNumber = address.BuildingNumber;
            City = address.City;
            CountryOrRegion = address.CountryOrRegion;
            StateOrProvince = address.StateOrProvince;
            PostalCode = address.PostalCode;
        }
    }
}
