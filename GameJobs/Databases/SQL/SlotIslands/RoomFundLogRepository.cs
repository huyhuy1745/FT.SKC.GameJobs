using System;
using System.Collections.Generic;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;

namespace FT.NH88.GameJobs.Databases.SQL.SlotIslands
{
    public class RoomFundLogRepository
    {
        private static readonly Lazy<RoomFundLogRepository> _instance = new Lazy<RoomFundLogRepository>(() => new RoomFundLogRepository());
        private static string _connectionStr;

        public static RoomFundLogRepository Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string connection)
        {
            _connectionStr = connection;
        }
        
        public List<FeesAndFundAdd> GetSumFeesAndFundAddInDate(DateTime date)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.GetList<FeesAndFundAdd>(
                    $"SELECT RoomID, SUM(Fees) as SumFees, Sum(PrizeFundAdd) as SumPrizeFundAdd, SUM(JackpotFundAdd) as SumJackpotFundAdd from RoomFundLogs where Date = {date:yyyyMMdd} group by RoomID");
            }
            finally
            {
                db.Close();
            }
        }
        
        public void Delete(DateTime date, int roomID)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                db.ExecuteNonQuery(
                    $"DELETE from RoomFundLogs where Date <= {date:yyyyMMdd} and RoomID = {roomID}");
            }
            finally
            {
                db.Close();
            }
        }
    }
}