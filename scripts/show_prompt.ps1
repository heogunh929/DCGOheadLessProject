param(
    [Parameter(Mandatory=$true)]
    [string]$Name,
    [string]$Root = "."
)

$promptPath = Join-Path $Root "docs\codex-prompts\prompts\$Name"
if (-not (Test-Path $promptPath)) {
    Write-Host "Prompt not found: $promptPath" -ForegroundColor Red
    exit 1
}

Get-Content $promptPath -Encoding UTF8
