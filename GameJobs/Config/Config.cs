using System.Runtime.CompilerServices;
using FT.NH88.GameWorkers.Config;
using RestSharp.Authenticators.OAuth;

namespace FT.NH88.GameJobs.Config
{
    public class Config
    {
        private static Config _instance;
        
        public static Config GetInstance()
        {
            return _instance;
        }

        public static void SetInstance(Config config)
        {
            Config._instance = config;
        }
        
        public string SyncFeesAndFunSchedule { get; set; }
        public string CreateGameFunLogSchedule { get; set; }
        public string TransferFeesHierarchySchedule { get; set; }
        public string TaiXiuChatBotSchedule { get; set; }
        public string FinishedXoSoSchedule { get; set; }
        
        public string Service { get; set; }
        public string RabitmqHost { get; set; }
        public int RabitmqPort { get; set; }
        public string RabitmqUsername { get; set; }
        public string RabitmqPassword { get; set; }
        public string RabitmqExchange { get; set; }
        public string RabitmqRoutekey { get; set; }
        
        public ElasticsearchConfig Elasticsearch { get; set; }
        
        public string BettingGameCoreDB { get; set; }
        public string LuckyDiceDB { get; set; }
        public string BaccaratDB { get; set; }
        public string DragonTigerDB { get; set; }
        public string SedieDB { get; set; }
        public string MiniBlockBusterDB { get; set; }
        public string MiniStarKingdomDB { get; set; }
        public string MiniXPokerDB { get; set; }
        public string SlotMayaDB { get; set; }
        public string SlotGenieDB { get; set; }
        public string SlotIslandsDB { get; set; }
        public string SlotThreeKingdomDB { get; set; }
        public string SlotZombieDB { get; set; }
        public string LotteryDB { get; set; }
        public string SquashCrabDB { get; set; }
        public string EmulatorGameDB { get; set; }
        
        public TaiXiuChatBotConfig TaiXiuChatBotConfig { get; set; }
   
    }
}