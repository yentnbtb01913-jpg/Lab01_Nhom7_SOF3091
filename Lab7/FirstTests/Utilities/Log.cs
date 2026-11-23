using System;
using System.IO;
using NUnit.Framework;
using FirstTests.Reporting;

namespace FirstTests.Utilities;

public static class Log
{
    private static readonly object Gate = new();
    private static string? filePath;

    // Một lượt Run tạo một log riêng; không ghi đè lần chạy trước.
    public static void Begin(DateTimeOffset started)
    {
        lock (Gate)
        {
            string folder = ProjectPaths.EnsureOutputDirectory("TestReport");
            string stem = "NhatKy_" + started.ToString("yyyyMMdd_HHmmss_fff");
            string proposed = Path.Combine(folder, stem + ".log");
            int index = 1;
            while (File.Exists(proposed))
                proposed = Path.Combine(folder, stem + "_" + index++ + ".log");
            filePath = proposed;
            File.AppendAllText(filePath, "[INFO] Bắt đầu lượt chạy: " + started.ToString("O") + Environment.NewLine);
        }
    }

    public static void Info(string message) => Write("INFO", message);
    public static void Error(string message) => Write("ERROR", message);
    private static void Write(string level, string message)
    {
        string line = $"[{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz}] [{level}] {message}";
        lock (Gate)
        {
            TestContext.Progress.WriteLine(line);
            if (filePath != null) File.AppendAllText(filePath, line + Environment.NewLine);
        }
    }
}
