using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FT.NH88.GameJobs.DataAccess;
using XoSoCronJob.Commons;

namespace FT.NH88.GameJobs.Databases.SQL.BettingGameCore
{
    public class GameFeesAndFunAddAggregationRepository
    {
        private static readonly Lazy<GameFeesAndFunAddAggregationRepository> _instance = new Lazy<GameFeesAndFunAddAggregationRepository>(() => new GameFeesAndFunAddAggregationRepository());
        private static string _connectionStr;

        public static GameFeesAndFunAddAggregationRepository Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string connection)
        {
            _connectionStr = connection;
        }

        public int Create(int gameID, int roomID, DateTime date,long totalFees, long totalPrizeFundAdd,long totalJackpotFundAdd)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                SqlParameter[] param = new SqlParameter[7];
                
                param[0] = new SqlParameter("@_GameID", SqlDbType.Int);
                param[0].Value = gameID;

                param[1] = new SqlParameter("@_RoomID", SqlDbType.Int);
                param[1].Value = roomID;
                
                param[2] = new SqlParameter("@_Date", SqlDbType.Int);
                param[2].Value = int.Parse(date.ToString("yyyyMMdd"));
                
                param[3] = new SqlParameter("@_TotalFees", SqlDbType.BigInt);
                param[3].Value = totalFees;

                param[4] = new SqlParameter("@_TotalPrizeFundAdd", SqlDbType.BigInt);
                param[4].Value = totalPrizeFundAdd;
                
                param[5] = new SqlParameter("@_TotalJackpotFundAdd", SqlDbType.BigInt);
                param[5].Value = totalJackpotFundAdd;
                
                param[6] = new SqlParameter("@_Response", SqlDbType.Int);
                param[6].Direction = ParameterDirection.Output;
                db.ExecuteNonQuerySP("SP_CreateGameFeesAndFunAddAggregation", param.ToArray());
                return  ConvertUtil.ToInt(param[6].Value);
            }
            finally
            {
                db?.Close();
            }
        }
    }
}