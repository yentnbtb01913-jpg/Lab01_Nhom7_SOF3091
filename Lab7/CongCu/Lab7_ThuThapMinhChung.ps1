<#
.SYNOPSIS
  Thu thập ảnh minh chứng còn thiếu cho báo cáo Lab 7 (SOF3091).

.DESCRIPTION
  Phần tự động : chạy dotnet build rồi dotnet test toàn bộ và chụp cửa sổ terminal  -> C08_Terminal_BuildVaTest.png
  Phần thủ công: hướng dẫn từng bước rồi tự chụp cửa sổ đang mở                      -> D04, E01, E02a
  Ảnh được lưu trong screenshots\minhchung, tên file trùng "Tên gợi ý" trong các khung của file Word.

.EXAMPLE
  # Đặt file này cạnh Lab7.slnx, mở PowerShell tại thư mục Lab7 rồi chạy:
  powershell -ExecutionPolicy Bypass -File .\Lab7_ThuThapMinhChung.ps1

.EXAMPLE
  .\Lab7_ThuThapMinhChung.ps1 -BoQuaTuDong            # chỉ chụp các ảnh thủ công
  .\Lab7_ThuThapMinhChung.ps1 -BoQuaThuCong           # chỉ chạy build/test và chụp C08
  .\Lab7_ThuThapMinhChung.ps1 -ChayLaiDebug           # chụp thêm terminal của D01 (Failed) và D02 (Passed)
#>
[CmdletBinding()]
param(
    [string]$Root,
    [string]$OutDir,
    [switch]$BoQuaTuDong,
    [switch]$BoQuaThuCong,
    [switch]$ChayLaiDebug
)

Set-StrictMode -Version Latest
# Lệnh native (dotnet) ghi cảnh báo ra stderr; để Continue và tự kiểm tra $LASTEXITCODE.
$ErrorActionPreference = 'Continue'

if (-not $Root) {
    if ($PSScriptRoot) { $Root = $PSScriptRoot } else { $Root = (Get-Location).Path }
}
if (-not $OutDir) { $OutDir = Join-Path $Root 'screenshots\minhchung' }

$sln = Get-ChildItem -LiteralPath $Root -Filter 'Lab7.sln*' -File -ErrorAction SilentlyContinue | Select-Object -First 1
if (-not $sln) {
    Write-Host "Không thấy Lab7.slnx trong: $Root" -ForegroundColor Red
    Write-Host 'Hãy đặt script cạnh Lab7.slnx hoặc truyền -Root <thư mục Lab7>.' -ForegroundColor Red
    exit 1
}
if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Host 'Không tìm thấy lệnh dotnet. Cài .NET 10 SDK rồi chạy lại.' -ForegroundColor Red
    exit 1
}
New-Item -ItemType Directory -Force -Path $OutDir | Out-Null
$logDir = Join-Path $OutDir 'logs'
New-Item -ItemType Directory -Force -Path $logDir | Out-Null

# ---------- Chụp cửa sổ đang hiển thị phía trước ----------
Add-Type -AssemblyName System.Drawing
if (-not ('Lab7Win' -as [type])) {
    Add-Type -TypeDefinition @"
using System;
using System.Runtime.InteropServices;
public static class Lab7Win {
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
    [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] public static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);
    [DllImport("user32.dll")] public static extern bool SetProcessDPIAware();
    [DllImport("dwmapi.dll")] public static extern int DwmGetWindowAttribute(IntPtr hwnd, int attr, out RECT rect, int size);
}
"@
}

