using System.Data.SQLite;
using System.Text;

namespace RestaurantReviewService.Commands.Builder
{
    public sealed class Users_SelectAllCommand : SqlLiteCommandBuilderBase
    {
        public Users_SelectAllCommand(SqlLiteDbConnection dbConnection) : base(dbConnection)
        {
            // do nothing
        }

        public override SQLiteCommand Build()
        {
            SQLiteCommand command = new SQLiteCommand(GetCommandText(), DbConnection.Connection);

            return command;
        }


        private string GetCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT UR.*, ST.Name FROM Users UR INNER JOIN ");
            builder.Append("STATES ST ON UR.StateREF = ST.Id ORDER BY UR.LastName ASC;");

            return builder.ToString();
        }
    }
}
