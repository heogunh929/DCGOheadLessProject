param(
    [string]$Root = "."
)

$ErrorActionPreference = "Stop"

$ResolvedRoot = (Resolve-Path -LiteralPath $Root).Path

$dotnet = Join-Path $ResolvedRoot ".dotnet\dotnet.exe"
if (-not (Test-Path $dotnet)) {
    $dotnet = "dotnet"
}

$env:DOTNET_CLI_HOME = Join-Path $ResolvedRoot ".dotnet_home"
$env:NUGET_PACKAGES = Join-Path $ResolvedRoot ".nuget\packages"
$env:TEMP = Join-Path $ResolvedRoot ".tmp"
$env:TMP = Join-Path $ResolvedRoot ".tmp"

$project = Join-Path $ResolvedRoot "src\DCGO.RL.Engine.Tests\DCGO.RL.Engine.Tests.csproj"
& $dotnet run --no-restore --project $project -- "AssetRegistryMapping"
exit $LASTEXITCODE
