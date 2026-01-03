using Common.Infrastructure.Context;
using Common.SharedConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.ContextFactory
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            IConfigurationBuilder localConfigBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true);

            IConfiguration localConfiguration = localConfigBuilder.Build();

            var sharedConfigPath = localConfiguration.GetValue<string>("SharedConfig:Path")!;

            SharedConfigurationLoader.SetSharedConfigPath(sharedConfigPath);

            IConfigurationBuilder fullConfigBuilder = new ConfigurationBuilder();
            ConfigurationModule.AddSharedConfiguration(fullConfigBuilder, "CommonInfrastructure");
            IConfiguration fullConfiguration = fullConfigBuilder.Build();

            string connectionString = fullConfiguration.GetConnectionString("DefaultConnection")!;
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
