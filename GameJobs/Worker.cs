using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using FT.NH88.GameJobs.DataAccess;
using FT.NH88.GameJobs.Databases.ES.BettingGameCore;
using FT.NH88.GameJobs.Databases.SQL.Baccarat;
using FT.NH88.GameJobs.Databases.SQL.BettingGameCore;
using FT.NH88.GameJobs.Databases.SQL.MiniBlockBuster;
using FT.NH88.GameJobs.Jobs.SyncFeesAndFundAdd;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Logger = FT.NH88.GameJobs.Commons.Logger;
using StoredProcedureActor = FT.NH88.GameJobs.Databases.SQL.Baccarat.StoredProcedureActor;

namespace FT.NH88.GameJobs
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            
            var config = GetConfig();
            Config.Config.SetInstance(config);
            
            Logger.InitLogger(config);
            Logger.Info(JsonConvert.SerializeObject(config));
            
            GameFeesAndFunAddAggregationRepository.Instance.SetConnectionString(config.BettingGameCoreDB);
            UserFeesHierarchyRepository.Instance.SetConnectionString(config.BettingGameCoreDB);
            Databases.SQL.BettingGameCore.StoredProcedureActor.Instance.SetConnectionString(config.BettingGameCoreDB);
            
            SessionResultRepository.Instance.SetConnectionString(config.BaccaratDB);
            StoredProcedureActor.Instance.SetConnectionString(config.BaccaratDB);
            
            UserProfileRepository.Instance.SetConnectionString(config.Elasticsearch.Url,config.Elasticsearch.Username,config.Elasticsearch.Password);
            
            Databases.SQL.Sedie.SessionResultRepository.Instance.SetConnectionString(config.SedieDB);
            Databases.SQL.Sedie.StoredProcedureActor.Instance.SetConnectionString(config.SedieDB);
            
            Databases.SQL.LuckyDice.SessionResultRepository.Instance.SetConnectionString(config.LuckyDiceDB);
            Databases.SQL.LuckyDice.StoredProcedureActor.Instance.SetConnectionString(config.LuckyDiceDB);
            
            Databases.SQL.DragonTiger.SessionResultRepository.Instance.SetConnectionString(config.DragonTigerDB);
            Databases.SQL.DragonTiger.StoredProcedureActor.Instance.SetConnectionString(config.DragonTigerDB);
            
            RoomFundLogRepository.Instance.SetConnectionString(config.MiniBlockBusterDB);
            Databases.SQL.MiniBlockBuster.StoredProcedureActor.Instance.SetConnectionString(config.MiniBlockBusterDB);
            
            Databases.SQL.MiniStarKingdom.RoomFundLogRepository.Instance.SetConnectionString(config.MiniStarKingdomDB);
            Databases.SQL.MiniStarKingdom.StoredProcedureActor.Instance.SetConnectionString(config.MiniStarKingdomDB);
            
            Databases.SQL.MiniXPoker.RoomFundLogRepository.Instance.SetConnectionString(config.MiniXPokerDB);
            Databases.SQL.MiniXPoker.StoredProcedureActor.Instance.SetConnectionString(config.MiniXPokerDB);
            
            Databases.SQL.SlotMaya.RoomFundLogRepository.Instance.SetConnectionString(config.SlotMayaDB);
            Databases.SQL.SlotMaya.StoredProcedureActor.Instance.SetConnectionString(config.SlotMayaDB);
            
            Databases.SQL.SlotGenie.RoomFundLogRepository.Instance.SetConnectionString(config.SlotGenieDB);
            Databases.SQL.SlotGenie.StoredProcedureActor.Instance.SetConnectionString(config.SlotGenieDB);
            
            Databases.SQL.SlotIslands.RoomFundLogRepository.Instance.SetConnectionString(config.SlotIslandsDB);
            Databases.SQL.SlotIslands.StoredProcedureActor.Instance.SetConnectionString(config.SlotIslandsDB);
            
            Databases.SQL.SlotThreeKingdom.RoomFundLogRepository.Instance.SetConnectionString(config.SlotThreeKingdomDB);
            Databases.SQL.SlotThreeKingdom.StoredProcedureActor.Instance.SetConnectionString(config.SlotThreeKingdomDB);
            
            Databases.SQL.SlotZombie.RoomFundLogRepository.Instance.SetConnectionString(config.SlotZombieDB);
            Databases.SQL.SlotZombie.StoredProcedureActor.Instance.SetConnectionString(config.SlotZombieDB);
            
            Databases.SQL.Lottery.StoredProcedureActor.Instance.SetConnectionString(config.LotteryDB);
            
            Databases.SQL.SquashCrab.StoredProcedureActor.Instance.SetConnectionString(config.SquashCrabDB);
            
            Databases.SQL.EmulatorGame.StoredProcedureActor.Instance.SetConnectionString(config.EmulatorGameDB);
            
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            // and start it off
            await scheduler.Start();

           Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dictJobTriggers = 
               new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
           
           addCreateGameFunLogJob(dictJobTriggers,config);
           addTransferFeesHierarchyJob(dictJobTriggers,config);
           addTaiXiuChatBotJob(dictJobTriggers, config);
           addFinishedXoSoJob(dictJobTriggers,config);

            await scheduler.ScheduleJobs(
                new ReadOnlyDictionary<IJobDetail, IReadOnlyCollection<ITrigger>>(dictJobTriggers), 
                true,
                stoppingToken
            );

            if (stoppingToken.IsCancellationRequested)
            {
                await scheduler.Shutdown(stoppingToken);
            }
        }

        private void addCreateGameFunLogJob(Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dictJobTriggers,Config.Config config)
        {
            JobKey jobKey = JobKey.Create("CreateGameFunLogScheduleKey");
            
            IJobDetail job = JobBuilder.Create<Jobs.CreateGameFunLog.Job>()
                .WithIdentity(jobKey)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("CreateGameFunLogSchedule")
                .StartNow()
                .WithCronSchedule(config.CreateGameFunLogSchedule)
                .Build();
            
            dictJobTriggers.Add(job,new ReadOnlyCollection<ITrigger>(new List<ITrigger>()
            {
                trigger
            }));
        }
        
        private void addTransferFeesHierarchyJob(Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dictJobTriggers,Config.Config config)
        {
            JobKey jobKey = JobKey.Create("TransferFeesHierarchyScheduleKey");
            
            IJobDetail job = JobBuilder.Create<Jobs.TransferFeesHierarchy.Job>()
                .WithIdentity(jobKey)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TransferFeesHierarchySchedule")
                .StartNow()
                .WithCronSchedule(config.TransferFeesHierarchySchedule)
                .Build();
            
            dictJobTriggers.Add(job,new ReadOnlyCollection<ITrigger>(new List<ITrigger>()
            {
                trigger
            }));
        }
        
        private void addTaiXiuChatBotJob(Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dictJobTriggers,Config.Config config)
        {
            JobKey jobKey = JobKey.Create("SendChatBotTaiXiuKey");
            
            IJobDetail job = JobBuilder.Create<Jobs.TaiXiuChatBot.Job>()
                .WithIdentity(jobKey)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("SendChatBotTaiXiuSchedule")
                .StartNow()
                .WithCronSchedule(config.TaiXiuChatBotSchedule)
                .Build();
            
            dictJobTriggers.Add(job,new ReadOnlyCollection<ITrigger>(new List<ITrigger>()
            {
                trigger
            }));
        }
        
        private void addFinishedXoSoJob(Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>> dictJobTriggers,Config.Config config)
        {
            JobKey jobKey = JobKey.Create("FinishedXoSoKey");
            
            IJobDetail job = JobBuilder.Create<Jobs.FinishedXoSo.Job>()
                .WithIdentity(jobKey)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("FinishedXoSoSchedule")
                .StartNow()
                .WithCronSchedule(config.FinishedXoSoSchedule)
                .Build();
            
            dictJobTriggers.Add(job,new ReadOnlyCollection<ITrigger>(new List<ITrigger>()
            {
                trigger
            }));
        }
        
        
        private static Config.Config GetConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            return config.Get<Config.Config>();
        }
    }
}