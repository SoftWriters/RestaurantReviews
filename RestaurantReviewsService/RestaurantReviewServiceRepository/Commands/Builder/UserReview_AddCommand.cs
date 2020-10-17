using RestaurantReviewService;
using RestaurantReviewService.Commands.Builder;
using RestaurantReviewService.Entities;
using System.Data.SQLite;
using System.Text;

namespace RestaurantReviewServiceRepository.Commands.Builder
{
    public sealed class UserReview_AddCommand : SqlLiteCommandBuilderBase
    {
        private UserReview _userReview;

        public UserReview_AddCommand(SqlLiteDbConnection dbConnection, UserReview userReview) : base(dbConnection)
        {
            _userReview = userReview;
        }

        public override SQLiteCommand Build()
        {
            SQLiteCommand command = new SQLiteCommand(DbConnection.Connection);

            command.CommandText = getCommandText();

            command.Parameters.Add(new SQLiteParameter("@paramUserId", _userReview.UserIdRef));
            command.Parameters.Add(new SQLiteParameter("@paramRestId", _userReview.RestaurantIdRef));
            command.Parameters.Add(new SQLiteParameter("@paramTitle", _userReview.Title));
            command.Parameters.Add(new SQLiteParameter("@paramComments", _userReview.Comments));
            command.Parameters.Add(new SQLiteParameter("@paramRatingRef", _userReview.RatingsRef));

            return command;
        }

        private string getCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("INSERT INTO UserReviews (UserIdREF, RestaurantIdRef, Title, Comments, RatingsREF)VALUES");
            builder.Append("(@paramUserId, @paramRestId, @paramTitle, @paramComments, @paramRatingRef);");

            return builder.ToString();

        }
    }
}
