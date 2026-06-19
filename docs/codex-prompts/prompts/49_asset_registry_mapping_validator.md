AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 49 - 원본 Asset ↔ Registry/Status/File Validator

## 목표

원본 card asset의 `CardEffectClassName`과 RL.Engine의 card script registry, status, 카드별 파일이 항상 일치하도록 자동 검증한다.

## 구현 방향

`DCGO/` 원본은 로컬에는 존재하지만 GitHub에는 포함되지 않을 수 있다.
따라서 검증을 두 층으로 만든다.

1. 순수 단위 테스트:
   - 작은 asset metadata fixture를 사용한다.
   - validator 규칙 자체를 검증한다.
2. 로컬 source audit command:
   - 명시된 `DCGO` root를 입력받는다.
   - source root가 없으면 성공으로 건너뛰지 말고 `SourceUnavailable` 결과를 명시한다.
   - source root가 있으면 mismatch를 실패로 반환한다.

## 검증 항목

- CardId/variant identity
- asset `CardEffectClassName`
- RL registry script effect class
- porting status
- 카드별 파일 존재
- source mapping 주석
- NoEffect marker 근거
- shared effect mapping
- variant-specific mapping
- Catalog registry coverage
- unregistered effect-bearing card

## 금지

- asset 파일이 없다는 이유로 자동 NoEffect 처리
- mismatch를 warning만 남기고 성공 처리
- 카드별 file requirement 완화
- 테스트용 whitelist 남발

## 테스트

- exact mapping
- shared effect mapping
- variant mapping
- false NoEffect detection
- missing per-card file
- stale status table
- unregistered effect
- source unavailable report
- actual local DCGO source audit, source가 존재하는 경우

## 산출물

- validator/service
- 실행 가능한 audit command 또는 script
- machine-readable report
- 한국어 요약 report
- validation strategy 문서 갱신
