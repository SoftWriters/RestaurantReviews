using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantReviewService.Commands.Builder
{
    public abstract class SqlLiteCommandBuilderBase : SqlLiteDbConnection, ISqlLiteCommandBuilder<SQLiteCommand>
    {
        public SqlLiteCommandBuilderBase(SqlLiteDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }


        protected SqlLiteDbConnection DbConnection { get; private set; }

        public abstract SQLiteCommand Build();
    }
}
