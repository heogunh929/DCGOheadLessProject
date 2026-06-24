# FND001-CS-06 Continuous/Static Keyword Descriptor Scope

## 목적

`FND001-CS-06 supported static keyword descriptor parity`는 `ContinuousOrStaticEffect`가 `PartiallyImplemented`로 남은 사유 중, 원본 static keyword wrapper 5종이 headless `ContinuousKeywordDescriptor` 근거와 source-aligned 방식으로 연결되는지만 닫는다.

이번 항목은 개별 `CardEffect` body 구현, generated status 승격, Foundation Gate 수치 조작, C0039 이후 card-porting 실행이 아니다.

## AS-IS Source Of Truth

- AS-IS root: `E:\headlessDCGO\DCGO\Assets`
- 원본 wrapper 파일:
  - `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Blocker.cs`
  - `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Rush.cs`
  - `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Reboot.cs`
  - `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Collision.cs`
  - `DCGO/Assets/Scripts/Script/CardEffectFactory/KeyWordEffects/Jamming.cs`
- 원본 effect class 파일:
  - `DCGO/Assets/Scripts/Script/CardEffects/BlockerClass.cs`
  - `DCGO/Assets/Scripts/Script/CardEffects/RushClass.cs`
  - `DCGO/Assets/Scripts/Script/CardEffects/RebootClass.cs`
  - `DCGO/Assets/Scripts/Script/CardEffects/CollisionClass.cs`
  - `DCGO/Assets/Scripts/Script/CardEffects/CanNotBeDestroyedByBattleClass.cs`

## 닫은 범위

| Keyword | 원본 wrapper | 원본 class/interface | headless descriptor |
| --- | --- | --- | --- |
| Blocker | `BlockerSelfStaticEffect` / `BlockerStaticEffect` | `BlockerClass` / `IBlockerEffect` | `BattleKeyword.Blocker` |
| Rush | `RushSelfStaticEffect` / `RushStaticEffect` | `RushClass` / `IRushEffect` | `BattleKeyword.Rush` |
| Reboot | `RebootSelfStaticEffect` / `RebootStaticEffect` | `RebootClass` / `IRebootEffect` | `BattleKeyword.Reboot` |
| Collision | `CollisionSelfStaticEffect` / `CollisionStaticEffect` | `CollisionClass` / `ICollisionEffect` | `BattleKeyword.Collision` |
| Jamming | `JammingSelfStaticEffect` / `JammingStaticEffect` | `CanNotBeDestroyedByBattleClass` / `ICanNotBeDestroyedByBattleEffect` | `BattleKeyword.Jamming` |

`Jamming`은 원본에 별도 `JammingClass`가 있는 구조가 아니라, security Digimon battle에서 파괴되지 않는 효과를 `CanNotBeDestroyedByBattleClass`로 만든다. 따라서 headless reuse/trust 판단에서도 `Jamming -> CanNotBeDestroyedByBattleClass -> BattleKeyword.Jamming` 매핑을 유지해야 한다.

## Headless Evidence

- `src/DCGO.RL.Engine/CardEffects/CardEffectFactory.cs`
  - 5개 supported wrapper를 `ContinuousKeywordDescriptor`로 생성한다.
  - `KeywordStaticEffect`는 `Blocker`, `Rush`, `Reboot`, `Collision`, `Jamming`만 허용한다.
  - unsupported keyword shape는 `UnsupportedMechanicException`으로 명시 실패한다.
- `src/DCGO.RL.Engine/Effects/ContinuousEffectDescriptor.cs`
  - `ContinuousKeywordDescriptor`가 source card, source permanent, controller, keyword, target kind, condition, source/target metadata criteria를 보존한다.
- `src/DCGO.RL.Engine/Effects/ContinuousEffectService.cs`
  - `CollectKeywords`와 `EvaluateKeywordsForPermanent`가 descriptor를 runtime keyword evaluation으로 연결한다.
- `src/DCGO.RL.Engine/Battle/BattleKeywordService.cs`
  - permanent/card-definition/temporary/continuous keyword를 공통 `HasKeyword` 경로로 합산한다.
- runtime 연결:
  - `Blocker`: blocker selection request
  - `Rush`: attack legality
  - `Reboot`: active phase unsuspend
  - `Collision`: forced block request
  - `Jamming`: security Digimon battle destruction prevention

## Source Sample Evidence

- full-card source scaffold에서 supported keyword wrapper sample record: 470개
- factory API reference 총합: 521건
- source-required-capabilities linked sample: 470개
- full-card parity `NotRun` sample: 470개

키워드별 source sample count:

- `Blocker`: 287
- `Rush`: 43
- `Reboot`: 91
- `Collision`: 30
- `Jamming`: 70

이 수치는 source-wide sample 후보를 연결한 것이며, executable Unity/RL parity pass가 아니다. full-card parity 전체는 계속 `NotRun 3918`, `Passed 0`이다.

## Test Candidates

기존 테스트 후보 12개가 verifier에서 확인된다.

- `Continuous static keyword field source grants Blocker`
- `CardEffectFactory Blocker static effect maps to keyword descriptor`
- `CardEffectFactory static keyword wrappers map supported keywords`
- `CardEffectFactory keyword static effect rejects unsupported keyword shape`
- `Continuous static keyword inherited source stops after move`
- `Continuous static keyword condition gates keyword`
- `Continuous static keyword replay deterministic`
- `BattleKeywords Blocker selection request`
- `BattleKeywords Rush attack legality`
- `BattleKeywords Reboot active phase`
- `BattleKeywords Collision forced block request`
- `BattleKeywords Jamming security battle`

## Retained Boundaries

- `FND001-CS-07` retained: `Piercing`, `Retaliation`, `Alliance`, `Vortex`, `Scapegoat`, `Iceclad`, `Progress` 등 trigger/process keyword shape는 이번 static keyword evidence로 닫지 않는다.
- `PARITY-001` retained: full-card Unity/RL executable parity는 아직 `NotRun`이다.
- `TRUST-001-RERUN` retained: headless `CardEffectFactory`, `ContinuousKeywordDescriptor`, `BattleKeywordService`는 SourceOfTruth mapping evidence 기준으로 reuse/delete/manual-review 재분류가 필요하다.
- `ContinuousOrStaticEffect` status는 계속 `PartiallyImplemented`이며, generated status를 `Implemented`나 `Verified`로 승격하지 않는다.
- Foundation Gate는 `OpenCodeReady=false`를 유지한다.

## Verification Artifact

- verifier: `scripts/verify_fnd001_static_keyword_scope.py`
- generated evidence: `docs/generated/as-is-restart/fnd-001-cs-06-static-keyword-verification.json`
- result:
  - source keyword wrapper coverage: 5 / 5
  - headless keyword wrapper coverage: 5 / 5
  - required test coverage: 12 / 12
  - unsupported keyword boundary retained: true
  - full-card parity reduced: false

## 다음 작업

다음 closeable candidate는 `FND001-CS-08 static DP/SecurityAttack/SecurityDigimonDP descriptor parity`다. `FND001-CS-07`은 unsupported trigger/process keyword mapping boundary로 남아 있으며, 별도 source mapping 작업 없이 이번 CS-06 evidence로 닫지 않는다.
