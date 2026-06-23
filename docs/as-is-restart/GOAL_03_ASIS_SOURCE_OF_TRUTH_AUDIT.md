# GOAL 03 AS-IS SourceOfTruth Audit

> GOAL 01/02 산출물을 기준으로 SourceOfTruth 분류의 일관성과 누락 가능성을 감사한 기준선이다. 후보 목록은 확정 판정이 아니라 후속 분석 입력이다.

## 입력 기준

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 입력 role 재분류: `docs/generated/as-is-restart/asis-role-reclassification.json`
- GOAL 03 생성 시각 UTC: `2026-06-23T05:54:12.1637796+00:00`
- expected SourceOfTruth roots: `Assets/Scripts`, `Assets/CardBaseEntity`
- source root sidecar 후보: `Assets/Scripts.meta`, `Assets/CardBaseEntity.meta`
- 본문/함수/call graph 분석: `false`
- headless 필요 여부 판정: `false`
- SourceOfTruth role 승격/수정: `false`

## 전체 요약

| 항목 | 값 |
| --- | --- |
| 전체 파일 수 | 71180 |
| 전체 폴더 수(root 포함) | 4798 |
| SourceOfTruth 파일 수 | 26884 |
| SourceOfTruth 폴더 수 | 1805 |
| SourceOfTruth C# 파일 수 | 4354 |
| SourceOfTruth CardDataCandidate 수 | 8187 |
| SourceOfTruth ScriptableObjectCandidate 수 | 8187 |

## SourceOfTruth root별 파일 수

| Root | 파일 수 |
| --- | --- |
| Assets/CardBaseEntity | 17753 |
| Assets/Scripts | 9131 |

## SourceOfTruth 확장자별 파일 수

| 확장자 | 파일 수 |
| --- | --- |
| .asset | 8187 |
| .cs | 4354 |
| .meta | 14343 |

## 감사 finding

| Finding | 상태 | 수 | 설명 |
| --- | --- | --- | --- |
| expected-root-source-file-coverage | Review | 2 | Expected SourceOfTruth file scope entries without SourceOfTruth role. |
| source-file-outside-expected-root | Pass | 0 | Files assigned SourceOfTruth outside Assets/Scripts and Assets/CardBaseEntity. |
| card-data-outside-source | Pass | 0 | CardDataCandidate entries not assigned SourceOfTruth. |
| scriptable-object-outside-source | Review | 67 | ScriptableObjectCandidate entries outside SourceOfTruth. Most are expected visual/package/project resources; review before GOAL 06. |
| project-owned-assets-outside-source | Review | 631 | Assets entries classified as project-owned UnityProjectSource but not SourceOfTruth. |
| editor-tool-code-outside-source | Review | 24 | Assets/Editor C# tools outside SourceOfTruth that may explain card data provenance. |
| source-file-meta-pair-coverage | Pass | 0 | SourceOfTruth non-meta files missing Unity .meta sidecar. |
| source-meta-orphan-coverage | Pass | 0 | SourceOfTruth .meta files whose base file/folder is absent. |
| source-folder-meta-pair-coverage | Pass | 0 | SourceOfTruth folders missing Unity .meta sidecar. |

## Candidate directory counts

### ScriptableObjectCandidate outside SourceOfTruth

| Directory | 파일 수 |
| --- | --- |
| Assets/AddOn | 43 |
| Assets/Editor | 1 |
| Assets/Photon | 2 |
| Assets/Resources | 12 |
| Assets/Settings | 5 |
| Assets/TextMesh Pro | 3 |
| Assets/UniversalRenderPipelineGlobalSettings.asset | 1 |

### Project-owned Assets outside SourceOfTruth

| Directory | 파일 수 |
| --- | --- |
| Assets/3DObjects.meta | 1 |
| Assets/AddOn.meta | 1 |
| Assets/Animation.meta | 1 |
| Assets/CardBaseEntity.meta | 1 |
| Assets/Editor | 374 |
| Assets/Editor.meta | 1 |
| Assets/Effect.meta | 1 |
| Assets/Image.meta | 1 |
| Assets/Materials.meta | 1 |
| Assets/Photon.meta | 1 |
| Assets/Plugins.meta | 1 |
| Assets/Prefab | 171 |
| Assets/Prefab.meta | 1 |
| Assets/Presets | 15 |
| Assets/Presets.meta | 1 |
| Assets/RenderTexture.meta | 1 |
| Assets/Resources | 27 |
| Assets/Resources.meta | 1 |
| Assets/Scenes | 8 |
| Assets/Scenes.meta | 1 |
| Assets/SCI-FI UI Components.meta | 1 |
| Assets/Scripts.meta | 1 |
| Assets/ScriptTemplates | 2 |
| Assets/ScriptTemplates.meta | 1 |
| Assets/Settings | 10 |
| Assets/Settings.meta | 1 |
| Assets/Shader_Material.meta | 1 |
| Assets/Sound.meta | 1 |
| Assets/TextMesh Pro.meta | 1 |
| Assets/UniversalRenderPipelineGlobalSettings.asset | 1 |
| Assets/UniversalRenderPipelineGlobalSettings.asset.meta | 1 |

### Assets/Editor C# 후보

| Directory | 파일 수 |
| --- | --- |
| Assets/Editor | 10 |
| Assets/Editor/Fixing Scripts | 14 |

## 중요 후보 샘플

### Expected source scope 중 SourceOfTruth 미부여

- `Assets/CardBaseEntity.meta` (UnityProjectSource; Source root folder .meta sidecar is UnityProjectSource in GOAL 02; review whether source-root sidecars should inherit SourceOfTruth.)
- `Assets/Scripts.meta` (UnityProjectSource; Source root folder .meta sidecar is UnityProjectSource in GOAL 02; review whether source-root sidecars should inherit SourceOfTruth.)

