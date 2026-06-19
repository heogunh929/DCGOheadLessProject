# Porting Structure Policy

## Current Snapshot - 2026-06-14

- source-aligned 로컬 작업트리는 ST1~ST3 카드별 파일/marker 구조와 registry snapshot을 가진다.
- latest recorded structure guard는 `All 212 tests passed.`다.
- 아직 commit 전인 queue 34~38 변경이 있으므로, 이 문서의 current snapshot은 로컬 작업트리 기준이다.
- `ST2-07`, `ST3-07`의 shared `ST1_06` mapping은 card-id 기반 `Implemented` script로 정리했다. `ST3-02` variant source-alignment risk는 여전히 정책상 차단/감사 대상이다.
- 아래 queue 35/36 정책은 historical context이자 현재 guard의 근거다.

## Queue 35 감사 후 추가 정책

2026-06-14 queue 35 현재 커밋 감사 기준으로 다음 정책을 추가한다.

- ST2/ST3도 ST1과 같은 source mapping 주석 형식을 사용해야 한다.
- ST2/ST3 NoEffect 카드를 카드별 marker 파일로 둘지 결정해야 한다. 결정 전까지는 NoEffect 판정을 asset `CardEffectClassName`과 대조하는 guard를 우선한다.
- shared `CardEffectClassName`은 NoEffect로 등록하지 않는다. `ST2-07 -> ST1_06`, `ST3-07 -> ST1_06` 같은 shared mapping은 명시 registry 또는 shared script policy로 고정해야 한다.
- asset variant가 서로 다른 `CardEffectClassName`을 갖는 경우, NoEffect 판정을 보류하고 감사 문서에 불확실성으로 남긴다.
- support helper는 카드 ID별 shortcut을 포함하지 않아야 하며, 각 카드 파일에서 timing/조건/대상/primitive 호출 흐름을 읽을 수 있어야 한다.

## Queue 36 guard 강화 후 추가 정책

2026-06-14 queue 36 기준으로 ST2/ST3 구조 guard를 ST1과 같은 수준으로 확장한다.

- ST2/ST3 `Implemented` 또는 `NoEffect` registry record는 모두 카드별 파일을 가져야 한다.
- 원본 set-local CardEffect 파일이 있는 ST2/ST3 카드는 RL.Engine 카드 파일 상단에 `DCGO/Assets/Scripts/CardEffect/<Set>/<Color>/<Card>.cs` source mapping을 둔다.
- 원본 set-local CardEffect 파일이 없는 ST2/ST3 `NoEffect` 카드는 동작 없는 marker 파일에 `No set-local CardEffect source file exists` 근거를 둔다.
- 이 marker 파일은 효과 구현이 아니며, `NoEffectCardScript` 등록의 근거를 파일 구조에 남기기 위한 것이다.
- `ST2-07`, `ST3-07`처럼 set-local CardEffect 파일은 없지만 원본 asset이 shared `ST1_06`을 참조하는 카드는 `NoEffect`로 등록하지 않는다. 카드별 RL.Engine 파일에는 shared source mapping을 적고, registry는 card-id 기반 `Implemented` script로 연결한다.
- `ST3-02`는 `ST3_02_P2.asset` variant가 `CardEffectClassName: ST3_02`를 참조하므로, marker와 상태 문서에 variant risk를 반드시 남긴다.
- `St2St3CardScriptCatalog`는 `St1CardScriptCatalog`와 동일하게 registry-only여야 한다.
- `docs/rl-engine/cardeffect-porting-status.md`의 ST1~ST3 registry snapshot은 실제 registry와 테스트로 대조한다.

작성일: 2026-06-14

## 목적

RL.Engine은 신규 카드게임 엔진이 아니라 `DCGO/` Unity battle 로직을 검증 가능하게 이식하는 headless 엔진이다. 따라서 포팅 구조는 원본 DCGO의 파일과 책임 경계를 추적할 수 있어야 한다.

이 문서는 ST2/ST3 확장 전에 고정할 구조 원칙을 정의한다.

## 기본 원칙

