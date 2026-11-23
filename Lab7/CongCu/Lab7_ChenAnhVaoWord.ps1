<#
.SYNOPSIS
  Chèn ảnh PNG vào các khung "ẢNH ..." trong file Word báo cáo Lab 7.

.DESCRIPTION
  Mỗi khung trong Word có dòng "Tên gợi ý: C08_Terminal_BuildVaTest.png". Script lấy mã ở đầu tên file
  (C08, D04, E01, E02a) trong thư mục ảnh, tìm khung có cùng mã, thay nội dung khung bằng ảnh đúng khổ trang.
  File gốc không bị sửa; kết quả lưu thành file mới có hậu tố _DaChenAnh.
  Cần máy Windows đã cài Microsoft Word, và phải đóng file Word đang mở.

.EXAMPLE
  powershell -ExecutionPolicy Bypass -File .\Lab7_ChenAnhVaoWord.ps1 `
      -DocPath ".\SOF3091_LAB07_TB01913_TranNguyenBinhYen_TheoMau_HoanChinh_BoSungAnh.docx"

.EXAMPLE
  .\Lab7_ChenAnhVaoWord.ps1 -DocPath .\BaoCao.docx -ImageDir D:\Anh -OutPath D:\BaoCao_Cuoi.docx
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory)][string]$DocPath,
    [string]$ImageDir,
    [string]$OutPath
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$DocPath = (Resolve-Path -LiteralPath $DocPath).Path
if (-not $ImageDir) {
    if ($PSScriptRoot) { $goc = $PSScriptRoot } else { $goc = (Get-Location).Path }
    $ImageDir = Join-Path $goc 'screenshots\minhchung'
}
if (-not (Test-Path -LiteralPath $ImageDir)) { throw "Không thấy thư mục ảnh: $ImageDir" }
if (-not $OutPath) {
    $ten = [System.IO.Path]::GetFileNameWithoutExtension($DocPath) + '_DaChenAnh.docx'
    $OutPath = Join-Path (Split-Path -Path $DocPath -Parent) $ten
}

$anh = @(Get-ChildItem -LiteralPath $ImageDir -Filter '*.png' -File | Where-Object { $_.Name -match '^[A-Z]\d{2}[a-z]?_' })
if ($anh.Count -eq 0) { throw "Không có ảnh PNG đặt tên theo mã (vd C08_...) trong: $ImageDir" }

Copy-Item -LiteralPath $DocPath -Destination $OutPath -Force

$daChen = New-Object System.Collections.Generic.List[string]
$khongThay = New-Object System.Collections.Generic.List[string]
$word = $null
$doc = $null
try {
    $word = New-Object -ComObject Word.Application
    $word.Visible = $false
    $word.DisplayAlerts = 0
    $doc = $word.Documents.Open($OutPath)

    $trang = $doc.PageSetup
    $rongToiDa = $trang.PageWidth - $trang.LeftMargin - $trang.RightMargin - 12
    $caoToiDa = $trang.PageHeight - $trang.TopMargin - $trang.BottomMargin - 60

    foreach ($f in $anh) {
        $ma = ($f.Name -split '_')[0]
        $mau = '*' + $ma + '_*.png*'
        $xong = $false
        foreach ($bang in $doc.Tables) {
            if ($bang.Rows.Count -ne 1 -or $bang.Columns.Count -ne 1) { continue }
            $noiDung = $bang.Range.Text
            if (($noiDung -like $mau) -and ($noiDung -match 'PNG')) {
                $o = $bang.Cell(1, 1)
                $o.Range.Text = ''
                $vung = $o.Range
                $vung.Collapse(1)   # wdCollapseStart
                $hinh = $doc.InlineShapes.AddPicture($f.FullName, $false, $true, $vung)
                $hinh.LockAspectRatio = -1
                if ($hinh.Width -gt $rongToiDa) { $hinh.Width = $rongToiDa }
                if ($hinh.Height -gt $caoToiDa) { $hinh.Height = $caoToiDa }
                $o.Range.ParagraphFormat.Alignment = 1      # căn giữa
                $o.Shading.BackgroundPatternColor = -16777216   # bỏ màu nền khung (wdColorAutomatic)
                $bang.Rows.Item(1).HeightRule = 0           # wdRowHeightAuto
                $xong = $true
                break
            }
        }
        if ($xong) { $daChen.Add($f.Name) } else { $khongThay.Add($f.Name) }
    }
    $doc.Save()
}
finally {
    if ($doc) { try { $doc.Close(0) } catch { } }
    if ($word) {
        try { $word.Quit() } catch { }
        [void][System.Runtime.InteropServices.Marshal]::ReleaseComObject($word)
    }
    [GC]::Collect()
    [GC]::WaitForPendingFinalizers()
}

Write-Host ''
Write-Host "File kết quả: $OutPath" -ForegroundColor Green
Write-Host ("Đã chèn {0} ảnh:" -f $daChen.Count)
foreach ($t in $daChen) { Write-Host "  [OK]   $t" -ForegroundColor Green }
if ($khongThay.Count -gt 0) {
    Write-Host 'Không thấy khung tương ứng (kiểm tra mã ở đầu tên file):' -ForegroundColor Yellow
    foreach ($t in $khongThay) { Write-Host "  [BỎ]   $t" -ForegroundColor Yellow }
}
