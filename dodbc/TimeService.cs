using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using Dapper;

namespace dodbc
{
    public class TimeService : ITimeService
    {
        private const string Sql = "SELECT TO_CHAR(NOW(), 'YYYY-MM-DD HH24:MI:SS.MS')";
        private string connectionString;

        public TimeService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                return new OdbcConnection(connectionString);
            }
        }

        public string Now()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var version = dbConnection.Query<String>(Sql);
                return version.FirstOrDefault();
            }
        }
    }
}
