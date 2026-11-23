using System;
using NUnit.Framework;
using FirstTests.Reporting;

// SetUpFixture nam ngoai namespace: chay cho TOAN BO test assembly.
// NUnit goi OneTimeTearDown ke ca khi bam Run Selected Tests trong Test Explorer.
[SetUpFixture]
public sealed class LabReportLifecycle
{
    [OneTimeSetUp]
    public void BeginRun() => ReportRun.Start();

    [OneTimeTearDown]
    public void ExportReports()
    {
        var results = ReportRun.Completed();
        if (results.Count == 0)
        {
            TestContext.Progress.WriteLine(
                "Khong co test nao hoan thanh: khong tao bao cao gia.");
            return;
        }
        try
        {
            ReportExporter.Write(results, ReportRun.StartedAt);
        }
        catch (Exception ex)
        {
            TestContext.Progress.WriteLine("LOI XUAT TESTREPORT: " + ex);
            // Lỗi xuất tệp không được làm đổi trạng thái NUnit của các bài test đã chạy.
            // Lỗi được hiển thị rõ trên Progress; không tạo báo cáo PASS giả.
            try
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(ProjectPaths.LabRoot(), "TestReport", "LoiXuatBaoCao.log"),
                    $"[{System.DateTimeOffset.Now:O}] {ex}{System.Environment.NewLine}");
            }
            catch { /* Khi ổ đĩa không ghi được, vẫn hiển thị lỗi trên Progress */ }
        }
    }
}
