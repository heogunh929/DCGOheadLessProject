param(
    [string]$Root = "."
)

$ErrorActionPreference = "Stop"

function Check-Path($Path, $Message) {
    if (-not (Test-Path $Path)) {
        Write-Host "[FAIL] $Message : $Path" -ForegroundColor Red
        return $false
    }
    Write-Host "[OK] $Message : $Path" -ForegroundColor Green
    return $true
}

$ok = $true
$ok = (Check-Path "$Root\AGENTS.md" "AGENTS.md") -and $ok
$ok = (Check-Path "$Root\DCGO" "DCGO source folder") -and $ok
$ok = (Check-Path "$Root\docs\codex-prompts\README.md" "Codex prompts README") -and $ok
$ok = (Check-Path "$Root\docs\codex-prompts\prompts" "Prompt folder") -and $ok
$ok = (Check-Path "$Root\src" "src folder") -and $ok

if (Test-Path "$Root\DCGO\Assets") {
    Write-Host "[OK] DCGO\Assets detected" -ForegroundColor Green
} else {
    Write-Host "[WARN] DCGO\Assets not found. Place the DCGO Unity source under DCGO\." -ForegroundColor Yellow
}

if ($ok) {
    Write-Host "Structure validation finished." -ForegroundColor Cyan
    exit 0
}

exit 1