function Save-ForegroundWindow {
    param([Parameter(Mandatory)][string]$Path)
    [void][Lab7Win]::SetProcessDPIAware()
    $handle = [Lab7Win]::GetForegroundWindow()
    $rect = New-Object 'Lab7Win+RECT'
    $size = [System.Runtime.InteropServices.Marshal]::SizeOf([type]'Lab7Win+RECT')
    # 9 = DWMWA_EXTENDED_FRAME_BOUNDS: khung cửa sổ chính xác, không kèm bóng đổ
    $ketQua = [Lab7Win]::DwmGetWindowAttribute($handle, 9, [ref]$rect, $size)
    if ($ketQua -ne 0) { [void][Lab7Win]::GetWindowRect($handle, [ref]$rect) }
    $rong = $rect.Right - $rect.Left
    $cao = $rect.Bottom - $rect.Top
    if ($rong -le 0 -or $cao -le 0) { throw 'Không xác định được cửa sổ để chụp.' }
    $bmp = New-Object System.Drawing.Bitmap($rong, $cao)
    try {
        $g = [System.Drawing.Graphics]::FromImage($bmp)
        try { $g.CopyFromScreen($rect.Left, $rect.Top, 0, 0, $bmp.Size) } finally { $g.Dispose() }
        $bmp.Save($Path, [System.Drawing.Imaging.ImageFormat]::Png)
    }
    finally { $bmp.Dispose() }
}

# ---------- Phần tự động ----------
function Invoke-DotnetCoLog {
    param([string]$TenLog, [string[]]$Tham)
    $logPath = Join-Path $logDir $TenLog
    & dotnet @Tham 2>&1 | Tee-Object -FilePath $logPath | Out-Host
    return $LASTEXITCODE
}

function Show-TongKet {
    param([string]$LogFile)
    Write-Host ''
    Write-Host '--- TỔNG KẾT ---' -ForegroundColor Cyan
    $dong = Select-String -LiteralPath (Join-Path $logDir $LogFile) -Pattern 'Passed!|Failed!|Total tests|Test Run (Successful|Failed)|Passed: |Failed: |succeeded|Build succeeded|Build FAILED' -ErrorAction SilentlyContinue |
        ForEach-Object { $_.Line.Trim() } | Select-Object -Last 8
    foreach ($d in $dong) { Write-Host "  $d" }
}

function Invoke-ChupBuildVaTest {
    Set-Location -LiteralPath $Root
    Clear-Host
    Write-Host '=== dotnet build ===' -ForegroundColor Cyan
    $buildExit = Invoke-DotnetCoLog -TenLog 'C08_build.txt' -Tham @('build', $sln.FullName, '--nologo', '-v', 'minimal')
    if ($buildExit -ne 0) {
        Write-Host "dotnet build lỗi (mã $buildExit). Xem log: $logDir\C08_build.txt" -ForegroundColor Red
        return
    }
    Write-Host ''
    Write-Host '=== dotnet test (toàn bộ, D01 là Explicit nên không chạy) ===' -ForegroundColor Cyan
    $testExit = Invoke-DotnetCoLog -TenLog 'C08_test.txt' -Tham @('test', $sln.FullName, '--no-build', '--nologo', '--logger', 'console;verbosity=normal')
    Show-TongKet -LogFile 'C08_test.txt'
    Write-Host ("Mã thoát dotnet test: {0}" -f $testExit)
    Start-Sleep -Milliseconds 800
    $anh = Join-Path $OutDir 'C08_Terminal_BuildVaTest.png'
    Save-ForegroundWindow -Path $anh
    Write-Host "Đã lưu: $anh" -ForegroundColor Green
}

function Invoke-ChupDebug {
    Set-Location -LiteralPath $Root
    foreach ($ca in @(@{ Ten = 'D01'; Loc = 'Name~D01_'; Anh = 'D01_Terminal_Failed.png' },
                      @{ Ten = 'D02'; Loc = 'Name~D02_'; Anh = 'D02_Terminal_Passed.png' })) {
        Clear-Host
        Write-Host ("=== dotnet test --filter {0} ===" -f $ca.Loc) -ForegroundColor Cyan
        [void](Invoke-DotnetCoLog -TenLog ("{0}_test.txt" -f $ca.Ten) -Tham @('test', $sln.FullName, '--filter', $ca.Loc, '--logger', 'console;verbosity=detailed'))
        Start-Sleep -Milliseconds 800
        $anh = Join-Path $OutDir $ca.Anh
        Save-ForegroundWindow -Path $anh
        Write-Host "Đã lưu: $anh" -ForegroundColor Green
        Start-Sleep -Seconds 1
    }
}

