# Chuyen file .trx thanh bao cao HTML hien dai, ho tro tieng Viet.
# Chay tai thu muc chua FirstTests.csproj:
# dotnet test --logger "trx;LogFileName=ketqua.trx"
# powershell -ExecutionPolicy Bypass -File Tools/trx-to-html.ps1

param(
    [string]$Trx = "TestResults/ketqua.trx",
    [string]$Out = "TestResults/baocao.html"
)

$ErrorActionPreference = "Stop"

if (-not (Test-Path -LiteralPath $Trx)) {
    Write-Error "Khong tim thay file $Trx. Hay chay dotnet test de tao file TRX truoc."
    exit 1
}

[xml]$x = Get-Content -LiteralPath $Trx -Raw -Encoding UTF8
$ns = New-Object System.Xml.XmlNamespaceManager($x.NameTable)
$ns.AddNamespace("t", "http://microsoft.com/schemas/VisualStudio/TeamTest/2010")

$counters = $x.SelectSingleNode("//t:ResultSummary/t:Counters", $ns)
$times = $x.SelectSingleNode("//t:Times", $ns)

$tong = [int]$counters.GetAttribute("total")
$dau = [int]$counters.GetAttribute("passed")
$rot = [int]$counters.GetAttribute("failed")
$khac = $tong - $dau - $rot

$batDau = [datetime]$times.GetAttribute("start")
$ketThuc = [datetime]$times.GetAttribute("finish")
$thoiGian = ($ketThuc - $batDau).ToString("hh\:mm\:ss")

# Anh xa testId voi ten class.
$lop = @{}
foreach ($u in $x.SelectNodes("//t:UnitTest", $ns)) {
    $method = $u.SelectSingleNode("t:TestMethod", $ns)
    if ($method) {
        $lop[$u.GetAttribute("id")] = $method.GetAttribute("className")
    }
}

# Thu thap ket qua tung test.
$dong = New-Object System.Collections.Generic.List[object]
foreach ($r in $x.SelectNodes("//t:UnitTestResult", $ns)) {
    $errorNode = $r.SelectSingleNode("t:Output/t:ErrorInfo/t:Message", $ns)
    $testId = $r.GetAttribute("testId")
    $className = if ($lop.ContainsKey($testId)) { $lop[$testId] } else { "Khong ro class" }

    $dong.Add([pscustomobject]@{
        Lop = $className
        Ten = $r.GetAttribute("testName")
        KetQua = $r.GetAttribute("outcome")
        ThoiGian = $r.GetAttribute("duration")
        Loi = if ($errorNode) { $errorNode.InnerText.Trim() } else { "" }
    })
}
$dong = $dong | Sort-Object Lop, Ten

function Enc([object]$value) {
    return [System.Net.WebUtility]::HtmlEncode([string]$value)
}

$rows = New-Object System.Text.StringBuilder
foreach ($d in $dong) {
    $statusClass = switch ($d.KetQua) {
        "Passed" { "passed" }
        "Failed" { "failed" }
        default { "other" }
    }
    $statusLabel = switch ($d.KetQua) {
        "Passed" { "Đạt" }
        "Failed" { "Không đạt" }
        "NotExecuted" { "Chưa chạy" }
        default { Enc $d.KetQua }
    }

    [void]$rows.AppendLine("<tr class='$statusClass'><td class='class-name'>$(Enc $d.Lop)</td><td class='test-name'>$(Enc $d.Ten)</td><td><span class='status $statusClass'>$statusLabel</span></td><td class='duration'>$(Enc $d.ThoiGian)</td></tr>")
    if ($d.Loi) {
        [void]$rows.AppendLine("<tr class='error-row'><td colspan='4'><details><summary>Chi tiết lỗi</summary><pre>$(Enc $d.Loi)</pre></details></td></tr>")
    }
}

$ngayLap = $batDau.ToString("dd/MM/yyyy HH:mm:ss")
$percent = if ($tong -gt 0) { [math]::Round(($dau / $tong) * 100, 1) } else { 0 }

