param(
    [ValidateSet('Debug','Release')]
    [string]$Configuration = 'Release',
    [switch]$Strict,
    [switch]$Pack,
    [switch]$SkipTests
)

$ErrorActionPreference = 'Stop'
$repo = Resolve-Path (Join-Path $PSScriptRoot '..')
Set-Location $repo

Write-Host '== Gridly clean ==' -ForegroundColor Cyan
dotnet clean .\Gridly.sln -c $Configuration

Write-Host '== Gridly restore ==' -ForegroundColor Cyan
dotnet restore .\Gridly.sln

$env:GRIDLY_STRICT = if ($Strict) { 'true' } else { 'false' }
Write-Host "== Gridly build ($Configuration) ==" -ForegroundColor Cyan
dotnet build .\Gridly.sln -c $Configuration --no-restore /p:CI=true

if (-not $SkipTests) {
    if (Test-Path .\tests) {
        Write-Host '== Gridly tests ==' -ForegroundColor Cyan
        dotnet test .\tests -c $Configuration --no-build --logger "trx;LogFileName=gridly-tests.trx"
    }
    if (Test-Path .\tools\QualityChecks\Gridly.QualityChecks.ps1) {
        Write-Host '== Gridly quality checks ==' -ForegroundColor Cyan
        powershell -ExecutionPolicy Bypass -File .\tools\QualityChecks\Gridly.QualityChecks.ps1
    }
}

if ($Pack) {
    Write-Host '== Gridly pack ==' -ForegroundColor Cyan
    New-Item -ItemType Directory -Force .\artifacts\nuget | Out-Null
    dotnet pack .\src\Gridly\Gridly.csproj -c $Configuration --no-build -o .\artifacts\nuget
}

Write-Host 'Gridly build pipeline completed.' -ForegroundColor Green
