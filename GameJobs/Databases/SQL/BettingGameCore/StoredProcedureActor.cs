using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.DataAccess;
using XoSoCronJob.Commons;

namespace FT.NH88.GameJobs.Databases.SQL.BettingGameCore
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

        public void AdminTransferToUser(long userID, double amount,string note, int reasonID, int transType,int serviceId, out double adminRemainBalance, out int response)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@_AdminId", 1));
                param.Add(new SqlParameter("@_ReceiverId", userID));
                param.Add(new SqlParameter("@_Amount", amount));
                param.Add(new SqlParameter("@_Note",note ));
                param.Add(new SqlParameter("@_ServiceID", serviceId));
                param.Add(new SqlParameter("@_FromTransID", 0));
                param.Add(new SqlParameter("@_ReasonID", reasonID));
                param.Add(new SqlParameter("@_TransType",transType));
                
                SqlParameter remainWallet = new SqlParameter("@_Wallet", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(remainWallet);
                
                SqlParameter res = new SqlParameter("@_Response", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(res);
   
                db.ExecuteNonQuerySP("SP_Admin_Transfer_To_User_2", param.ToArray());

                response = Convert.ToInt32(res.Value);
                adminRemainBalance = 0;
                if (response == 1)
                {
                    adminRemainBalance = Convert.ToDouble(remainWallet.Value);
                }

                return;
            }
            catch (Exception ex)
            {
                Logger.Error("StoredProcedureActor.AdminTransferToUser",ex);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }

            adminRemainBalance = 0;
            response = -99;
        }
        
        public void AdminTransferToAgency(long userID, double amount,string note, int reasonID, int transType,int serviceId, out double adminRemainBalance, out int response)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@_AdminId", 1));
                param.Add(new SqlParameter("@_ReceiverId", userID));
                param.Add(new SqlParameter("@_WalletType", 1));
                param.Add(new SqlParameter("@_Amount", amount));
                param.Add(new SqlParameter("@_ServiceID", serviceId));
                param.Add(new SqlParameter("@_Note",note ));
                param.Add(new SqlParameter("@_ReasonID", reasonID));
                param.Add(new SqlParameter("@_TransType",transType));
                
                SqlParameter remainWallet = new SqlParameter("@_Wallet", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(remainWallet);
                
                SqlParameter res = new SqlParameter("@_Response", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(res);
   
                db.ExecuteNonQuerySP("SP_Admin_Transfer_To_Agency_2", param.ToArray());

                // adminRemainBalance = Convert.ToDouble(remainWallet.Value);
                adminRemainBalance = 0;
                response = Convert.ToInt32(res.Value);
                
                return;
            }
            catch (Exception ex)
            {
                Logger.Error("StoredProcedureActor.AdminTransferToUser",ex);
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }

            adminRemainBalance = 0;
            response = -99;
        }

        public int SendMail(long userId, string nickname, string title, string content)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@_SenderID", 1));
                param.Add(new SqlParameter("@_ReceiverID", userId));
                param.Add(new SqlParameter("@_ReceiverName", nickname));
                param.Add(new SqlParameter("@_Title", title));
                param.Add(new SqlParameter("@_Content", content));
                param.Add(new SqlParameter("@_Status",1 ));
                param.Add(new SqlParameter("@_CreatedTime", DateTime.Now));
                param.Add(new SqlParameter("@_ServiceID",1));
                
                SqlParameter response = new SqlParameter("@_Response", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                param.Add(response);
   
                db.ExecuteNonQuerySP("SP_System_SendMail", param.ToArray());
                
                return Convert.ToInt32(response.Value);
            }
            catch (Exception ex)
            {
                Logger.Error("StoredProcedureActor.SendMail",ex);
                return -1;
            }
            finally
            {
                if (db != null)
                {
                    db.Close();
                }
            }
        }
    }
}