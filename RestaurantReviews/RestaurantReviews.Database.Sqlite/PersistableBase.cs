using SQLite.Net;

namespace RestaurantReviews.Database.Sqlite
{
    public abstract class PersistableBase
    {
        //SQLite creates columns for derived class properties before the base. This is abstract so that derived classes can declare it as the first property. 
        public abstract int Id { get; set; }

        public virtual bool Save(SQLiteConnection sqliteConnection)
        {
            if (Id == 0 && sqliteConnection.Insert(this) == 0)
                return false;
            else if (sqliteConnection.Update(this) == 0)
                return false;

            return true;
        }

        public virtual bool Remove(SQLiteConnection sqliteConnection)
        {
            if (sqliteConnection.Delete(this) == 0)
                return false;

            Id = 0;
            return true;
        }
    }
}
