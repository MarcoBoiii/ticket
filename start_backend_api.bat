@echo off
cd /d "%~dp0backend/API"
set ASPNETCORE_URLS=https://localhost:7257
dotnet run --project API.csproj --launch-profile https
pause