### Assets/Editor C# 후보

- `Assets/Editor/AttachCardData.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/AttachCardPoolPrefab.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/AttachShuffledNumberIDs.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/AutoTextureConvert.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/CheckDeckCodeParse.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/ConvertDigiEggCardKind.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/CsvLoader_CardEntity.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/FindEntitiesFromCardIndex.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/AdjustSetSpecificCardIndex.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/ChangeAssetsToEnglish.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/CleanUpClassName.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FindInconsistentCardType.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FindInconsistentNaming.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FindMissingAAs.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FixDigiEggCost.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FixErrataImages.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FixForDualTypings.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/FixRestrictedValues.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/GetHighestCardIndex.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/GetListOfInvalidClassNames.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/GetLowestCardIndex.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/Fixing Scripts/MatchClassNames.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/GetAsset.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)
- `Assets/Editor/JSONLoader_CardEntity.cs` (UnityProjectSource; Assets/Editor C# tool. It may encode card data import/fixup semantics, but it is editor-only and not battle runtime SourceOfTruth unless a later goal chooses to treat tooling as data provenance.)

### ScriptableObjectCandidate outside SourceOfTruth 샘플

- `Assets/AddOn/DeformationImage-master/ProjectSettings/EditorBuildSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/EditorSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/GraphicsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/Physics2DSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/ProjectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/QualitySettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/UnityConnectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/DeformationImage-master/ProjectSettings/XRSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/EditorBuildSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/EditorSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/GraphicsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/Physics2DSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/ProjectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlitchFx-master/ProjectSettings/QualitySettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/EditorBuildSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/EditorSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/GraphicsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/Physics2DSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/ProjectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/QualitySettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/UnityAdsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/GlowImage-master/GlowImage/ProjectSettings/UnityConnectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/EditorBuildSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/EditorSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/GraphicsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/PackageManagerSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/Physics2DSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/ProjectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/QualitySettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/UnityConnectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/VersionControlSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/ProjectSettings/XRSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/UIOutline-master/UserSettings/EditorUserSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/EditorBuildSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/EditorSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/GraphicsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/Physics2DSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/ProjectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/QualitySettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/UnityAdsSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/unity3d-curves-master/ProjectSettings/UnityConnectSettings.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/WebGLInput-master/Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF - Fallback.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/AddOn/WebGLInput-master/Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset` (VisualOnly; Visual or add-on/UI ScriptableObject candidate.)
- `Assets/Editor/Fixing Scripts/ScriptableObjects/CardEntity_Inconsistency.asset` (UnityProjectSource; Editor tooling ScriptableObject candidate; possible card-data provenance support.)
- `Assets/Photon/PhotonUnityNetworking/Code/Editor/PunSceneSettingsFile.asset` (ExternalPackage; External package ScriptableObject candidate.)
- `Assets/Photon/PhotonUnityNetworking/Resources/PhotonServerSettings.asset` (ExternalPackage; External package ScriptableObject candidate.)
- `Assets/Resources/DOTweenSettings.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/ANTQUAB SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/CP_REVENGE SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/data-latin SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/digitalism SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Makinas-4-Flat SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Makinas-4-Square SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Mechanoarc SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/mplus-1c-regular SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Play-Bold SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Play-Regular SDF.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Resources/Fonts & Materials/Play-Regular SDF_Card.asset` (UnityProjectSource; Project resource ScriptableObject candidate; review only if GOAL 06 finds CardBaseEntity dependency.)
- `Assets/Settings/ForwardRenderer.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)
- `Assets/Settings/SampleSceneProfile.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)
- `Assets/Settings/UniversalRP-HighQuality.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)
- `Assets/Settings/UniversalRP-LowQuality.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)
- `Assets/Settings/UniversalRP-MediumQuality.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)
- `Assets/TextMesh Pro/Resources/Sprite Assets/EmojiOne.asset` (ExternalPackage; External package ScriptableObject candidate.)
- `Assets/TextMesh Pro/Resources/Style Sheets/Default Style Sheet.asset` (ExternalPackage; External package ScriptableObject candidate.)
- `Assets/TextMesh Pro/Resources/TMP Settings.asset` (ExternalPackage; External package ScriptableObject candidate.)
- `Assets/UniversalRenderPipelineGlobalSettings.asset` (UnityProjectSource; ScriptableObject candidate outside GOAL 02 SourceOfTruth roots; review before promotion.)

## GOAL 06 추천 범위

- GOAL 06은 SourceOfTruth인 Assets/CardBaseEntity/*.asset 전체와 해당 .meta sidecar를 우선 입력으로 사용한다.
- Assets/Editor의 카드 데이터 import/fixup 도구 24개는 CardBaseEntity provenance 보조 후보로 별도 목록화하되, battle runtime SourceOfTruth로 즉시 승격하지 않는다.
- ScriptableObjectCandidate outside SourceOfTruth 67개는 대부분 visual/package/project resource로 보이며, GOAL 06에서는 CardBaseEntity 구조 분석과 분리한다.
- Assets/Scripts.meta 및 Assets/CardBaseEntity.meta는 source root sidecar boundary 후보로 남긴다. 파일/폴더 내부 coverage에는 누락이 없다.

## 금지 범위 준수

- `src/` 아래 C# 코드는 수정하지 않았다.
- 원본 `DCGO/Assets`는 수정하지 않았다.
- CardEffect body 구현은 수행하지 않았다.
- C0039 이후 card-porting은 수행하지 않았다.
- headless 필요 여부 최종 판정은 수행하지 않았다.
- commit/push는 수행하지 않았다.
