# 36 - CardEffect 파일 구조 guard 강화

AGENTS.md 지침, docs/progress/LOCAL_GIT_GUIDE.md 지침, common_constraints.md, 현재 /goal을 따르라.

이번 작업은 구조 검증 테스트 추가/강화 작업이다. 카드 효과 기능을 새로 구현하지 마라.

## 목표

앞으로 Codex가 카드 효과를 catalog나 큰 통합 파일에 몰아넣지 못하도록 구조 guard를 테스트로 고정한다.

## 구현할 테스트

1. 모든 Implemented/NoEffect ST1 카드가 `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_XX.cs` 파일을 가진다.
2. 모든 Implemented/NoEffect ST2 카드가 `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_XX.cs` 파일을 가진다.
3. 모든 Implemented/NoEffect ST3 카드가 `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_XX.cs` 파일을 가진다.
4. 원본 CardEffect가 있는 카드 파일에는 `DCGO/Assets/Scripts/CardEffect/...` source mapping이 있다.
5. 원본 CardEffect가 없는 NoEffect 카드 파일에는 missing-source 근거가 있다.
6. Catalog 파일은 registry-only다.
   - `new EffectDescriptor(`, `SelectionContinuation`, `Tier1PrimitiveService`, `ZoneMover`, `TemporaryModifier` 같은 카드별 body 흔적이 있으면 실패한다.
7. 카드별 파일에서 직접 zone list 수정 패턴을 감지한다.
   - `Hand.Add`, `Trash.Add`, `Security.Add`, `Deck.Add`, `Remove(` 등을 무조건 실패시키지는 말고, PlayerState zone list 직접 조작인지 보수적으로 탐지한다.
8. CardEffect status 문서와 실제 registry status가 일치하는지 확인한다.

## 허용

- 테스트 helper 추가
- 구조 policy 문서 업데이트
- 필요한 경우 catalog를 registry-only로 정리하는 최소 refactor

## 금지

- 새 카드 효과 구현
- ST2/ST3 card body 변경
- 원본 DCGO 수정
- 학습용 API 구현
- commit

## 문서 갱신

- docs/rl-engine/porting-structure-policy.md
- docs/rl-engine/validation-strategy.md
- docs/rl-engine/cardeffect-porting-status.md

## 테스트

- 가능한 전체 테스트 실행
- 구조 guard 테스트가 실패하면, 원칙을 완화하지 말고 코드 구조를 원본-like 방향으로 고친다.

## 완료 보고

- 추가한 guard 목록
- 실패/수정 내역
- 전체 테스트 결과
- commit은 만들지 않았다는 확인
