using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class LoggingManager : ILogging
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public LoggingManager()
        {
            // Logger zaten static olarak tanımlı.
        }

        public void Initialize(string serviceName)
        {
            if (_isInitialized)
                return;

            lock (_lock)
            {
                if (_isInitialized)
                    return;

                LogManager.Setup().LoadConfigurationFromFile("nlog.config");

                if (LogManager.Configuration.Variables.ContainsKey("serviceName"))
                    LogManager.Configuration.Variables["serviceName"] = serviceName;
                else
                    LogManager.Configuration.Variables.Add("serviceName", serviceName);

                LogManager.ReconfigExistingLoggers();

                logger = LogManager.GetLogger(serviceName);
                _isInitialized = true;
            }
        }

        public void Trace(string message) => logger.Trace(message);
        public void Debug(string message) => logger.Debug(message);
        public void Info(string message) => logger.Info(message);
        public void Warn(string message) => logger.Warn(message);
        public void Error(string message, Exception exception = null!) => logger.Error(exception, message);
        public void Fatal(string message, Exception exception = null!) => logger.Fatal(exception, message);
    }
}
