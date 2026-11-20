using System;
using System.Collections.Generic;

namespace FirstTests.Config;

// Cac tham so duoc doc tu file JSON, khong dat URL trong source C#.
public sealed class TestConfig
{
    public string BaseUrl { get; set; } = string.Empty;
    public string Browser { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; }
    public bool Headless { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    public Dictionary<string, string> Sites { get; set; } = new();

    // Lay dia chi website phu theo khoa trong appsettings.json.
    public string Site(string name)
    {
        if (Sites.TryGetValue(name, out string? url) &&
            Uri.TryCreate(url, UriKind.Absolute, out _)) return url;
        throw new InvalidOperationException($"Thieu URL hop le cho Sites:{name} trong appsettings.json.");
    }
}
