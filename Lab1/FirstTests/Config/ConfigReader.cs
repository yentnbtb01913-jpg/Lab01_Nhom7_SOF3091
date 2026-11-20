using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FirstTests.Config;

public static class ConfigReader
{
    // BaseDirectory tro den bin/Debug/net10.0; khong phu thuoc thu muc chay lenh.
    public static TestConfig Load()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
        if (!File.Exists(path))
            throw new FileNotFoundException("Khong tim thay Config/appsettings.json trong output. Kiem tra CopyToOutputDirectory.", path);

        IConfigurationRoot source = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(Path.Combine("Config", "appsettings.json"), optional: false)
            .Build();

        TestConfig? cfg = source.Get<TestConfig>();
        if (cfg is null) throw new InvalidDataException("Khong doc duoc noi dung appsettings.json.");
        Validate(cfg);
        return cfg;
    }

    // Kiem tra gia tri cua config truoc khi khoi tao webdriver.
    public static void Validate(TestConfig cfg)
    {
        if (!Uri.TryCreate(cfg.BaseUrl, UriKind.Absolute, out _))
            throw new InvalidDataException("BaseUrl trong appsettings.json khong hop le.");
        if (string.IsNullOrWhiteSpace(cfg.Browser))
            throw new InvalidDataException("Browser trong appsettings.json khong duoc rong.");
        if (cfg.TimeoutSeconds < 1 || cfg.TimeoutSeconds > 120)
            throw new InvalidDataException("TimeoutSeconds phai trong khoang 1 den 120.");
        if (cfg.WindowWidth < 640 || cfg.WindowHeight < 480)
            throw new InvalidDataException("WindowWidth/WindowHeight qua nho.");
        foreach (var item in cfg.Sites) cfg.Site(item.Key);
    }
}
