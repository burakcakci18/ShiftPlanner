using Common.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.SharedConfiguration
{
    public static class SharedConfigurationLoader
    {
        private static string? _sharedConfigPath;

        public static void SetSharedConfigPath(string relativePath, ILogging? logger = null)
        {
            var basePath = AppContext.BaseDirectory;
            var fullPath = Path.GetFullPath(Path.Combine(basePath, relativePath));

            if (!File.Exists(fullPath))
            {
                logger?.Error("Shared config not found: " + fullPath);
                throw new FileNotFoundException("Shared config not found", fullPath);
            }

            _sharedConfigPath = fullPath;
            logger?.Info("Shared config path set: " + fullPath);
        }

        public static string GetSharedConfigPath(ILogging? logger = null)
        {
            if (string.IsNullOrWhiteSpace(_sharedConfigPath))
            {
                logger?.Error("Shared configuration path not initialized. Call SetSharedConfigPath first.");
                throw new InvalidOperationException("Shared configuration path not initialized.");
            }

            return _sharedConfigPath;
        }

        public static string GetCode(string key, ILogging? logger = null, bool isEncrypted = false)
        {
            var path = GetSharedConfigPath(logger);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();

            var value = configuration[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                logger?.Warn($"Key '{key}' not found in shared configuration.");
                throw new KeyNotFoundException($"Key '{key}' not found in shared configuration.");
            }
            else
            {
                logger?.Info($"Key '{key}' resolved with value: {value}");
            }

            return value;
        }


        public static List<T> GetSectionList<T>(string key, ILogging? logger = null) where T : class, new()
        {
            var path = GetSharedConfigPath(logger);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();

            var section = configuration.GetSection(key);
            var items = section.Get<List<T>>();

            if (items == null || !items.Any())
            {
                logger?.Warn($"No configuration found under '{key}' section.");
                throw new InvalidOperationException($"No configuration found under '{key}' section.");
            }

            logger?.Info($"{items.Count} item(s) loaded from '{key}' section.");
            return items;
        }

        public static T GetSectionObject<T>(string key, ILogging? logger = null) where T : class, new()
        {
            var path = GetSharedConfigPath(logger);

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();

            var item = configuration.GetSection(key).Get<T>();

            if (item == null)
            {
                logger?.Warn($"Configuration for '{key}' is missing or invalid.");
                throw new InvalidOperationException($"Configuration for '{key}' is missing or invalid.");
            }

            logger?.Info($"Configuration for '{key}' loaded successfully.");
            return item;
        }


        public static string GetEnvironment()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            return environment;
        }

    }

    public class ServicesConfig
    {
        public string[] AllowedExtensions { get; set; } = [];
        public string[] AllowedMimeTypes { get; set; } = [];
    }
}
