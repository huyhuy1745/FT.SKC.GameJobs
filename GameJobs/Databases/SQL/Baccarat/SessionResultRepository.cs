using System;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;

namespace FT.NH88.GameJobs.Databases.SQL.Baccarat
{
    public class SessionResultRepository
    {
        private static readonly Lazy<SessionResultRepository> _instance = new Lazy<SessionResultRepository>(() => new SessionResultRepository());
        private static string _connectionStr;

        public static SessionResultRepository Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string connection)
        {
            _connectionStr = connection;
        }

        public Fees GetSumFeesInDate(DateTime date)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.GetInstance<Fees>(
                    $"SELECT SUM(Fees) as SumFees from [Session.Results] where Date = {date:yyyyMMdd}");
            }
            finally
            {
                db.Close();
            }
        }

        public void Delete(DateTime date)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                 db.ExecuteNonQuery(
                    $"DELETE from [Session.Results] where Date <= {date:yyyyMMdd}");
            }
            finally
            {
                db.Close();
            }
        }
    }
}