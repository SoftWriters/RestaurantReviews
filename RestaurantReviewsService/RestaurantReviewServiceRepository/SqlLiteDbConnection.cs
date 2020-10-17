using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using RestaurantReviewServiceRepository;

namespace RestaurantReviewService
{
    public class SqlLiteDbConnection : DbConnectionBase, IDisposable
    {
        public SqlLiteDbConnection()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbConnection"/> class.
        /// </summary>
        /// <param name="dbFileName">Name of the db file.</param>
        public SqlLiteDbConnection(string dbFileName) : base()
        {
            SQLiteConnection.CreateFile(dbFileName);
        }

        #region Fields

        // TODO: These members can also be configuration driven. - they can be lazy loaded and exposed via the LocalizationSettings
        // object.

        private SQLiteConnection _connection;
        private const string DB_VERSION = "3";
        private const string DB_NEW = "False";
        private const string DB_COMPRESS = "True";
        private const string DB_FAIL_IF_MISSING = "True";
        private const int PAGE_SIZE = 4096;
        private const int CACHE_SIZE = 10000;
        private const int TIMEOUT = 500;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>
        /// The connection.
        /// </value>
        public SQLiteConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State == ConnectionState.Closed)
                {
                    Connect();
                }

                return _connection;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Closes the underlying connection and disposes the connection object.
        /// </summary>
        public void Close()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        private void Connect()
        {
            try
            {
                //string connectionString = string.Format("Data Source={0};Version={1};New={2};Compress={3};",
                //    EventDefinitionManagementSettings.ConfigurationSettingsTargetDB, DB_VERSION, DB_NEW, DB_COMPRESS);

                _connection = new SQLiteConnection(getSqliteConnectionString());

                _connection.Open();

            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="DbConnection"/> class.
        /// </summary>
        ~SqlLiteDbConnection()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }

        /// <summary>
        /// Gets the sqlite connection string.
        /// </summary>
        /// <returns></returns>
        private string getSqliteConnectionString()
        {
            //SQLiteConnectionStringBuilder connBuilder = new SQLiteConnectionStringBuilder();
            //connBuilder.DataSource = filePath;
            //connBuilder.Version = 3;
            ////Set page size to NTFS cluster size = 4096 bytes
            //connBuilder.PageSize = 4096;
            //connBuilder.CacheSize = 10000;
            //connBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
            //connBuilder.Pooling = true;
            //connBuilder.LegacyFormat = false;
            //connBuilder.DefaultTimeout = 500;
            //connBuilder.Password = "yourpass";

            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();


            string TARGET_DB = Resources.SqlLiteDataBase;
            //const string TARGET_DB = "C:\\Development\\RestaurantReviews\\Data\\RestaurantReviews.db";
            connectionStringBuilder.DataSource = TARGET_DB;
            connectionStringBuilder.Version = Convert.ToInt32(DB_VERSION);
            connectionStringBuilder.PageSize = PAGE_SIZE;
            connectionStringBuilder.CacheSize = CACHE_SIZE;
            //connectionStringBuilder.JournalMode = SQLiteJournalModeEnum.Wal;
            connectionStringBuilder.DefaultTimeout = TIMEOUT;

            return connectionStringBuilder.ToString();

        }

        #endregion
    }
}
