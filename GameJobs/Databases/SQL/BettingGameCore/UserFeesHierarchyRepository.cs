using System;
using System.Collections.Generic;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;

namespace FT.NH88.GameJobs.Databases.SQL.BettingGameCore
{
    public class UserFeesHierarchyRepository
    {
        private static readonly Lazy<UserFeesHierarchyRepository> _instance = new Lazy<UserFeesHierarchyRepository>(() => new UserFeesHierarchyRepository());
        private static string _connectionStr;

        public static UserFeesHierarchyRepository Instance
        {
            get { return _instance.Value; }
        }
        
        public void SetConnectionString(string connection)
        {
            _connectionStr = connection;
        }

        public List<UserFeesHierarchy> GetList(int limit,DateTime date)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.GetList<UserFeesHierarchy>(
                    $"select TOP {limit} * from UserFeesHierarchies where IsTransferToWallet = 'false' and Date <= {date:yyyyMMdd} order by ID");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Close();
            }
        }

        public int UpdateTransferToWallet(int id, bool isTranferred, long commission)
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.ExecuteNonQuery($"UPDATE " +
                                          $"[BettingGameCore].[dbo].[UserFeesHierarchies] " +
                                          $"SET " +
                                          $"[IsTransferToWallet] = '{isTranferred}', " +
                                          $"[RealTotalCommissionsTransfer] = {commission}, " +
                                          $"[UnixUpdatedAt] = datediff(second,'1970-01-01',getutcdate()), " +
                                          $"[UpdatedAt] = getdate() " +
                                          $"WHERE [ID] = {id}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db?.Close();
            }
        }
    }
}