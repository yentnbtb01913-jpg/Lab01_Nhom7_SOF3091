using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace FirstTests.Reporting;

public sealed record TestRunRow(
    string FullName,
    string TestName,
    string TestClass,
    string Outcome,
    string Error,
    string Screenshot,
    double DurationSeconds,
    DateTimeOffset CompletedAt);

// Chi ghi trang thai that tu NUnit sau khi moi test chay xong.
public static class ReportRun
{
    private static readonly object Gate = new();
    private static readonly Dictionary<string, TestRunRow> Results = new();
    public static DateTimeOffset StartedAt { get; private set; } = DateTimeOffset.Now;

    public static void Start()
    {
        lock (Gate)
        {
            Results.Clear();
            StartedAt = DateTimeOffset.Now;
        }
    }

    public static void Record(double seconds, string? screenshot, string? overrideOutcome = null,
        string? overrideError = null)
    {
        var current = TestContext.CurrentContext;
        string fullName = current.Test.FullName;
        string outcome = overrideOutcome ?? current.Result.Outcome.Status.ToString();
        string message = overrideError ?? current.Result.Message ?? "";
        var row = new TestRunRow(
            fullName, current.Test.Name, current.Test.ClassName ?? "", outcome,
            message, screenshot ?? "", Math.Max(0, seconds), DateTimeOffset.Now);
        lock (Gate)
        {
            // Loi SetUp da ghi truoc khong duoc bi lan TearDown ghi de thanh Passed.
            if (Results.TryGetValue(fullName, out var previous) &&
                previous.Outcome == "Failed" && row.Outcome != "Failed")
                row = previous;
            Results[fullName] = row;
        }
    }

    public static IReadOnlyList<TestRunRow> Completed()
    {
        lock (Gate) return Results.Values.OrderBy(x => x.TestClass)
            .ThenBy(x => x.TestName).ToList();
    }
}
