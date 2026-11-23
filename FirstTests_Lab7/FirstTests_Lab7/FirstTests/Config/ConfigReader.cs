using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace FirstTests.Config
{
    public static class ConfigReader
    {
        public static TestConfig Load()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    Path.Combine("Config", "appsettings.json"),
                    optional: false,
                    reloadOnChange: false
                )
                .Build();

            return configuration.Get<TestConfig>()
                ?? throw new Exception("Không đọc được cấu hình!");
        }
    }
}