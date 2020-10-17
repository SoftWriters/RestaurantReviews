using RestaurantReviewService;
using RestaurantReviewService.Commands.Builder;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewServiceRepository.Commands.Builder
{
    public sealed class UserReviews_SelectAllForRestaurantId : SqlLiteCommandBuilderBase
    {
        private int _restaurantId;
        private const string _restaurantIdParam = "@paramReataurantId";

        public UserReviews_SelectAllForRestaurantId(SqlLiteDbConnection dbConnection, int restaurantId) : base(dbConnection)
        {
            _restaurantId = restaurantId;
        }

        public override SQLiteCommand Build()
        {
            SQLiteCommand command = new SQLiteCommand(GetCommandText(), DbConnection.Connection);

            command.Parameters.Add(new SQLiteParameter(_restaurantIdParam, _restaurantId.ToString()));

            return command;
        }

        private string GetCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT UR.* , R.Id AS RatingsIdRef, R.Level, R.Description FROM UserReviews UR INNER JOIN Ratings R ON UR.RatingsRef = R.Id ");
            builder.AppendFormat("WHERE UR.RestaurantIdREF = {0};", _restaurantIdParam);

            return builder.ToString();
        }
    }
}
