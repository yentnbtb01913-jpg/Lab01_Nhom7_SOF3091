@echo off
setlocal
chcp 65001 >nul
cd /d "%~dp0"
echo =====================================================
echo LAB 6 - CHAY SONG SONG 4 LUONG / SAME TEST SET
 echo NUnit.NumberOfTestWorkers = 4
 echo =====================================================
if not exist "Lab6.slnx" (
 echo.
 echo LOI: Khong thay Lab6.slnx.
 echo Giai nen 2 file nay vao thu muc Lab6, canh Lab6.slnx.
 pause
 exit /b 1
)
 echo.
echo LENH: dotnet test .\Lab6.slnx --settings .\ChaySongSong.runsettings --logger "console;verbosity=normal"
echo.
dotnet test ".\Lab6.slnx" --settings ".\ChaySongSong.runsettings" --logger "console;verbosity=normal"
set "RESULT=%ERRORLEVEL%"
echo.
echo =====================================================
echo Exit code: %RESULT%
echo CHUP ANH: Hien dong lenh va ket qua Passed/Failed/Total/Duration/Total time.
echo DOI CHIEU voi lan CHAY TUAN TU bang cung bo test (khong filter).
echo =====================================================
pause
exit /b %RESULT%
