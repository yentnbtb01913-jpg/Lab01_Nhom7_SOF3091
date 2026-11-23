@echo off
chcp 65001 >nul
cd /d "%~dp0"
echo =====================================================
echo LAB 6 - CHAY TUAN TU (KHONG CHAY SONG SONG)
echo NUnit.NumberOfTestWorkers = 0
 echo =====================================================
if not exist "Lab6.slnx" (
 echo.
 echo LOI: Khong thay Lab6.slnx. Hay giai nen 2 file nay vao THU MUC Lab6, canh Lab6.slnx.
 pause
 exit /b 1
)
 echo.
echo LENH: dotnet test .\Lab6.slnx --settings .\ChayTuanTu.runsettings --logger "console;verbosity=normal"
echo.
dotnet test ".\Lab6.slnx" --settings ".\ChayTuanTu.runsettings" --logger "console;verbosity=normal"
set RESULT=%ERRORLEVEL%
echo.
echo =====================================================
echo Ket qua lenh: exit code %RESULT%
echo CHUP ANH: Hien thi ca dong lenh, Passed / Failed / Total / Duration (hoac Total time).
echo =====================================================
pause
exit /b %RESULT%