1. 원본 파일 구조를 최대한 유지한다.
   - 원본 `DCGO/Assets/Scripts/CardEffect/<Set>/<Color>/<Card>.cs`에 대응되는 RL.Engine 파일을 카드별로 둔다.
   - 원본 공통 처리 파일은 RL.Engine 공통 service와 명시적으로 매핑한다.
   - 파일 구조만으로도 원본 source mapping을 추적할 수 있어야 한다.

2. CardEffect는 카드별 파일로 둔다.
   - 카드별 effect body는 `src/DCGO.RL.Engine/CardEffects/<Set>/<Color>/<Card>.cs`에 둔다.
   - 한 파일에 여러 카드의 실제 body를 몰아넣지 않는다.
   - 원본 CardEffect 파일이 없는 카드는 `NoEffectCardScript`로 명시한다.
   - 원본 asset의 `CardEffectClassName`이 다른 카드 효과를 참조하는 경우, NoEffect로 처리하지 말고 shared-effect mapping을 명시한다.

3. Catalog는 registry 등록만 담당한다.
   - `St1CardScriptCatalog`, `St2St3CardScriptCatalog`는 `ICardScript` 목록 생성과 registry 생성만 담당한다.
   - Catalog에 카드별 effect body, target 조건, primitive 호출, trigger wiring을 넣지 않는다.
   - Catalog는 구현 상태를 숨기는 장소가 아니며, 실제 구현 파일과 status 문서가 일치해야 한다.

4. 공통 service는 원본 공통 처리에 대응될 때만 둔다.
   - `TriggerPipelineService`는 원본 `AutoProcessing` / `MultipleSkills` 계열 흐름과 매핑되어야 한다.
   - `SecurityEffectExecutionService`는 원본 security check와 security effect 실행 흐름에 매핑되어야 한다.
   - `Tier1PrimitiveService` / `ZoneMover`는 원본 zone 이동과 battle primitive를 대체하는 경계로 유지한다.
   - 공통 service가 카드별 특수 처리를 하드코딩하면 안 된다.

5. helper는 카드별 body를 숨기지 않아야 한다.
   - 반복되는 selection, duration modifier, security execution 호출은 helper로 추출할 수 있다.
   - helper 이름과 책임은 원본 공통 로직 또는 명확한 generic primitive에 대응되어야 한다.
   - helper가 특정 카드 ID에 의존하거나 카드별 body 전체를 대체하면 구조 감사 대상이다.
   - 카드별 파일은 최소한 해당 카드의 timing, 조건, 대상, primitive 호출 흐름을 읽을 수 있어야 한다.

6. 구현 상태표와 실제 파일 존재가 일치해야 한다.
   - `docs/rl-engine/cardeffect-porting-status*.md`의 status는 실제 RL.Engine 파일/테스트와 맞아야 한다.
   - `Implemented`는 원본 의미가 보존되고 regression test가 존재할 때만 사용한다.
   - `PartiallyImplemented`는 어떤 effect branch가 남았는지 문서화한다.
   - `NoEffect`는 원본 CardEffect 파일 부재뿐 아니라 asset `CardEffectClassName`까지 확인한 뒤 사용한다.
   - `Unsupported`는 silent no-op이 아니라 validator/report에 명시되어야 한다.

7. 구조 검증 테스트를 추가해야 한다.
   - Catalog가 registry 등록 외 클래스를 포함하지 않는지 검사한다.
   - 원본 CardEffect 파일과 RL.Engine 카드별 파일의 대응 관계를 검사한다.
   - `NoEffectCardScript` 등록 카드가 원본 asset에서 비어 있는 `CardEffectClassName`인지 검사한다.
   - shared `CardEffectClassName`은 명시 mapping이 있는지 검사한다.
   - 구현 상태표와 registry 등록 상태가 불일치하면 실패해야 한다.

## 금지 사항

- ST2/ST3 전용 shortcut으로 원본 효과를 우회하지 않는다.
- 카드별 effect body를 Catalog나 대형 helper에 숨기지 않는다.
- 원본 의미가 불확실한 효과를 `Implemented`로 올리지 않는다.
- `NoEffect`를 파일 부재만으로 자동 확정하지 않는다.
- zone list를 직접 수정하지 않는다. `ZoneMover` 또는 primitive를 사용한다.
- RL 학습용 `ObservationEncoder`, `RewardCalculator`, `DatasetExporter`, `Trainer`, RL Environment API를 엔진 완성 전 구현하지 않는다.

