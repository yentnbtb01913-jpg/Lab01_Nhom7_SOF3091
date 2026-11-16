namespace FirstTests.Config;

// Lớp ánh xạ với appsettings.json. Tên thuộc tính PHẢI khớp tên khóa trong JSON.
public class TestConfig
{
    public string BaseUrl        { get; set; } = "";
    public string TheInternetUrl { get; set; } = "";
    public string Browser        { get; set; } = "chrome";
    public int    TimeoutSeconds { get; set; } = 10;
    public bool   Headless       { get; set; }
    public int    WindowWidth    { get; set; } = 1920;
    public int    WindowHeight   { get; set; } = 1080;
}
