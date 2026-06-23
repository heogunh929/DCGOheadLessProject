# AS-IS Porting Scope Decision

이 문서는 원본 DCGO AS-IS 전체 파일에 대해 headless 포팅 대상/참조/제외/수동검토 결정을 닫은 기준선이다.
기존 GOAL-07의 `CandidateReview`는 최종 상태로 남기지 않고, 결정 가능한 항목은 `ReferenceOnly` 또는 `Exclude`로 닫았다.

## Scope

- AS-IS root: `E:/headlessDCGO/DCGO`
- 전체 파일 수: 71180
- 전체 폴더 수: 4798
- OpenCodeReady: `False`
- Foundation blockers: unknown common API 27, unsupported capability 26, partial capability 37

## Decision Counts

| Decision | Count |
| --- | --- |
| Port | 12541 |
| ReferenceOnly | 17653 |
| Exclude | 40986 |
| ManualReview | 0 |

## Bucket Counts

| Bucket | Count |
| --- | --- |
| CardData | 8187 |
| CardEffectLogic | 3918 |
| CoreRuntime | 436 |
| EditorDataProvenance | 259 |
| ExternalDependency | 2623 |
| GeneratedOrBuild | 34501 |
| ProjectConfig | 31 |
| SoundOnly | 102 |
| SourceMeta | 14343 |
| UnityOnly | 397 |
| VisualOnly | 6383 |

## Candidate Closure

- GOAL-07 CandidateReview input files: 259
- CandidateReview left as ManualReview: 0
- `Assets/Editor/**/*.cs` data/import/fixup tools are `ReferenceOnly / EditorDataProvenance`.
- non-CardBaseEntity serialized review assets are `ReferenceOnly / EditorDataProvenance`.

## Port Targets

### CoreRuntime
- `Assets/Scripts/Script/AttackProcess.cs` (CoreRuntime)
- `Assets/Scripts/Script/AutomaticOrder/StartTurnTamerMemory.cs` (CoreRuntime)
- `Assets/Scripts/Script/AutoProcessing.cs` (CoreRuntime)
- `Assets/Scripts/Script/BattleMode.cs` (CoreRuntime)
- `Assets/Scripts/Script/BendText.cs` (CoreRuntime)
- `Assets/Scripts/Script/BGMObject.cs` (CoreRuntime)
- `Assets/Scripts/Script/BrainStormObject.cs` (CoreRuntime)
- `Assets/Scripts/Script/BreakGlass.cs` (CoreRuntime)
- `Assets/Scripts/Script/BurstEffectObject.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardController.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardDetail.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardDistribution.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardDistributionTab.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/CanSuspend.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/CanUnsuspend.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/IgnoreBattle.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnAddDigivolutionCards.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnAttack.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnAttackTargetSwitch.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnCardsAddedToHand.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnCardsReturnToHandFromTrash.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnCardsReturnToLibraryFromTrash.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnDeletion.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnEndAttack.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnFaceUpSecurityIncrease.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnMove.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnReturnLibraryBottomDigivolutionCards.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnSuspend.cs` (CoreRuntime)
- `Assets/Scripts/Script/CardEffectCommons/CanUseEffects/OnTrashBySelfDigiBurst.cs` (CoreRuntime)
- ... 406 more in JSON

### CardEffectLogic
- `Assets/Scripts/CardEffect/AD1/Black/AD1_023.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_010.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_011.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_012.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_013.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_014.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_019.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_020.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Blue/AD1_024.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Green/AD1_022.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Purple/AD1_018.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_001.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_002.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_003.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_004.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_005.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_006.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_007.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_008.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_009.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Red/AD1_025.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Yellow/AD1_015.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Yellow/AD1_016.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Yellow/AD1_017.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/AD1/Yellow/AD1_021.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/BT1/Blue/BT1_003.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/BT1/Blue/BT1_004.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/BT1/Blue/BT1_029.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/BT1/Blue/BT1_030.cs` (CardEffectLogic)
- `Assets/Scripts/CardEffect/BT1/Blue/BT1_031.cs` (CardEffectLogic)
- ... 3888 more in JSON

### CardData
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P2.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Green/AD1_022.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset` (CardData)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset` (CardData)
- ... 8157 more in JSON

## ReferenceOnly Buckets

