using RestaurantReviews.Core;
using SQLite.Net.Attributes;
using System;

namespace RestaurantReviews.Database.Sqlite
{
    [Table(TableName)]
    public class SqliteUser : PersistableBase, IUser
    {
        public const string TableName = "User";

        public SqliteUser() //Sqlite constructor
        {

        }

        public SqliteUser(IUser user)
        {
            UniqueId = user.UniqueId;
            DisplayName = user.DisplayName;
        }

        [PrimaryKey, AutoIncrement]
        public override int Id { get; set; }

        [Unique, Indexed]
        public Guid UniqueId { get; set; }

        public string DisplayName { get; set; }
    }
}
