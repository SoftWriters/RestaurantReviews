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

        public SqliteRestaurant(IRestaurant restaurant, SqliteAddress address)
        {
            UpdateProperties(restaurant, address);
        }

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