- `.editorconfig` (UnityOnly)
- `.gitattributes` (UnityOnly)
- `.github/ISSUE_TEMPLATE/bug_report.md` (UnityOnly)
- `.github/ISSUE_TEMPLATE/feature_request.md` (UnityOnly)
- `.gitignore` (UnityOnly)
- `.idea/.idea.DCGO/.idea/encodings.xml` (UnityOnly)
- `.idea/.idea.DCGO/.idea/indexLayout.xml` (UnityOnly)
- `.idea/.idea.DCGO/.idea/projectSettingsUpdater.xml` (UnityOnly)
- `.idea/.idea.DCGO/.idea/vcs.xml` (UnityOnly)
- `.idea/.idea.DCGO/.idea/workspace.xml` (UnityOnly)
- `.idea/.idea.DCGO2/.idea/indexLayout.xml` (UnityOnly)
- `.idea/.idea.DCGO2/.idea/projectSettingsUpdater.xml` (UnityOnly)
- `.idea/.idea.DCGO2/.idea/vcs.xml` (UnityOnly)
- `.vscode/extensions.json` (UnityOnly)
- `.vscode/launch.json` (UnityOnly)
- `.vscode/settings.json` (UnityOnly)
- `.vsconfig` (UnityOnly)
- `Assets/3DObjects.meta` (UnityOnly)
- `Assets/AddOn.meta` (UnityOnly)
- `Assets/Animation.meta` (UnityOnly)
- `Assets/CardBaseEntity.meta` (UnityOnly)
- `Assets/CardBaseEntity/AD1.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Black.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Black/Tamer.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Black/Tamer/AD1_023_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_010.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_011.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_012_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_013.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_014_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Digimon/AD1_024_P2.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Tamer.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_019.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Blue/Tamer/AD1_020_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Green.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Green/AD1_022.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Purple.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Purple/Digimon.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Purple/Digimon/AD1_018_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_001_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_002_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_003.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_004_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_005_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_006_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_007_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_008_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_009_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Red/Digimon/AD1_025_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_015_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_016_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Digimon/AD1_017_P1.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021.asset.meta` (SourceMeta)
- `Assets/CardBaseEntity/AD1/Yellow/Tamer/AD1_021_P1.asset.meta` (SourceMeta)
- ... 17573 more in JSON

## Excluded Buckets

- `Assets/3DObjects/saigonogarasu.fbx` (VisualOnly)
- `Assets/3DObjects/saigonogarasu.fbx.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Ani.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Ani/Shield_01.anim` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Ani/Shield_01.anim.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Ani/Shield_01.controller` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Ani/Shield_01.controller.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_00.mat` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_00.mat.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_01.mat` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_01.mat.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_02.mat` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_02.mat.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_03.mat` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mat/Shield_03.mat.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mod.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mod/Shield_01.FBX` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Mod/Shield_01.FBX.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shader.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shader/Shield_01.shader` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shader/Shield_01.shader.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_00.prefab` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_00.prefab.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_01.prefab` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_01.prefab.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_02.prefab` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_02.prefab.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_03.prefab` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Shield_03.prefab.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/Noise_001.png` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/Noise_001.png.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/N_001.png` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/N_001.png.meta` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/N_002.tga` (VisualOnly)
- `Assets/AddOn/@ Xxuebi/Tex/N_002.tga.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/readme.txt` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/readme.txt.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/AutoLayout3DEditor.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/AutoLayout3DEditor.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/GridLayoutGroup3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/GridLayoutGroup3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/LayoutElement3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/LayoutElement3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/LayoutGroup3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/LayoutGroup3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/XAxisLayoutGroup3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/XAxisLayoutGroup3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/YAxisLayoutGroup3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/YAxisLayoutGroup3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/ZAxisLayoutGroup3D.cs` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Scripts/ZAxisLayoutGroup3D.cs.meta` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Test.unity` (VisualOnly)
- `Assets/AddOn/AutoLayout3D/Test.unity.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/.gitignore` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scenes.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scenes/SampleScene.unity` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scenes/SampleScene.unity.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scripts.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scripts/DeformationImage.cs` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scripts/DeformationImage.cs.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scripts/DeformationImageVertex.cs` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Assets/Scripts/DeformationImageVertex.cs.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/LICENSE` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/LICENSE.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Logs.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Logs/Packages-Update.log` (GeneratedOrBuild)
- `Assets/AddOn/DeformationImage-master/Logs/Packages-Update.log.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Packages.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Packages/manifest.json` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/Packages/manifest.json.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/ProjectSettings.meta` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/AudioManager.asset` (VisualOnly)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/AudioManager.asset.meta` (VisualOnly)
- ... 40906 more in JSON

## ManualReview

- 없음

## File-Level Truth

전수 파일별 결정은 `docs/generated/as-is-restart/asis-porting-scope-decision.json`의 `files` 배열에 기록했다.
Markdown에는 전체 71,180행을 중복하지 않고 요약과 샘플만 둔다.

## Non-Goals Confirmed

- `src/` 구현 코드 수정 없음.
- 원본 `DCGO/Assets` 수정 없음.
- CardEffect body 구현 없음.
- C0039 이후 card-porting 실행 없음.
- Foundation Gate 수치 조작 없음.
- generated status 승격 없음.
- commit/push 없음.

## Recommended Commit Message

`docs: close AS-IS porting scope decisions`
