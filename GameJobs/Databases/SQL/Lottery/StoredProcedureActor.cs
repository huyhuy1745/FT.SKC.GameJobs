using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;
using XoSoCronJob.Commons;

namespace FT.NH88.GameJobs.Databases.SQL.Lottery
{
    public class StoredProcedureActor
    {
        private static readonly Lazy<StoredProcedureActor> _instance = new Lazy<StoredProcedureActor>(() => new StoredProcedureActor());
        private static string _connectionStr;

        public static StoredProcedureActor Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string connection)
        {
            _connectionStr = connection;
        }

        public int CreateGameFunLog()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@_Response", SqlDbType.Int);
                param[0].Direction = ParameterDirection.Output;
                db.ExecuteNonQuerySP("SP_GameFunLog_Create", param.ToArray());
                return  ConvertUtil.ToInt(param[0].Value);
            }
            finally
            {
                db?.Close();
            }
        }
        
        public SessionResult GetSessionResult(DateTime openDate)
        {
            DBHelper db = null;
            try
            {
                var pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@_CityID", 1);
                pars[1] = new SqlParameter("@_OpenDate", openDate);

                db = new DBHelper(_connectionStr);
                var rs = db.GetInstanceSP<SessionResult>("SP_Session_GetInfo", pars);
                return rs;
            }
            catch (Exception ex)
            {
                Logger.Error("Lottery.GetSessionResult",ex);
            }
            finally
            {
                db?.Close();
            }

            return null;
        }
        
        public int UpdateResult(XoSoResult data)
        {
            DBHelper db = null;
            int response = -1;
            try
            {
                var pars = new SqlParameter[11];
                pars[0] = new SqlParameter("@_CityID", 1);
                pars[1] = new SqlParameter("@_SpecialPrizeData", data.PrizeDB);
                pars[2] = new SqlParameter("@_FirstPrizeData", data.Prize1);
                pars[3] = new SqlParameter("@_SecondPrizeData", data.Prize2);
                pars[4] = new SqlParameter("@_ThirdPrizeData", data.Prize3);
                pars[5] = new SqlParameter("@_FourthPrizeData", data.Prize4);
                pars[6] = new SqlParameter("@_FifthPrizeData", data.Prize5);
                pars[7] = new SqlParameter("@_SixthPrizeData", data.Prize6);
                pars[8] = new SqlParameter("@_SeventhPrizeData", data.Prize7);
                pars[9] = new SqlParameter("@_EighthPrizeData", string.Empty);
                pars[10] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };

                db = new DBHelper(_connectionStr);
                db.ExecuteNonQuerySP("SP_ResultSession_Insert", pars);

                response = ConvertUtil.ToInt(pars[10].Value);
                return response;
            }
            catch (Exception ex)
            {
                Logger.Error("XoSoDatabase.UpdateResult",ex);
            }
            finally
            {
                Logger.Info($"XoSoDatabase.UpdateResult - R:{response}");
                db?.Close();
            }

            return -99;
        }
        
        public int FinishSession()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                var pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@_ResponseStatus", SqlDbType.Int) { Direction = ParameterDirection.Output };
                db.ExecuteNonQuerySP("SP_FinishSession",pars);
               return ConvertUtil.ToInt(pars[0].Value);
            }
            catch (Exception ex)
            {
                Logger.Error("EmulatorGame.GetListChatBot",ex);
                return -1;
            }
            finally
            {
                db?.Close();
            }
        }
    }
}