using RestaurantReviewService.Entities;
using System.Data.SQLite;
using System.Text;

namespace RestaurantReviewService.Commands.Builder
{
    public sealed class Restaurant_AddCommand : SqlLiteCommandBuilderBase
    {
        private Restaurant _restaurant;

        public Restaurant_AddCommand(SqlLiteDbConnection dbConnection, Restaurant restaurant) : base(dbConnection)
        {
            _restaurant = restaurant;
        }

        public override SQLiteCommand Build()
        {
            
            SQLiteCommand command = new SQLiteCommand(DbConnection.Connection);

            command.CommandText = getCommandText();

            command.Parameters.Add(new SQLiteParameter("@paramName", _restaurant.Name));
            command.Parameters.Add(new SQLiteParameter("@paramStreetAddress", _restaurant.StreetAddress));
            command.Parameters.Add(new SQLiteParameter("@paramOverview", _restaurant.Overview));
            command.Parameters.Add(new SQLiteParameter("@paramCity", _restaurant.City));
            command.Parameters.Add(new SQLiteParameter("@paramStateREF", _restaurant.StateIdRef));
           

            return command;
        }


        private string getCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("INSERT INTO Restaurants (Name, StreetAddress, Overview, City, StateREF) VALUES");
            builder.Append("(@paramName, @paramStreetAddress, @paramOverview, @paramCity, @paramStateREF);");

            return builder.ToString();
          
        }
    }
}
