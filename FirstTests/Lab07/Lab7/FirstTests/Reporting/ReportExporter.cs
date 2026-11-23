using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using NUnit.Framework;

namespace FirstTests.Reporting;

internal sealed class EvidenceRule
{
    public string Match { get; set; } = "";
    public string FileName { get; set; } = "";
    public string Part { get; set; } = "";
    public string Steps { get; set; } = "";
    public string Expected { get; set; } = "";
    public string Precondition { get; set; } = "";
}

internal sealed record ReportCase(
    TestRunRow Run, string Id, string Part, string Steps,
    string Expected, string Precondition);

public static class ReportExporter
{
    public static void Write(IReadOnlyList<TestRunRow> runs, DateTimeOffset started)
    {
        string root = ProjectPaths.LabRoot();
        string lab = Path.GetFileName(root);
        string directory = Path.Combine(root, "TestReport");
        Directory.CreateDirectory(directory);
        string cfg = Path.Combine(AppContext.BaseDirectory, "EvidenceNames.json");
        EvidenceRule[] rules = File.Exists(cfg)
            ? JsonSerializer.Deserialize<EvidenceRule[]>(File.ReadAllText(cfg)) ?? Array.Empty<EvidenceRule>()
            : Array.Empty<EvidenceRule>();
        // Metadata chỉ để diễn giải kịch bản; các trạng thái thực tế luôn lấy từ NUnit.
        string browserName;
        try { browserName = FirstTests.Config.ConfigReader.Load().Browser; }
        catch { browserName = "ChuaDocCauHinh"; }
        List<ReportCase> cases = runs.Select(r =>
        {
            var rule = rules.FirstOrDefault(x =>
                r.FullName.Contains(x.Match, StringComparison.OrdinalIgnoreCase) ||
                r.TestName.Contains(x.Match, StringComparison.OrdinalIgnoreCase));
            return new ReportCase(r,
                (rule?.FileName.Replace("<Browser>", browserName) ?? "TC")
                    + "_" + System.Text.RegularExpressions.Regex.Replace(
                        r.TestClass.Split('.').Last() + "_" + r.TestName, @"[^a-zA-Z0-9_-]", "_"),
                rule?.Part ?? "ChuaPhanLoai",
                rule?.Steps ?? "Chay test NUnit: " + r.TestName,
                rule?.Expected ?? "Xem Assert trong ca test NUnit tương ứng",
                rule?.Precondition ?? "Da cai .NET SDK va co the chay Selenium WebDriver");
        }).ToList();
        // Một lần nhấn Run (Run All hoặc Run Selected) = một cặp tệp MỚI.
        // Không tích lũy kết quả của lần chạy trước, không ghi đè dù ca Failed.
        // Nếu đã có TestReport.pdf thì dùng TestReport1.pdf, TestReport2.pdf...
        int index = 0;
        string suffix;
        string excelPath;
        string pdfPath;
        string? htmlPath;
        do
        {
            suffix = index == 0 ? "" : index.ToString(CultureInfo.InvariantCulture);
            excelPath = Path.Combine(directory, "TestReport" + suffix + ".xlsx");
            pdfPath = Path.Combine(directory, "TestReport" + suffix + ".pdf");
            htmlPath = lab == "Lab7" ? Path.Combine(directory, "TestReport" + suffix + ".html") : null;
            index++;
        }
        while (File.Exists(pdfPath) || File.Exists(excelPath) ||
            (htmlPath != null && File.Exists(htmlPath)));

        // Tạo tệp tạm để tránh xuất được PDF nhưng Excel lại thiếu khi gặp lỗi.
        string tempId = Guid.NewGuid().ToString("N");
        string tempPdf = Path.Combine(directory, "._" + tempId + ".pdf");
        string tempXlsx = Path.Combine(directory, "._" + tempId + ".xlsx");
        string? tempHtml = htmlPath != null ? Path.Combine(directory, "._" + tempId + ".html") : null;
        try
        {
            WriteExcel(cases, lab, started, tempXlsx);
            WritePdf(cases, lab, started, tempPdf);
            if (tempHtml != null) WriteHtml(cases, lab, started, tempHtml);

            File.Move(tempXlsx, excelPath, overwrite: false);
            File.Move(tempPdf, pdfPath, overwrite: false);
            if (tempHtml != null && htmlPath != null)
                File.Move(tempHtml, htmlPath, overwrite: false);
        }
        finally
        {
            if (File.Exists(tempXlsx)) File.Delete(tempXlsx);
            if (File.Exists(tempPdf)) File.Delete(tempPdf);
            if (tempHtml != null && File.Exists(tempHtml)) File.Delete(tempHtml);
        }
        TestContext.Progress.WriteLine("[TESTREPORT] " + pdfPath);
        TestContext.Progress.WriteLine("[TESTREPORT] " + excelPath);
        if (htmlPath != null) TestContext.Progress.WriteLine("[TESTREPORT] " + htmlPath);
    }

