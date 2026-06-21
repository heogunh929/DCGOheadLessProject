# DCGO Source Revision

## 상태

- Queue: `61_dcgo_source_snapshot_pin`
- Status: `needs-review`
- 승인 상태: 사용자 승인 전
- 작성 시각: `2026-06-21T17:04:11.8493884+09:00`

이 문서는 전체 카드풀 inventory와 포팅을 시작하기 전에 DCGO 원본 snapshot을 고정하기 위한 61번 결과다. 아직 사용자가 최종 SHA를 승인하지 않았으므로 queue 61은 `done`이 아니라 `needs-review`로 둔다.

## 기준 upstream

- Repository: `https://github.com/DCGO2/DCGO.git`
- Branch: `develop`
- `git ls-remote`로 확인한 현재 `develop`: `935259fac9ab52fddbc077037e63383fc34ae970`
- 61 prompt에 기록된 기존 후보 SHA: `3b68b9c49d80002a1e75ce4304dcf2c3295d28af`

`fetch`, `pull`, `checkout`은 수행하지 않았다. `git ls-remote`만 사용해 upstream ref를 읽었다.

## 로컬 DCGO 폴더 판정

- Path: `DCGO/`
- 독립 Git repository 여부: 아니오
- `git -C DCGO rev-parse --show-toplevel`: workspace root `E:/headlessDCGO`
- `git -C DCGO ls-files` 결과: `0`
- 의미: 현재 `DCGO/` 원본은 workspace Git으로 추적되는 별도 source checkout이 아니라 복사된 source tree로 취급해야 한다.
- dirty source 판정: Git metadata로는 판정 불가. 대신 아래 source manifest로 핵심 root fingerprint를 고정한다.
- `.gitmodules`: 없음
- `.gitattributes` LFS 패턴: `*.psd`, `*.asset`, `*.ttf`

## Manifest

Git metadata가 없는 복사본이므로 61 prompt의 fallback에 따라 deterministic SHA-256 file manifest를 생성했다.

- Manifest: `docs/source/dcgo-source-file-manifest.json`
- Manifest SHA-256: `d0b46c4d82901494e2cfa51c423dc70a7f8f29bbd0bc38eda6c91b7ebd90ef30`
- Hash algorithm: `SHA256`
- File count: `26882`
- Roots:
  - `DCGO/Assets/CardBaseEntity`
  - `DCGO/Assets/Scripts/CardEffect`
  - `DCGO/Assets/Scripts/Script`

## 승인 필요 사항

사용자는 다음 중 어떤 snapshot을 기준으로 고정할지 승인해야 한다.

1. 현재 upstream `develop` SHA `935259fac9ab52fddbc077037e63383fc34ae970`
2. 61 prompt 기존 후보 SHA `3b68b9c49d80002a1e75ce4304dcf2c3295d28af`
3. 현재 로컬 `DCGO/` 복사본 manifest SHA `d0b46c4d82901494e2cfa51c423dc70a7f8f29bbd0bc38eda6c91b7ebd90ef30`

사용자 승인 전에는 전체 카드풀 asset/effect inventory를 확정 snapshot 기준 작업으로 시작하지 않는다.
