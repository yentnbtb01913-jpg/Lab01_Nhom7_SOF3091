namespace FirstTests.Utilities;

// Nhật ký dùng chung cho cả framework.
// Mỗi dòng có: thời gian + mức độ + số luồng (để nhìn thấy khi chạy song song).
public static class Log
{
    public static void Info(string msg)  => Ghi("INFO ", msg);

    public static void Warn(string msg)  => Ghi("WARN ", msg);

    public static void Error(string msg) => Ghi("ERROR", msg);

    private static void Ghi(string mucDo, string msg)
    {
        // TestContext.Progress có sẵn trong NUnit, không cần cài thư viện log
        TestContext.Progress.WriteLine(
            $"[{DateTime.Now:HH:mm:ss.fff}] [{mucDo}] [T{Environment.CurrentManagedThreadId:00}] {msg}");
    }
}
