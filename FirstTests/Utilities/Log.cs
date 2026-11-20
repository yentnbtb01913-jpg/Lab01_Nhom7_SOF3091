using System;
using System.IO;
using NUnit.Framework;

namespace FirstTests.Utilities
{
    public static class Log
    {
        private static readonly string logFolder =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs"
            );

        private static readonly string logFile =
            Path.Combine(logFolder, "test-log.txt");

        private static readonly object lockObject = new object();

        private static void WriteLog(string level, string message)
        {
            // Gộp thông báo nhiều dòng thành 1 dòng để log không bị vỡ
            message = (message ?? string.Empty)
                .Replace("\r", " ")
                .Replace("\n", " ");

            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            // T<số> = ID luồng, giúp thấy rõ khi chạy song song
            string logMessage =
                $"[{time}] [{level}] [T{Environment.CurrentManagedThreadId}] {message}";

            lock (lockObject)
            {
                Directory.CreateDirectory(logFolder);

                File.AppendAllText(
                    logFile,
                    logMessage + Environment.NewLine
                );
            }

            TestContext.Progress.WriteLine(logMessage);
        }

        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        public static void Error(string message)
        {
            WriteLog("ERROR", message);
        }
    }
}