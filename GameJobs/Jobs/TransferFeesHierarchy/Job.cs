using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.Databases.ES.BettingGameCore;
using FT.NH88.GameJobs.Databases.SQL.BettingGameCore;
using FT.NH88.GameJobs.Models;
using FT.NH88.GameJobs.Utils;
using Quartz;

namespace FT.NH88.GameJobs.Jobs.TransferFeesHierarchy
{
    public class Job: IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                bool isContinue = true;
                int numberRecordCanGet = 10000;
                while (isContinue)
                {
                    List<UserFeesHierarchy> listData = UserFeesHierarchyRepository.Instance.GetList(numberRecordCanGet,DateTime.Now.AddDays(-1));
                    foreach (UserFeesHierarchy data in listData)
                    {
                        UserProfile userProfile = UserProfileRepository.Instance.GetById(data.UserID);
                        if (userProfile == null)
                        {
                            Logger.Error($"TransferFeesHierarchy.Execute - cannot find user [{data.UserID}]");
                            continue;
                        }

                        long commission = Convert.ToInt64(data.TotalCommissions);

                        if (commission > 0)
                        {
                            Databases.SQL.BettingGameCore.StoredProcedureActor.Instance.AdminTransferToUser(
                                data.UserID,
                                commission,
                                $"Thanh toán hoa hồng ngày {data.GetDate():d}",
                                48,
                                7,
                                userProfile.ServiceId,
                                out _, 
                                out var resp
                            );
                            if (resp != 1)
                            {
                                Logger.Error($"TransferFeesHierarchy.Execute  - không thể cộng tiền cho - userID: [{data.UserID}] - commission: [{commission}] - userFeesHierarchyID: [{data.ID}] - response: [{resp}]",null);
                                continue;
                            }
                            // gửi hoa hồng
                            Databases.SQL.BettingGameCore.StoredProcedureActor.Instance.SendMail(
                                userProfile.UserId,
                                userProfile.Displayname, 
                                $"Hoa hồng ngày {data.GetDate():d}",
                                $"Hoa hồng đã nhận: {commission.LongToMoneyFormat()}" +
                                $". Chúc bạn chơi game vui vẻ" +
                                $".");
                        }
                        UserFeesHierarchyRepository.Instance.UpdateTransferToWallet(data.ID, true, commission);
                    }

                    if (listData.Count < numberRecordCanGet)
                    {
                        isContinue = false;
                    }
                }
            });
        }
    }
}