using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;
using XoSoCronJob.Commons;

namespace FT.NH88.GameJobs.Databases.SQL.EmulatorGame
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

        public List<ChatBot> GetListChatBot()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.GetListSP<ChatBot>("SP_Get_Bot_Chat");
            }
            catch (Exception ex)
            {
                Logger.Error("EmulatorGame.GetListChatBot",ex);
                return new List<ChatBot>();
            }
            finally
            {
                db?.Close();
            }
        }

        public List<ChatMessage> GetListChatMessage()
        {
            DBHelper db = null;
            try
            {
                db = new DBHelper(_connectionStr);
                return db.GetListSP<ChatMessage>("SP_Get_Bot_Chat_Message");
            }
            catch (Exception ex)
            {
                Logger.Error("EmulatorGame.GetListChatMessage",ex);
                return new List<ChatMessage>();
            }
            finally
            {
                db?.Close();
            }
        }
    }
}