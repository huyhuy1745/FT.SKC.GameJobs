using System;
using System.Threading.Tasks;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.Databases.SQL.Baccarat;
using Quartz;

namespace FT.NH88.GameJobs.Jobs.CreateGameFunLog
{
    public class Job: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                Logger.Info("Start create game fun logs");
                
                try
                {
                    int response =  Databases.SQL.LuckyDice.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of LuckyDice");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - LuckyDice",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.Sedie.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of Sedie");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - Sedie",ex);
                }
                
                try
                {
                    int response =  StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of Baccarat");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - Baccarat",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.DragonTiger.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of DragonTiger");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - DragonTiger",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.MiniBlockBuster.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of MiniBlockBuster");
                }
                catch (Exception ex)
                {
                   Logger.Error("CreateGameFunLog.Execute - MiniBlockBuster",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.MiniStarKingdom.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of MiniStarKingdom");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - MiniStarKingdom",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.MiniXPoker.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of MiniXPoker");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - MiniXPoker",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SlotMaya.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SlotMaya");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SlotMaya",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SlotGenie.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SlotGenie");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SlotGenie",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SlotIslands.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SlotIslands");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SlotIslands",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SlotThreeKingdom.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SlotThreeKingdom");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SlotThreeKingdom",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SlotZombie.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SlotZombie");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SlotZombie",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.Lottery.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of Lottery");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - Lottery",ex);
                }
                
                try
                {
                    int response =  Databases.SQL.SquashCrab.StoredProcedureActor.Instance.CreateGameFunLog();
                    CheckResponseAndSendError(response,"cannot create game fun log of SquashCrab");
                }
                catch (Exception ex)
                {
                    Logger.Error("CreateGameFunLog.Execute - SquashCrab",ex);
                }
                
                Logger.Info("End create game fun logs");
            });
        }
        
        private void CheckResponseAndSendError(int response, string error)
        {
            if(response == 1)
                return;
            Logger.Error($"{error} - response: [{response}]");
        }
    }
}