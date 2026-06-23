@echo off
setlocal
powershell -ExecutionPolicy Bypass -File "%~dp0Build-Gridly.ps1" %*
endlocal
