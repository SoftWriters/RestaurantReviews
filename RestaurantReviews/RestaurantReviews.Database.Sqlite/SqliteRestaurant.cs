using RestaurantReviews.Core;
using SQLite.Net;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    [Table(TableName)]
    public class SqliteRestaurant : PersistableBase, IRestaurant
    {
        public const string TableName = "Restaurant";

        public SqliteRestaurant() //Sqlite constructor
        {

        }

        public SqliteRestaurant(IRestaurant restaurant)
        {
            UpdateProperties(restaurant);
        }

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        //Foreign Key to Address. TODO: Enforce this
        public int AddressId { get; set; }

        [Unique, Indexed]
        public Guid UniqueId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Ignore]
        public IAddress Address { get; internal set; } //Set by parent db on load

        public void UpdateProperties(IRestaurant restaurant)
        {
            UniqueId = restaurant.UniqueId;
            Name = restaurant.Name;
            Description = restaurant.Description;
            Address = restaurant.Address;
        }

        public override bool Save(SQLiteConnection sqliteConnection)
        {
            if (!SaveAddress(sqliteConnection))
                return false;

            return base.Save(sqliteConnection);
        }

        public override bool Remove(SQLiteConnection sqliteConnection)
        {
            if (!RemoveAddress(sqliteConnection))
                return false;

            return base.Remove(sqliteConnection);
        }

        private bool SaveAddress(SQLiteConnection sqliteConnection)
        {
            var dbAddress = Address as SqliteAddress;
            if (dbAddress == null)
                dbAddress = new SqliteAddress(Address);

            if (!dbAddress.Save(sqliteConnection))
                return false;

            AddressId = dbAddress.Id;

            return true;
        }

        private bool RemoveAddress(SQLiteConnection sqliteConnection)
        {
            AddressId = 0;

            if (Address is SqliteAddress dbAddress)
                return dbAddress.Remove(sqliteConnection);

            return true;
        }
    }
}
