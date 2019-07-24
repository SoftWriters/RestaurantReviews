﻿using System.Data.SQLite;
using System.Text;

namespace RestaurantReviewServiceRepository.Commands.Builder
{
    public sealed class Restaurants_SelectAllCommand : SqlLiteCommandBuilderBase
    {
        public Restaurants_SelectAllCommand(SqlLiteDbConnection dbConnection) : base(dbConnection)
        {
            // do nothing
        }

        public override SQLiteCommand Build()
        {
            SQLiteCommand command = new SQLiteCommand(getCommandText(), DbConnection.Connection);

            return command;
        }

        private string getCommandText()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("SELECT RR.*, ST.Name FROM Restaurants RR INNER JOIN ");
            builder.Append("STATES ST ON RR.StateREF = ST.Id ORDER BY RR.NAME ASC;");

            return builder.ToString();
        }
    }
}