## ST2/ST3 구현 전 체크리스트

- [ ] `DCGO/Assets/Scripts/CardEffect/ST2/**`, `ST3/**`와 RL.Engine 카드별 파일 매핑표가 있다.
- [ ] asset `CardEffectClassName` 기반 NoEffect/shared-effect 검증이 끝났다.
- [ ] Catalog가 registry-only인지 확인됐다.
- [ ] 공통 helper가 카드별 body를 숨기지 않는지 확인됐다.
- [ ] docs의 최신 상태와 과거 기록이 분리됐다.
- [ ] ST1 target deck baseline이 깨지지 않았다.
- [ ] ST2/ST3 validation 결과가 통과이든 실패이든, shared/variant source-alignment risk와 unsupported/gap report를 silent no-op 없이 명시한다.

## 구조 검증 테스트 기준

2026-06-14에 ST1 구조 검증 테스트를 추가했고, queue 36에서 ST2/ST3까지 확장했다. 이 테스트는 새 카드 효과 구현이 아니라 포팅 구조를 강제하기 위한 guard다.

- `Implemented` 또는 `NoEffect` ST1 CardId는 `src/DCGO.RL.Engine/CardEffects/ST1/Red/ST1_XX.cs` 파일을 가져야 한다.
- `Implemented` 또는 `NoEffect` ST2 CardId는 `src/DCGO.RL.Engine/CardEffects/ST2/Blue/ST2_XX.cs` 파일을 가져야 한다.
- `Implemented` 또는 `NoEffect` ST3 CardId는 `src/DCGO.RL.Engine/CardEffects/ST3/Yellow/ST3_XX.cs` 파일을 가져야 한다.
- 원본 CardEffect 파일이 존재하는 ST1 카드는 RL.Engine 파일에 `DCGO/Assets/Scripts/CardEffect/ST1/Red/ST1_XX.cs` source mapping을 적어야 한다.
- 원본 CardEffect 파일이 존재하는 ST2/ST3 카드는 RL.Engine 파일에 set-local source mapping을 적어야 한다.
- 원본 CardEffect 파일이 없는 ST1 NoEffect 카드는 RL.Engine marker 파일에 `No original CardEffect source file exists` 근거를 적어야 한다.
- 원본 set-local CardEffect 파일이 없는 ST2/ST3 NoEffect 카드는 RL.Engine marker 파일에 `No set-local CardEffect source file exists` 근거를 적어야 한다.
- `St1CardScriptCatalog`와 `St2St3CardScriptCatalog`는 registry 생성만 담당해야 하며 `EffectDescriptor`, `SelectionRequest`, `SelectionContinuation`, primitive 실행, `ZoneMover`, `TemporaryModifier`, delete/trash/destroy 같은 body 로직을 포함하면 안 된다.
- `docs/rl-engine/cardeffect-porting-status.md`의 ST1 status table은 실제 `St1CardScriptCatalog` registry와 일치해야 한다.
- 같은 문서의 ST1~ST3 registry snapshot은 `St1CardScriptCatalog` + `St2St3CardScriptCatalog` 실제 registry와 일치해야 한다.
- status table에서 `Implemented` 또는 `NoEffect`인 ST1 카드는 실제 파일 존재와 일치해야 한다.
- registry snapshot에서 `Implemented` 또는 `NoEffect`인 ST1~ST3 카드는 실제 파일 존재와 일치해야 한다.
- CardEffect 파일에서 `Hand.Add`, `Deck.Add`, `Security.Add`, `Trash.Add`, `FieldPermanents.Add`, `CardsIn(...).Add`, `CurrentZone =` 같은 직접 zone mutation 패턴은 금지한다.
- ST2/ST3 파일은 각각 `CardEffects/ST2/...`, `CardEffects/ST3/...` 아래에 있어야 한다.