# ---------- Phần thủ công (có hướng dẫn) ----------
function Invoke-ChupThuCong {
    param([hashtable]$Buoc)
    $duongDan = Join-Path $OutDir $Buoc.File
    while ($true) {
        Write-Host ''
        Write-Host ("[{0}] {1}" -f $Buoc.Ma, $Buoc.Tieu) -ForegroundColor Cyan
        $i = 1
        foreach ($h in $Buoc.HuongDan) { Write-Host ("   {0}. {1}" -f $i, $h); $i++ }
        if ($Buoc.Url) { Start-Process $Buoc.Url }
        $traLoi = Read-Host 'Nhấn Enter khi sẵn sàng chụp (gõ S để bỏ qua ảnh này)'
        if ($traLoi -match '^[sS]$') { Write-Host '   Đã bỏ qua.' -ForegroundColor Yellow; return $false }
        for ($giay = 5; $giay -ge 1; $giay--) {
            Write-Host ("   Chụp sau {0}s: bấm chuột vào đúng cửa sổ cần chụp..." -f $giay)
            Start-Sleep -Seconds 1
        }
        Save-ForegroundWindow -Path $duongDan
        Write-Host "   Đã lưu: $duongDan" -ForegroundColor Green
        $lai = Read-Host '   Chụp lại? (Y/N, mặc định N)'
        if ($lai -notmatch '^[yY]$') { return $true }
    }
}

$cacBuocThuCong = @(
    @{ Ma = 'D04'; File = 'D04_Lab7_AI_ChanDoan_StackTrace.png'; Url = $null
       Tieu = 'Hội thoại AI chẩn đoán stack trace của D01'
       HuongDan = @('Mở ChatGPT / Claude / Gemini trong trình duyệt.',
                    'Dán prompt ở mục D4 của báo cáo (đã kèm stack trace D01), gửi và chờ AI trả lời xong.',
                    'Cuộn để cửa sổ thấy cả prompt lẫn câu trả lời, che thông tin cá nhân nếu có.') },
    @{ Ma = 'E01'; File = 'E01_Lab7_AI_DeXuatSai.png'; Url = $null
       Tieu = 'Hội thoại AI đề xuất sai (Coupon / Visa / OTP / SQL injection)'
       HuongDan = @('Mở lại hội thoại AI của Phần A (prompt sinh 12 test case).',
                    'Cuộn tới bảng có TC09 (mã giảm giá), TC10 (thẻ Visa), TC11 (OTP), TC12 (SQL injection).',
                    'Để cửa sổ thấy rõ các dòng đó rồi quay lại đây.') },
    @{ Ma = 'E02a'; File = 'E02a_Cart.png'; Url = 'https://www.saucedemo.com/'
       Tieu = 'Trang Cart của SauceDemo'
       HuongDan = @('Đăng nhập: standard_user / secret_sauce.',
                    'Bấm Add to cart ở Sauce Labs Backpack.',
                    'Bấm biểu tượng giỏ hàng để vào trang Cart (địa chỉ có cart.html), thấy nút Checkout.') }
)

# ---------- Chạy ----------
if (-not $BoQuaTuDong) { Invoke-ChupBuildVaTest }
if ($ChayLaiDebug) { Invoke-ChupDebug }
if (-not $BoQuaThuCong) {
    Write-Host ''
    Write-Host 'PHẦN THỦ CÔNG: script sẽ đếm ngược 5 giây rồi chụp cửa sổ đang ở phía trước.' -ForegroundColor Yellow
    foreach ($b in $cacBuocThuCong) { [void](Invoke-ChupThuCong -Buoc $b) }
}

Write-Host ''
Write-Host '=== KẾT QUẢ ===' -ForegroundColor Cyan
$canCo = @('C08_Terminal_BuildVaTest.png', 'D04_Lab7_AI_ChanDoan_StackTrace.png', 'E01_Lab7_AI_DeXuatSai.png', 'E02a_Cart.png')
foreach ($f in $canCo) {
    if (Test-Path -LiteralPath (Join-Path $OutDir $f)) { Write-Host ("  [CÓ]    {0}" -f $f) -ForegroundColor Green }
    else { Write-Host ("  [THIẾU] {0}" -f $f) -ForegroundColor Yellow }
}
Write-Host ''
Write-Host "Thư mục ảnh: $OutDir"
Write-Host 'Bước tiếp theo: chạy Lab7_ChenAnhVaoWord.ps1 để chèn ảnh vào các khung trong file Word.'
