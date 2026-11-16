using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
namespace FirstTests.Config;

public static class ConfigReader
{
    // Lazy: chỉ đọc file 1 lần cho cả phiên chạy và an toàn khi chạy song song
    private static readonly Lazy<TestConfig> cache = new(DocCauHinh);

    public static TestConfig Load() => cache.Value;

    private static TestConfig DocCauHinh()
    {
        var cauHinh = new ConfigurationBuilder()
            // AppContext.BaseDirectory luôn trỏ đúng thư mục bin/Debug/...
            // KHÔNG dùng Directory.GetCurrentDirectory(): nó đổi tùy cách chạy test.
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("Config/appsettings.json", optional: false)
            // Cho phép ghi đè từ dòng lệnh / CI mà không sửa file: TEST_Browser=firefox
            .AddEnvironmentVariables("TEST_")
            .Build();

        return cauHinh.Get<TestConfig>()
               ?? throw new InvalidOperationException(
                      "Không đọc được appsettings.json — kiểm tra CopyToOutputDirectory trong .csproj.");
    }
}
