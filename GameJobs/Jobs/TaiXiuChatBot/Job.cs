using System;
using System.Threading.Tasks;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.Helpers;
using FT.NH88.GameJobs.Models;
using Quartz;

namespace FT.NH88.GameJobs.Jobs.TaiXiuChatBot
{
    public class Job : IJob
    {
        Random random = new Random();
        
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                try
                {
                    int numberChat = random.Next(
                        Config.Config.GetInstance().TaiXiuChatBotConfig.NumberChatMin, 
                        Config.Config.GetInstance().TaiXiuChatBotConfig.NumberChatMax
                    );

                    int count = 0;
                    while (count < numberChat)
                    {
                        count++;
                        int randomTime = random.Next(
                            Config.Config.GetInstance().TaiXiuChatBotConfig.MinTimePerChat, 
                            Config.Config.GetInstance().TaiXiuChatBotConfig.MaxTimePerChat
                        );
                        System.Threading.Thread.Sleep(randomTime);

                        var lstChatBots = Databases.SQL.EmulatorGame.StoredProcedureActor.Instance.GetListChatBot();
                        if (lstChatBots.Count == 0)
                            return;
                        
                        var bot = lstChatBots[0];

                        var lstChatMessages =
                            Databases.SQL.EmulatorGame.StoredProcedureActor.Instance.GetListChatMessage();
                        if (lstChatMessages.Count == 0)
                            return;

                        var api = new ApiUtil<bool>();
                        api.ApiAddress = Config.Config.GetInstance().TaiXiuChatBotConfig.ChatURL;
                        api.URI = Config.Config.GetInstance().TaiXiuChatBotConfig.ChatURL;
                        
                        var res = api.Send(new
                        {
                            AccountId = bot.BotId,
                            NickName = bot.DisplayName,
                            ServiceId = bot.ServiceID,
                            Vip = 1,
                            Message = lstChatMessages[0].Content
                        });
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("TaiXiuChatBot.Execute",ex);
                }
            });
        }
    }
}