    private static void WriteExcel(List<ReportCase> cases, string lab, DateTimeOffset started, string path)
    {
        using var book = new XLWorkbook();
        var detail = book.Worksheets.Add("TestCases");
        var summary = book.Worksheets.Add("TongHop");
        string[] headers = {
            "STT", "Mã Test Case", "Phần", "Tên ca kiểm thử", "Điều kiện trước", "Các bước kiểm thử",
            "Kết quả mong đợi", "Kết quả thực tế", "Trạng thái", "Thời gian (giây)",
            "Thời điểm hoàn thành", "Ảnh PNG", "Thông báo lỗi", "NUnit FullName"
        };
        detail.Range(1, 1, 1, headers.Length).Merge();
        detail.Cell(1, 1).Value = $"TEST CASE & KẾT QUẢ - {lab.ToUpperInvariant()} - SOF3091";
        detail.Range(1, 1, 1, headers.Length).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#17314F"));
        detail.Range(1, 1, 1, headers.Length).Style.Font.SetFontColor(XLColor.White).Font.SetBold();
        detail.Range(1, 1, 1, headers.Length).Style.Font.FontSize = 16;
        detail.Row(1).Height = 31;
        detail.Range(2, 1, 2, headers.Length).Merge();
        detail.Cell(2, 1).Value = "Trần Nguyễn Bình Yên  |  TB01913  |  Nhóm 07";
        for (int j = 0; j < headers.Length; j++) detail.Cell(4, j + 1).Value = headers[j];
        var heading = detail.Range(4, 1, 4, headers.Length);
        heading.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#087E8B"));
        heading.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
        heading.Style.Alignment.WrapText = true;
        detail.Row(4).Height = 33;
        for (int i = 0; i < cases.Count; i++)
        {
            var c = cases[i]; int row = i + 5;
            string actual = c.Run.Outcome.Equals("Passed", StringComparison.OrdinalIgnoreCase)
                ? "Passed (NUnit)"
                : string.IsNullOrWhiteSpace(c.Run.Error)
                    ? c.Run.Outcome + " (NUnit)"
                    : c.Run.Error;
            string[] values = {
                (i+1).ToString(), c.Id, c.Part, c.Run.TestName,
                c.Precondition, c.Steps, c.Expected, actual, c.Run.Outcome,
                c.Run.DurationSeconds.ToString("0.000", CultureInfo.InvariantCulture),
                c.Run.CompletedAt.ToString("dd/MM/yyyy HH:mm:ss"),
                string.IsNullOrWhiteSpace(c.Run.Screenshot) ? "Không có ảnh" : c.Run.Screenshot,
                c.Run.Error, c.Run.FullName
            };
            for (int j = 0; j < values.Length; j++) detail.Cell(row, j+1).Value = values[j];
            if (i % 2 == 1) detail.Range(row, 1, row, headers.Length)
                .Style.Fill.SetBackgroundColor(XLColor.FromHtml("#F3F7FA"));
            var status = detail.Cell(row, 9);
            status.Style.Fill.SetBackgroundColor(
                c.Run.Outcome == "Passed" ? XLColor.FromHtml("#D9F4DC") :
                c.Run.Outcome == "Failed" ? XLColor.FromHtml("#FCE1DF") :
                XLColor.FromHtml("#F4E9CC"));
            status.Style.Font.SetBold();
            detail.Row(row).Height = 38;
        }
        detail.Range(4, 1, Math.Max(5, cases.Count+4), headers.Length)
            .Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        detail.Range(5, 1, Math.Max(5, cases.Count+4), headers.Length).Style.Alignment.WrapText = true;
        if (cases.Count > 0) detail.Range(4, 1, cases.Count+4, headers.Length).SetAutoFilter();
        detail.SheetView.FreezeRows(4);
        double[] widths = {7,30,16,44,37,43,42,48,14,18,25,55,45,70};
        for (int j = 0; j < widths.Length; j++) detail.Column(j + 1).Width = widths[j];

        summary.Range("A1:F1").Merge();
        summary.Cell("A1").Value = "TỔNG HỢP KẾT QUẢ TEST";
        summary.Range("A1:F1").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#17314F"));
        summary.Range("A1:F1").Style.Font.SetFontColor(XLColor.White).Font.SetBold();
        summary.Range("A1:F1").Style.Font.FontSize = 17;
        summary.Cell("A3").Value = "Bài thực hành"; summary.Cell("B3").Value = lab;
        summary.Cell("A4").Value = "Họ và tên"; summary.Cell("B4").Value = "Trần Nguyễn Bình Yên";
        summary.Cell("A5").Value = "MSSV / Nhóm"; summary.Cell("B5").Value = "TB01913 / 07";
        summary.Cell("A6").Value = "Bắt đầu"; summary.Cell("B6").Value = started.ToString("dd/MM/yyyy HH:mm:ss zzz");
        summary.Cell("A8").Value = "Tổng ca thực thi"; summary.Cell("B8").Value = cases.Count;
        summary.Cell("A9").Value = "Passed"; summary.Cell("B9").Value = cases.Count(x=>x.Run.Outcome=="Passed");
        summary.Cell("A10").Value = "Failed"; summary.Cell("B10").Value = cases.Count(x=>x.Run.Outcome=="Failed");
        summary.Cell("A11").Value = "Khác"; summary.Cell("B11").Value = cases.Count(x=>x.Run.Outcome!="Passed" && x.Run.Outcome!="Failed");
        summary.Cell("A13").Value = "Chú thích";
        summary.Cell("B13").Value = "Chỉ thống kê các ca thật sự thực thi qua NUnit trong lượt chạy này.";
        summary.Range("A8:B11").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#E8F1F6"));
        summary.Range("A3:A13").Style.Font.SetBold();
        summary.Column("A").Width = 25; summary.Column("B").Width = 85;
        book.SaveAs(path);
    }

