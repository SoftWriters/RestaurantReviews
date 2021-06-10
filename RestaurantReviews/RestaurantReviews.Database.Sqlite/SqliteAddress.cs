using RestaurantReviews.Core;
using SQLite.Net;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    [Table(TableName)]
    public class SqliteAddress : PersistableBase, IAddress
    {
        public const string TableName = "Address";

        public SqliteAddress()
        {

        }

        public SqliteAddress(IAddress address)
        {
            if (address == null)
                return;

            StreetLine1 = address.StreetLine1;
            StreetLine2 = address.StreetLine2;
            BuildingNumber = address.BuildingNumber;
            City = address.City;
            CountryOrRegion = address.CountryOrRegion;
            StateOrProvince = address.StateOrProvince;
            PostalCode = address.PostalCode;
        }

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        public string StreetLine1 { get; set; }

        public string StreetLine2 { get; set; }

        public string BuildingNumber { get; set; }

        public string City { get; set; }

        public string CountryOrRegion { get; set; }

        public string StateOrProvince { get; set; }

        public string PostalCode { get; set; } //TODO: max length on this
    }
}
