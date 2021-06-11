using System;
using FT.NH88.FloggerCore;

namespace FT.NH88.GameJobs.Commons
{
    public class Logger
    {
        private static Flogger instanceLogger;

        public static void InitLogger(Config.Config config)
        {
            instanceLogger = new Flogger( new ConfigLog()
            {
                Server = config.Service,
                RabitMq = new RabitMQConfig()
                {
                    Host = config.RabitmqHost,
                    Port = config.RabitmqPort,
                    Username = config.RabitmqUsername,
                    Password = config.RabitmqPassword,
                    Exchange = config.RabitmqExchange,
                    Routekey = config.RabitmqRoutekey,
                }
            });
        }
        
        public static void Info(string msg)
        {
            instanceLogger.Info(msg);
        }
    
        public static void Debug(string msg)
        {
            instanceLogger.Debug(msg);
            
        }
   
        public static void Error(string msg)
        {
            instanceLogger.Error(msg);
        }
    
        public static void Error(string msg,Exception ex)
        {
            instanceLogger.Error(msg,ex);
        }
    }
}