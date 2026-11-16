using System;
using System.Collections.Generic;
using System.Text;

namespace FirstTests.Config;

public class TestConfig
{
    public string BaseUrl { get; set; } = "";
    public string TheInternetUrl { get; set; } = "";
    public string Browser { get; set; } = "chrome";
    public int TimeoutSeconds { get; set; } = 10;
    public bool Headless { get; set; }
    public int WindowWidth { get; set; } = 1920;
    public int WindowHeight { get; set; } = 1080;
}

