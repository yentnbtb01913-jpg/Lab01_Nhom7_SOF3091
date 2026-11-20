using System;
using System.IO;
using NUnit.Framework;

namespace FirstTests.Utilities
{
    public static class Log
    {
        private static readonly string LogFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private static readonly string LogFile =
            Path.Combine(LogFolder, "test-log.txt");

        // Bảo vệ thao tác ghi file khi nhiều fixture chạy song song.
        private static readonly object LockObject = new();

        private static void WriteLog(string level, string message)
        {
            string safeMessage = (message ?? string.Empty)
                .Replace("\r", " ")
                .Replace("\n", " ");

            string line =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level}] " +
                $"[T{Environment.CurrentManagedThreadId}] {safeMessage}";

            lock (LockObject)
            {
                Directory.CreateDirectory(LogFolder);
                File.AppendAllText(LogFile, line + Environment.NewLine);
            }

            TestContext.Progress.WriteLine(line);
        }

        public static void Info(string message) => WriteLog("INFO", message);

        public static void Error(string message) => WriteLog("ERROR", message);
    }
}
