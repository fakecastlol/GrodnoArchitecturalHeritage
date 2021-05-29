using NLog;
using System;

namespace Logger.API.Logging
{
    public class LogNLog : ILog
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public LogNLog()
        {
        }

        public void Debug(string message)
        {
            var logger = LogManager.GetLogger("RmqTarget");
            var logEventInfo = new LogEventInfo(LogLevel.Debug, "RmqLogMessage", $"{message}, generated at {DateTime.UtcNow}.");
            logger.Log(logEventInfo);
            //LogManager.Shutdown();  
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }
    }
}
