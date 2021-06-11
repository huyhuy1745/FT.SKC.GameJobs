using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FT.NH88.GameJobs.DataAccess;
using XoSoCronJob.Commons;

namespace FT.NH88.GameJobs.Databases.SQL.Baccarat
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
    }
}