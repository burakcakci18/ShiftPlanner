using Common.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SharedConfiguration
{
    public static class ConfigurationModule
    {
        public static IConfigurationBuilder AddSharedConfiguration(this IConfigurationBuilder builder, string serviceName)
        {
            var logger = new LoggingManager();

            var path = SharedConfigurationLoader.GetSharedConfigPath(logger);
            return builder.AddJsonFile(path, optional: false, reloadOnChange: true);
        }
    }
}
