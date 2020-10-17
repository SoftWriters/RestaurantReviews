using System.Data.SQLite;
using System.Text;

namespace RestaurantReviewService.Commands.Builder
{
    public sealed class UserReviews_SelectAllForUserId : SqlLiteCommandBuilderBase
    {
        private int _userId;
        private const string _userIdParam = "@paramUserId";

        public UserReviews_SelectAllForUserId(SqlLiteDbConnection dbConnection, int userId) : base(dbConnection)
        {
            _userId = userId;
        }

        public override SQLiteCommand Build()
        {
            SQLiteCommand command = new SQLiteCommand(GetCommandText(), DbConnection.Connection);

            command.Parameters.Add(new SQLiteParameter(_userIdParam, _userId.ToString()));

            return command;
        }


        private string GetCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT UR.* , R.Id AS RatingsIdRef, R.Level, R.Description FROM UserReviews UR INNER JOIN Ratings R ON UR.RatingsRef = R.Id ");
            builder.AppendFormat("WHERE UR.UserIdREF = {0};", _userIdParam);

            return builder.ToString();
        }
    }
}
