using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging.IoC
{
    public static class LoggingModule
    {
        public static void RegisterLoggingModule(this IServiceCollection services)
        {
            services.AddSingleton<ILogging, LoggingManager>();
        }

        public static void AddLoggingModule(this IServiceCollection services, WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Logging.ClearProviders();
            webApplicationBuilder.Logging.SetMinimumLevel(LogLevel.Trace);
            webApplicationBuilder.Host.UseNLog();
        }
    }
}