    private static void WritePdf(List<ReportCase> cases, string lab, DateTimeOffset started, string path)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        int passed = cases.Count(x => x.Run.Outcome == "Passed");
        int failed = cases.Count(x => x.Run.Outcome == "Failed");
        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(26);
                page.DefaultTextStyle(style => style.FontFamily("Arial").FontSize(9));
                page.Header().Column(h =>
                {
                    h.Item().Text("Test Report").FontSize(24).Bold().FontColor("#17314F");
                    h.Item().Text($"{lab}  |  Trần Nguyễn Bình Yên  |  TB01913  |  Nhóm 07")
                        .FontSize(10).FontColor("#526274");
                    h.Item().Text($"Started: {started:yyyy-MM-ddTHH:mm:sszzz}")
                        .FontSize(8).FontColor("#68788B");
                });
                page.Content().PaddingTop(15).Column(col =>
                {
                    col.Spacing(12);
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Background("#EDF1F4").Padding(12).Column(b =>
                        {
                            b.Item().Text($"Test Classes  ({cases.Select(x=>x.Run.TestClass).Distinct().Count()})").Bold();
                            b.Item().Text("Lớp kiểm thử trong lượt chạy").FontSize(8);
                        });
                        row.ConstantItem(12);
                        row.RelativeItem().Background("#EDF1F4").Padding(12).Column(b =>
                        {
                            b.Item().Text($"Test Cases  ({cases.Count})").Bold();
                            b.Item().Text($"{passed} passed").FontColor("#257A39");
                            b.Item().Text($"{failed} failed").FontColor("#A53636");
                            b.Item().Text($"{cases.Count-passed-failed} other").FontColor("#697684");
                        });
                    });
                    foreach (var group in cases.GroupBy(x=>x.Run.TestClass))
                    {
                        col.Item().Background("#EEF0F2").Padding(9).Text(group.Key).FontSize(10).FontColor("#61707A");
                        foreach (var item in group)
                        {
                            bool ok = item.Run.Outcome == "Passed";
                            string background = ok ? "#E2F2C7" : item.Run.Outcome == "Failed" ? "#F9DAD7" : "#F3E9D1";
                            col.Item().Background(background).Padding(7).Column(line =>
                            {
                                line.Item().Row(row =>
                                {
                                    row.RelativeItem(4).Text(item.Run.TestName).Bold().FontSize(8);
                                    row.ConstantItem(64).Text(item.Run.Outcome).FontColor(ok ? "#257A39" : "#9A3838").FontSize(8);
                                    row.ConstantItem(78).AlignRight().Text($"{item.Run.DurationSeconds:0.000} s").FontSize(8);
                                });
                                // Giữ nguyên tên đầy đủ và thông báo lỗi để đối chiếu Test Explorer.
                                line.Item().Text("NUnit FullName: " + item.Run.FullName)
                                    .FontSize(7).FontColor("#637486");
                                if (!string.IsNullOrWhiteSpace(item.Run.Error))
                                    line.Item().Text("Lỗi NUnit: " + item.Run.Error)
                                        .FontSize(7).FontColor("#8A3333");
                            });
                        }
                    }
                    col.Item().Text("Kết quả chỉ lấy từ lượt chạy NUnit hiện tại. Ảnh PNG lưu trong thư mục screenshots/.")
                        .Italic().FontSize(8).FontColor("#61707A");
                });
                page.Footer().AlignCenter().Text(t =>
                {
                    t.Span("Trang "); t.CurrentPageNumber(); t.Span(" / "); t.TotalPages();
                });
            });
        }).GeneratePdf(path);
    }


    // HTML hỗ trợ xem kết quả ngay khi Run trong Visual Studio.
    // Dữ liệu lấy từ chính kết quả NUnit, KHÔNG được gọi là HTML chuyển đổi TRX.
    // HTML chỉ hỗ trợ đọc nhanh. Lab7 không yêu cầu chuyển đổi TRX sang HTML.
    private static void WriteHtml(List<ReportCase> cases, string lab, DateTimeOffset started, string path)
    {
        static string E(string? value) => System.Net.WebUtility.HtmlEncode(value ?? "");
        int passed = cases.Count(c => c.Run.Outcome == "Passed");
        int failed = cases.Count(c => c.Run.Outcome == "Failed");
        var sb = new System.Text.StringBuilder();
        sb.AppendLine("<!doctype html><html lang=\"vi\"><head><meta charset=\"utf-8\"><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\">");
        sb.AppendLine("<title>Test Report - " + E(lab) + "</title><style>body{font-family:system-ui,Segoe UI,Arial,sans-serif;color:#18283e;background:#f8fafc;max-width:1200px;margin:28px auto;padding:0 18px}h1{font-size:30px}p{color:#64748b}.summary{display:flex;gap:18px;margin:25px 0}.tile{background:#fff;padding:18px 28px;border:1px solid #e2e8f0;border-radius:12px;min-width:130px}.tile strong{font-size:25px;display:block}.ok{color:#21763b}.fail{color:#b02929}.group{margin:25px 0 8px;padding:13px;background:#e5eaf0;border-radius:6px}table{border-collapse:collapse;width:100%;background:#fff;margin-bottom:20px}th,td{border-bottom:1px solid #e2e8f0;padding:13px;text-align:left;vertical-align:top;overflow-wrap:anywhere}th{background:#f0f3f6}tr.passed{background:#eef8e8}tr.failed{background:#ffe9e7}.trace{font-family:Consolas,monospace;white-space:pre-wrap;font-size:12px;max-width:550px}</style></head><body>");
        sb.AppendLine("<h1>Test Report</h1><p>" + E(lab) + " | Trần Nguyễn Bình Yên | TB01913 | Nhóm 07</p><p>Started: " + E(started.ToString("yyyy-MM-ddTHH:mm:sszzz")) + "</p>");
        sb.AppendLine("<div class=\"summary\"><div class=\"tile\">Test Classes<strong>" + cases.Select(c=>c.Run.TestClass).Distinct().Count() + "</strong></div><div class=\"tile\">Test Cases<strong>" + cases.Count + "</strong></div><div class=\"tile ok\">Passed<strong>" + passed + "</strong></div><div class=\"tile fail\">Failed<strong>" + failed + "</strong></div><div class=\"tile\">Other<strong>" + (cases.Count-passed-failed) + "</strong></div></div>");
        foreach (var group in cases.GroupBy(c=>c.Run.TestClass))
        {
            sb.AppendLine("<h3 class=\"group\">" + E(group.Key) + "</h3><table><thead><tr><th>Ca kiểm thử</th><th>Trạng thái (NUnit)</th><th>Giây</th><th>Lỗi NUnit</th></tr></thead><tbody>");
            foreach (var c in group)
            {
                string css = c.Run.Outcome == "Passed" ? "passed" : c.Run.Outcome == "Failed" ? "failed" : "";
                sb.AppendLine("<tr class=\""+css+"\"><td title=\""+E(c.Run.FullName)+"\">"+E(c.Run.TestName)+"</td><td>"+E(c.Run.Outcome)+"</td><td>"+c.Run.DurationSeconds.ToString("0.000",CultureInfo.InvariantCulture)+"</td><td><div class=\"trace\">"+E(c.Run.Error)+"</div></td></tr>");
            }
            sb.AppendLine("</tbody></table>");
        }
        sb.AppendLine("<p>Trạng thái và lỗi lấy từ NUnit khi Run Test. Bản HTML này được tạo trực tiếp từ NUnit; khi cần minh chứng chuyển đổi .trx, hãy thực hiện bước xuất TRX ở README.</p></body></html>");
        File.WriteAllText(path, sb.ToString(), System.Text.Encoding.UTF8);
    }

}