$html = @"
<!DOCTYPE html>
<html lang="vi">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Báo cáo kiểm thử</title>
<style>
:root {
  font-family: "Segoe UI", Arial, sans-serif;
  color: #172033;
  background: #f4f6fb;
}
* { box-sizing: border-box; }
body { margin: 0; padding: 36px 22px; }
.container { max-width: 1440px; margin: 0 auto; }
.header {
  display: flex; justify-content: space-between; align-items: flex-end;
  gap: 20px; margin-bottom: 26px;
}
.eyebrow { color: #64748b; font-size: 12px; font-weight: 700; letter-spacing: 1.5px; text-transform: uppercase; }
h1 { margin: 7px 0 6px; font-size: 30px; letter-spacing: -.6px; }
.subtitle { color: #64748b; font-size: 14px; }
.summary {
  display: grid; grid-template-columns: repeat(4, minmax(150px, 1fr));
  gap: 16px; margin-bottom: 22px;
}
.card {
  background: #fff; border: 1px solid #e7eaf1; border-radius: 14px;
  padding: 19px 21px; box-shadow: 0 4px 14px rgba(20, 35, 70, .04);
}
.card-label { color: #64748b; font-size: 13px; font-weight: 600; }
.card-value { margin-top: 8px; font-size: 30px; line-height: 1.1; font-weight: 750; }
.total .card-value { color: #243b75; }
.passed-card .card-value { color: #16834a; }
.failed-card .card-value { color: #d33b43; }
.other-card .card-value { color: #64748b; }
.progress-wrap { background: #e8edf5; height: 7px; border-radius: 20px; margin-top: 14px; overflow: hidden; }
.progress { height: 100%; width: $percent%; background: #20a464; border-radius: 20px; }
.panel {
  background: #fff; border: 1px solid #e7eaf1; border-radius: 14px;
  box-shadow: 0 4px 14px rgba(20, 35, 70, .04); overflow: hidden;
}
.panel-head {
  display: flex; justify-content: space-between; align-items: center;
  gap: 16px; padding: 19px 22px; border-bottom: 1px solid #edf0f5;
}
.panel-title { font-size: 17px; font-weight: 700; }
.search {
  width: min(340px, 100%); padding: 10px 13px; border: 1px solid #dce2ec;
  border-radius: 9px; outline: none; font: inherit; font-size: 13px;
}
.search:focus { border-color: #6685d8; box-shadow: 0 0 0 3px #6685d81c; }
.table-wrap { overflow-x: auto; }
table { width: 100%; border-collapse: collapse; min-width: 760px; }
th {
  padding: 13px 18px; text-align: left; color: #64748b; background: #f8f9fc;
  font-size: 12px; font-weight: 700; text-transform: uppercase; letter-spacing: .5px;
}
td { padding: 13px 18px; border-top: 1px solid #edf0f5; font-size: 13px; vertical-align: top; }
tbody tr:hover:not(.error-row) { background: #fafbfe; }
.class-name { color: #64748b; }
.test-name { color: #24324a; font-weight: 600; }
.duration { color: #64748b; white-space: nowrap; font-variant-numeric: tabular-nums; }
.status { display: inline-block; padding: 5px 9px; border-radius: 20px; font-size: 11px; font-weight: 750; }
.status.passed { color: #137a43; background: #e7f7ee; }
.status.failed { color: #bd3038; background: #ffebec; }
.status.other { color: #596579; background: #eef1f5; }
.error-row td { background: #fff7f7; padding: 10px 18px 14px; }
details summary { color: #bd3038; cursor: pointer; font-size: 12px; font-weight: 650; }
pre { white-space: pre-wrap; overflow-wrap: anywhere; color: #9f2830; font: 12px/1.55 Consolas, monospace; margin: 10px 0 0; }
.footer { text-align: center; color: #94a0b2; font-size: 12px; padding: 22px 0 4px; }
.empty { padding: 25px; color: #64748b; text-align: center; }
@media (max-width: 760px) {
  body { padding: 22px 12px; }
  .header { display: block; }
  h1 { font-size: 25px; }
  .summary { grid-template-columns: repeat(2, minmax(0, 1fr)); gap: 10px; }
  .card { padding: 15px; }
  .card-value { font-size: 25px; }
  .panel-head { align-items: stretch; flex-direction: column; }
  .search { width: 100%; }
}
</style>
</head>
<body>
<div class="container">
  <header class="header">
    <div>
      <div class="eyebrow">FirstTests · Test Runner</div>
      <h1>Báo cáo kiểm thử</h1>
      <div class="subtitle">Thời điểm chạy: $ngayLap &nbsp;·&nbsp; Tổng thời gian: $thoiGian</div>
    </div>
  </header>

  <section class="summary">
    <div class="card total">
      <div class="card-label">Tổng số ca</div>
      <div class="card-value">$tong</div>
      <div class="progress-wrap"><div class="progress"></div></div>
    </div>
    <div class="card passed-card">
      <div class="card-label">Đạt (Passed)</div>
      <div class="card-value">$dau</div>
    </div>
    <div class="card failed-card">
      <div class="card-label">Không đạt (Failed)</div>
      <div class="card-value">$rot</div>
    </div>
    <div class="card other-card">
      <div class="card-label">Khác / Bỏ qua</div>
      <div class="card-value">$khac</div>
    </div>
  </section>

  <section class="panel">
    <div class="panel-head">
      <div class="panel-title">Chi tiết các ca kiểm thử</div>
      <input id="search" class="search" type="search" placeholder="Tìm theo tên test hoặc class...">
    </div>
    <div class="table-wrap">
      <table>
        <thead><tr><th>Class</th><th>Ca kiểm thử</th><th>Kết quả</th><th>Thời gian</th></tr></thead>
        <tbody id="testRows">
          $($rows.ToString())
        </tbody>
      </table>
      <div id="empty" class="empty" hidden>Không tìm thấy ca kiểm thử phù hợp.</div>
    </div>
  </section>
  <div class="footer">Báo cáo được tạo tự động từ kết quả kiểm thử TRX.</div>
</div>
<script>
const search = document.getElementById('search');
const body = document.getElementById('testRows');
const empty = document.getElementById('empty');
search.addEventListener('input', () => {
  const q = search.value.toLocaleLowerCase('vi');
  const rows = Array.from(body.querySelectorAll('tr')).filter(r => !r.classList.contains('error-row'));
  let visible = 0;
  rows.forEach(r => {
    const match = r.innerText.toLocaleLowerCase('vi').includes(q);
    r.hidden = !match;
    const next = r.nextElementSibling;
    if (next && next.classList.contains('error-row')) next.hidden = !match;
    if (match) visible++;
  });
  empty.hidden = visible !== 0;
});
</script>
</body>
</html>
"@

# Ghi file bang UTF-8 co BOM de Windows PowerShell va trinh duyet doc dung tieng Viet.
$outFull = [System.IO.Path]::GetFullPath($Out)
$outDir = [System.IO.Path]::GetDirectoryName($outFull)
if (-not (Test-Path -LiteralPath $outDir)) {
    New-Item -ItemType Directory -Force -Path $outDir | Out-Null
}
$utf8Bom = New-Object System.Text.UTF8Encoding($true)
[System.IO.File]::WriteAllText($outFull, $html, $utf8Bom)

Write-Host "Da tao bao cao: $outFull"
Write-Host "Tong: $tong | Dat: $dau | Khong dat: $rot | Khac: $khac"
