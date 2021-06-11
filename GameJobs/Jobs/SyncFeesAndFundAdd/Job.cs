using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FT.NH88.GameJobs.Commons;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Models;
using Quartz;
using Serilog;

namespace FT.NH88.GameJobs.Jobs.SyncFeesAndFundAdd
{
    public class Job : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                // DateTime prevDate = DateTime.Today;
                
                // DateTime prevDate = DateTime.Today.AddDays(-1);
                // int response = 0;
                
                // Logger.Info($"Start SyncFeesAndFundAdd - {prevDate}");
                
                // // tai xiu
                // Fees luckyDiceFees =
                //     Games.LuckyDice.DataAccess.SessionResultRepository.Instance.GetSumFeesInDate(prevDate);
                // if (luckyDiceFees.SumFees > 0)
                // {
                //     response = GameFeesAndFunAddAggregationRepository.Instance.Create(8, 0, prevDate, luckyDiceFees.SumFees, 0, 0);
                //     CheckResponseAndSendError(response,$"error when calutale total fees of luckyDice");
                //     // if (response == 1)
                //     //     Games.LuckyDice.DataAccess.SessionResultRepository.Instance.Delete(prevDate);
                // }
                //
                // // baccarat
                // Fees baccaratFees =
                //     Games.Baccarat.DataAccess.SessionResultRepository.Instance.GetSumFeesInDate(prevDate);
                // if (baccaratFees.SumFees > 0)
                // {
                //     response = GameFeesAndFunAddAggregationRepository.Instance.Create(19, 0, prevDate,
                //         baccaratFees.SumFees, 0, 0);
                //     CheckResponseAndSendError(response, $"error when calutale total fees of baccarat");
                //     // if (response == 1)
                //     //     Games.LuckyDice.DataAccess.SessionResultRepository.Instance.Delete(prevDate);
                // }
                //
                // // rong ho
                // Fees dragonTigerFees =
                //     Games.DragonTiger.DataAccess.SessionResultRepository.Instance.GetSumFeesInDate(prevDate);
                // if (dragonTigerFees.SumFees > 0)
                // {
                //     response = GameFeesAndFunAddAggregationRepository.Instance.Create(14, 0, prevDate,
                //         dragonTigerFees.SumFees, 0, 0);
                //     CheckResponseAndSendError(response, $"error when calutale total fees of dragonTiger");
                //     // if (response == 1)
                //     //     Games.LuckyDice.DataAccess.SessionResultRepository.Instance.Delete(prevDate);
                // }
                //
                // // xoc dia
                // Fees sedieFees =
                //     Games.Sedie.DataAccess.SessionResultRepository.Instance.GetSumFeesInDate(prevDate);
                // if (sedieFees.SumFees > 0)
                // {
                //     response = GameFeesAndFunAddAggregationRepository.Instance.Create(63, 0, prevDate, sedieFees.SumFees,
                //         0, 0);
                //     CheckResponseAndSendError(response, $"error when calutale total fees of sedie");
                //     // if (response == 1)
                //     //     Games.LuckyDice.DataAccess.SessionResultRepository.Instance.Delete(prevDate);
                // }
                
                // xieu xe
                // List<FeesAndFundAdd> listMiniBlockBusterFeesAndFundAdds = Games.MiniBlockBuster.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listMiniBlockBusterFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listMiniBlockBusterFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(12, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of miniBlockBuster");
                //         // if (response == 1)
                //         //     Games.MiniBlockBuster.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // candy
                // List<FeesAndFundAdd> listMiniStarKingdomFeesAndFundAdds = Games.MiniStarKingdom.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listMiniStarKingdomFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listMiniStarKingdomFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(7, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of miniStarKingdom");
                //         // if (response == 1)
                //         //     Games.MiniStarKingdom.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // poker
                // List<FeesAndFundAdd> listMiniXPokerFeesAndFundAdds = Games.MiniXPoker.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listMiniXPokerFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listMiniXPokerFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(11, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of miniXPoker");
                //         // if (response == 1)
                //         //     Games.MiniXPoker.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // cowboys
                // List<FeesAndFundAdd> listSlotMayaFeesAndFundAdds = Games.SlotMaya.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listSlotMayaFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listSlotMayaFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(3, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of slotMaya");
                //         // if (response == 1)
                //         //     Games.SlotMaya.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // ai cap
                // List<FeesAndFundAdd> listSlotGenieFeesAndFundAdds = Games.SlotGenie.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listSlotGenieFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listSlotGenieFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(4, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of slotGenie");
                //         // if (response == 1)
                //         //     Games.SlotGenie.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // thuy cung
                // List<FeesAndFundAdd> listSlotIslandsFeesAndFundAdds = Games.SlotIslands.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listSlotIslandsFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listSlotIslandsFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(1, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of slotIslands");
                //         // if (response == 1)
                //         //     Games.SlotIslands.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // tam quoc
                // List<FeesAndFundAdd> listSlotThreeKingdomFeesAndFundAdds = Games.SlotThreeKingdom.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listSlotThreeKingdomFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listSlotThreeKingdomFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(2, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of slotThreeKingdom");
                //         // if (response == 1)
                //         //     Games.SlotThreeKingdom.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                //
                // // songoku
                // List<FeesAndFundAdd> listSlotZombieFeesAndFundAdds = Games.SlotZombie.DataAccess
                //     .RoomFundLogRepository.Instance.GetSumFeesAndFundAddInDate(prevDate);
                // if (listSlotZombieFeesAndFundAdds.Count > 0)
                // {
                //     foreach (var data in listSlotZombieFeesAndFundAdds)
                //     {
                //         response = GameFeesAndFunAddAggregationRepository.Instance.Create(15, data.RoomID, prevDate,
                //             data.SumFees, data.SumPrizeFundAdd, data.SumJackpotFundAdd);
                //         CheckResponseAndSendError(response, $"error when calutale total fees of slotZombie");
                //         // if (response == 1)
                //         //     Games.SlotZombie.DataAccess
                //         //         .RoomFundLogRepository.Instance.Delete(prevDate,data.RoomID);
                //     }
                // }
                // Logger.Info($"End SyncFeesAndFundAdd - {prevDate}");
            });
        }

        public void CheckResponseAndSendError(int response, string error)
        {
            if(response == 1)
                return;
            Logger.Error($"{error} - {response}");
        }
    }
}