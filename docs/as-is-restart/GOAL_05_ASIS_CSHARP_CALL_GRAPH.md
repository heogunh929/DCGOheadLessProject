# GOAL 05 AS-IS C# Call Graph

> 이 문서는 GOAL 04 symbol inventory와 전체 `.cs` 구문 트리를 기반으로 호출/참조 edge를 추출한 기준선이다. headless 필요 여부 판정, capability 분석/구현, Verified 판정은 수행하지 않았다.

## 입력 기준

- AS-IS root 경로: `E:\headlessDCGO\DCGO`
- 입력 inventory: `docs/generated/as-is-restart/asis-full-file-inventory.json`
- 입력 role inventory: `docs/generated/as-is-restart/asis-role-reclassification.json`
- 입력 C# file index: `docs/generated/as-is-restart/asis-csharp-file-index.json`
- 입력 C# symbol index: `docs/generated/as-is-restart/asis-csharp-symbol-index.json`
- GOAL 05 생성 시각 UTC: `2026-06-23T04:53:13.2004374+00:00`
- 추출 방식: Roslyn syntax-only call/reference extraction + GOAL 04 symbol-name heuristic resolution
- semantic model 생성: `false`
- headless 필요 여부 판정: `false`

## 전체 요약

| 항목 | 값 |
| --- | --- |
| 분석한 .cs 파일 수 | 7723 |
| call edge 총수 | 1378594 |
| Exact | 162648 |
| Probable | 445797 |
| Heuristic | 711911 |
| Unresolved | 58238 |
| Unity 후보 | 122493 |
| GManager 후보 | 38651 |
| Photon 후보 | 32878 |
| UI 후보 | 98062 |
| Coroutine 후보 | 62387 |
| SourceOfTruth caller 호출 수 | 642654 |
| 추출 실패 파일 수 | 0 |

## Call kind별 수

| Call kind | 수 |
| --- | --- |
| methodInvocation | 297482 |
| propertyAccess | 272556 |
| fieldAccess | 240984 |
| typeReference | 217785 |
| enumReference | 107650 |
| identifierReference | 62190 |
| methodReference | 55235 |
| constructorCall | 47376 |
| staticMemberAccess | 23883 |
| coroutineYield | 20609 |
| coroutineCall | 17237 |
| memberAccessReference | 10985 |
| eventSubscription | 2977 |
| eventUnsubscription | 1045 |
| coroutineObjectCreation | 394 |
| eventReference | 141 |
| eventInvocationCandidate | 65 |

## Resolution confidence별 수

| Confidence | 수 |
| --- | --- |
| Heuristic | 711911 |
| Probable | 445797 |
| Exact | 162648 |
| Unresolved | 58238 |

## Caller role별 호출 수

| Caller role | 호출 수 |
| --- | --- |
| ExternalPackage | 711941 |
| UnityProjectSource | 699296 |
| GeneratedCache | 679298 |
| SourceOfTruth | 642654 |
| VisualOnly | 21673 |

## Boundary별 호출 수

| Boundary | 호출 수 |
| --- | --- |
| NonSourceOfTruthCaller | 735940 |
| SourceOfTruthInternal | 447207 |
| SourceOfTruthToExternalPackage | 106311 |
| SourceOfTruthToOtherOrUnresolved | 70681 |
| SourceOfTruthToUnity | 18455 |

## 태그별 후보 호출 수

| Tag | 호출 수 |
| --- | --- |
| Unity | 122493 |
| UI | 98062 |
| Coroutine | 62387 |
| GManager | 38651 |
| Photon | 32878 |

## Callee 후보별 호출 수 상위 200개

| Callee 후보 | 호출 수 |
| --- | --- |
| (empty) | 89116 |
| CardEffectCommons | 39547 |
| UnityEngine.Rendering.ComponentSingleton<TType>.instance | 26372 |
| SkillInfo.Hashtable | 21039 |
| Unity.Mathematics.Tests.TestUtils | 17870 |
| Coffee.UIParticleExtensions.ModifiedMaterial.Add | 17798 |
| Photon.Pun.PhotonView.Owner | 17518 |
| EffectTiming | 16770 |
| Unity.Mathematics.Tests.Assert2.AreEqual | 16770 |
| EvolutionEffectObject.CardSource | 16705 |
| Unity.EditorCoroutines.Editor.EditorCoroutineUtility.StartCoroutine | 16596 |
| IDiscardHand.cardSource | 15322 |
| ContinuousController | 15297 |
| AutoLayout3D.Bool3.x | 15284 |
| UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent | 15005 |
| ICardEffect | 13498 |
| ActivateClass.Activate | 12269 |
| AutoLayout3D.Bool3.y | 12090 |
| csShowAllEffect.i | 11149 |
| SelectPermanentEffect | 10348 |
| GManager | 10216 |
| MethodImplOptions.AggressiveInlining | 9024 |
| Permanent.TopCard | 8948 |
| ICardEffect.SetUpICardEffect | 7993 |
| IDiscardHand.hashtable | 7935 |
| UnityEngine.Rendering.VolumeStack.GetComponent<T> | 7755 |
| ICardEffect.CanUseCondition | 7560 |
| CardEffectFactory.ActivateClass | 7471 |
| AutoLayout3D.Bool3.z | 7297 |
| SelectCardEffect | 7142 |
| ActivateClass.SetUpActivateClass | 6883 |
| ActivateClass | 6871 |
| Coffee.UIEffects.UIHsvModifier.value | 6859 |
| UnityEditor.Rendering.SpeedTree8MaterialUpgrader.WindQuality.Count | 6828 |
| SimplifiedSelectCardConditionClass.Mode | 6784 |
| MindLinkClass.CanSelectPermanentCondition | 6428 |
| SelectAttackEffect.SetUp | 6125 |
| CardEffectCommons.IsExistOnBattleArea | 5635 |
| Photon.Realtime.Extensions.Contains | 5600 |
| CardSource.PermanentOfThisCard | 5568 |
| ICardEffect.CanActivateCondition | 5239 |
| ICardEffect.EffectDiscription | 5175 |
| Unity.Burst.Intrinsics.v128.v128 | 4937 |
| Shapes2D.FillType.None | 4822 |
| Unity.Mathematics.bool2x2.c1 | 4734 |
| Unity.Mathematics.bool2x2.c0 | 4711 |
| LemniscateOfBooth.a | 4687 |
| CardEffectFactory | 4285 |
| Unity.Mathematics.bool4.w | 4265 |
| SelectCardEffect.Mode.Custom | 4131 |
| SelectAttackEffect.SetUpCustomMessage | 4074 |
| List<ICardEffect> | 3965 |
| CEntity_Effect | 3924 |
| LemniscateOfBooth.b | 3880 |
| unity.libwebp.Interop.WebPPicture.v | 3840 |
| Unity.Mathematics.Geometry.Math | 3780 |
| CardEffectCommons.HasMatchConditionPermanent | 3607 |
| UnityEngine.Rendering.Universal.LibTessDotNet.Dict<TValue>.Min | 3603 |
| UnityEditor.Timeline.SelectionManager.Count | 3545 |
| SelectHandEffect | 3544 |
| UnityEditor.ShaderGraph.BlockNode.CustomBlockType.Vector3 | 3459 |
| Unity.Mathematics.bool2x3.c2 | 3214 |
| UnityEngine.Rendering.DynamicResScalerSlot.System | 3121 |
| CardEffectCommons.IsExistOnBattleAreaDigimon | 3024 |
| Cinemachine.CinemachineVirtualCameraBase.StandbyUpdateMode.Never | 2908 |
| System.ComponentModel | 2895 |
| System.ComponentModel.EditorBrowsableState | 2895 |
| EffectTiming.OnEnterFieldAnyone | 2848 |
| Photon.Pun.UtilityScripts.CellTreeNode.ENodeType.Root | 2814 |
| BlockerClass.PermanentCondition | 2808 |
| UnityEditor.SpriteRect.rect | 2666 |
| Unity.Burst.Intrinsics.v64.v64 | 2618 |
| UnityEngine.Rendering.Universal.UTess.ArraySlice<T>.Length | 2557 |
| CardSource.IsDigimon | 2418 |
| IDiscardHands.cardEffect | 2257 |
| UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphPass.index | 2253 |
| Unity.Mathematics.math.ShuffleComponent | 2223 |
| CardEffectCommons.MatchConditionPermanentCount | 2203 |
| CardEffectCommons.IsPermanentExistsOnOpponentBattleAreaDigimon | 2168 |
| TMPro.TextMeshPro.transform | 2164 |
| DCGO.Tools.Repair.InconsistentName.DataType.name | 2156 |
| List<CardSource> | 2080 |
| UnityEditor.Rendering.FilterWindow.IProvider.position | 2033 |
| SelectCardEffect.Root.DigivolutionCards | 1993 |
| UnityEditor.ShaderGraph.BlockNode.CustomBlockType.Vector2 | 1977 |
| Coffee.UIExtensions.ShinyEffectForUGUI.width | 1921 |
| NotImplementedException | 1920 |
| Player.HandCards | 1893 |
| UnityEditor.Timeline.TimeFieldDrawer.state | 1853 |
| EvoCost.CardColor | 1804 |
| SimplifiedSelectCardConditionClass.SelectCardCoroutine | 1786 |
| GManager.userSelectionManager | 1761 |
| ICardEffect.SetHashString | 1723 |
| Unity.Mathematics.bool2x4.c3 | 1627 |
| CardEffectCommons.IsOwnerTurn | 1610 |
| CardSource.CardNames | 1584 |
| DCGO.Tools.Repair.InconsistentName.DataType.text | 1555 |
| EffectDuration | 1534 |
| CardEffectCommons.IsPermanentExistsOnOwnerBattleAreaDigimon | 1527 |
| Player.Enemy | 1524 |
| UnityEngine.Rendering.Universal.UTess.UTriangle.c | 1524 |
| UnityEditor.Rendering.HierarchicalBox.material | 1501 |
| CardEffectCommons.IgnoreRequirement.Level | 1488 |
| UnityEngine.TestTools.MonoBehaviourTest<T>.gameObject | 1473 |
| Unity.Mathematics.float4.float4 | 1443 |
| UnityEditor.Rendering.LookDev.EnvironmentElement.target | 1439 |
| UnityEditor.U2D.Sprites.ITexture2D.height | 1423 |
| UnityEditor.ShaderGraph.ShaderSpliceUtil.TemplatePreprocessor.result | 1385 |
| Coffee.UIEffects.UIGradient.GradientStyle.Rect | 1377 |
| CardSource.EqualsTraits | 1361 |
| CardEffectCommons.CanTriggerWhenDigivolving | 1325 |
| Unity.Mathematics.float3.float3 | 1325 |
| SelectCardEffect.SetUpCustomMessage_ShowCard | 1323 |
| Player.AddMemory | 1310 |
| TMPro.TMP_Vertex.zero | 1302 |
| UnityEditor.Graphing.SlotReference.node | 1283 |
| CardEffectFactory.AddSelfDigivolutionRequirementStaticEffect | 1282 |
| EffectDescription | 1279 |
| Unity.Mathematics.math.float2 | 1275 |
| UnityEngine.Experimental.Rendering.RenderGraphModule.RenderGraphContext.cmd | 1256 |
| DCGO.Tools.Repair.InconsistentName.DataType.type | 1253 |
| SelectCardEffect.Root.Trash | 1243 |
| Coffee.UIExtensions.AnimatableProperty.ShaderPropertyType.Color | 1242 |
| Unity.Mathematics.math.float3 | 1206 |
| ICardEffect.SetIsInheritedEffect | 1205 |
| Unity.Mathematics.math.float4 | 1204 |
| CardSource.HasLevel | 1203 |
| UnityEditor.Rendering.LookDev.SidePanel.Debug | 1202 |
| CardEffectCommons.CanTriggerOnPlay | 1197 |
| CardSource.CardTraits | 1197 |
| UnityEditor.Rendering.ISerializedCamera.serializedObject | 1196 |
| Unity.Mathematics.math.double2 | 1195 |
| ITrashDeckCards.cardSources | 1185 |
| List<Permanent> | 1170 |
| CardSource.IsTamer | 1149 |
| UnityEditor.Timeline.TitleMode.GameObject | 1138 |
| Unity.Mathematics.uint4.uint4 | 1132 |
| Unity.Mathematics.int4.int4 | 1113 |
| UnityEditor.ShaderGraph.GraphData.path | 1099 |
| CardEffectCommons.HasMatchConditionOwnersCardInTrash | 1073 |
| CardEffectCommons.PlayPermanentCards | 1073 |
| Photon.Pun.UtilityScripts.CellTreeNode.Draw | 1072 |
| Unity.Mathematics.double4.double4 | 1071 |
| Coffee.UIEffects.UIDissolve.color | 1053 |
| Photon.Chat.AuthenticationValues.ToString | 1053 |
| UnityEngine.Rendering.Universal.Internal.CopyColorPass.source | 1049 |
| EffectTiming.OnAllyAttack | 1035 |
| UnityEditor.Timeline.ClipInspector.EditorClipSelection.clip | 1032 |
| UnityEditor.ShaderGraph.PropertyNode.property | 1025 |
| CardSource.ContainsCardName | 1020 |
| UnityEditor.Rendering.VolumeComponentListEditor.asset | 1020 |
| UnityEditor.U2D.Sprites.SpriteEditorMenu.Styles.Styles | 1010 |
| Unity.Mathematics.bool4.bool4 | 1009 |
| UnityEditor.ShaderGraph.CustomInterpSubGen.Descriptor.dst | 1009 |
| Unity.Mathematics.math.double3 | 996 |
| Microsoft.Unity.VisualStudio.Editor.ProcessRunner.Append | 973 |
| UserSelectionManager.WaitForEndSelect | 964 |
| CardSource.EqualsCardName | 956 |
| Unity.Mathematics.float2.float2 | 955 |
| UnityEditor.Rendering.VolumeComponentEditor.PropertyField | 950 |
| Unity.Mathematics.math.double4 | 935 |
| CardEffectCommons.CanPlayAsNewPermanent | 930 |
| Coffee.UIParticleExtensions.CombineInstanceEx.Clear | 920 |
| Cinemachine.Editor.BaseEditor<T>.FindProperty<TValue> | 906 |
| UnityEngine.Rendering.Universal.ScriptableRendererFeature.SetActive | 906 |
| TMPro.TMP_Text.m_textInfo | 904 |
| EffectTiming.SecuritySkill | 897 |
| Photon.Pun.PhotonNetwork.PhotonNetwork | 890 |
| UnityEngine.Rendering.Universal.LibTessDotNet.PriorityQueue<TValue>.StackItem.r | 883 |
| Player.GetBattleAreaDigimons | 882 |
| UnityEngine.Timeline.DiscreteTime.Max | 877 |
| Unity.Mathematics.int3.int3 | 868 |
| UnityEditor.ShaderGraph.ShaderSpliceUtil.TemplatePreprocessor.Token.s | 863 |
| Unity.Mathematics.uint3.uint3 | 859 |
| UnityEditor.Timeline.Signals.SignalReceiverItem.UnityEventCloner.evt | 859 |
| UnityEngine.Rendering.DebugUI.Widget.parent | 859 |
| ISecurityCheck.player | 858 |
| Unity.Mathematics.Tests.TestUtils.SignedFloatQNaN | 858 |
| Unity.Mathematics.int2.int2 | 850 |
| UnityEditor.Rendering.HierarchicalBox.size | 847 |
| CardSource.CardColors | 842 |
| Cinemachine.Examples.MixingCameraBlend.AxisEnum.X | 842 |
| Unity.Mathematics.Tests.TestUtils.SignedDoubleQNaN | 830 |
| UnityEngine.Rendering.DebugUI.IntField.max | 825 |
| Permanent.AddDigivolutionCardsBottom | 821 |
| UnityEngine.Rendering.Universal.ProjectedTransform.PositionHandleParam.Handle.Y | 816 |
| CardEffectCommons.CanTriggerOnAttack | 815 |
| Permanent.DPBoost.Condition | 815 |
| Unity.Mathematics.math.uint2 | 811 |
| Unity.Burst.Intrinsics.v256.v256 | 807 |
| Coffee.UIExtensions.AnimatableProperty.id | 805 |
| Unity.Mathematics.math.uint4 | 805 |
| Unity.Mathematics.uint2.uint2 | 800 |
| Photon.Realtime.Player.Equals | 790 |
| Microsoft.Unity.VisualStudio.Editor.Messaging.Message.Type | 783 |
| Unity.Mathematics.math.uint3 | 782 |
| Player.SecurityCards | 780 |
| Unity.Mathematics.double3.double3 | 776 |
| Cinemachine.Editor.SerializedPropertyHelper.FindPropertyRelative | 773 |
| UnityEngine.Rendering.Universal.LibTessDotNet.PriorityQueue<TValue>.StackItem.p | 768 |

## Unresolved call 샘플 상위 200개

- `Assets/AddOn/AutoLayout3D/Scripts/LayoutElement3D.cs` `AutoLayout3D.LayoutElement3D.OnDrawGizmos` -> `Gizmos.DrawWireCube` (methodInvocation, line 32)
- `Assets/AddOn/AutoLayout3D/Scripts/LayoutGroup3D.cs` `AutoLayout3D.LayoutGroup3D.elementList` -> `List<LayoutElement3D>` (constructorCall, line 56)
- `Assets/AddOn/AutoLayout3D/Scripts/XAxisLayoutGroup3D.cs` `AutoLayout3D.XAxisLayoutGroup3D.UpdateLayout` -> `sum` (eventSubscription, line 26)
- `Assets/AddOn/AutoLayout3D/Scripts/YAxisLayoutGroup3D.cs` `AutoLayout3D.YAxisLayoutGroup3D.UpdateLayout` -> `sum` (eventSubscription, line 26)
- `Assets/AddOn/AutoLayout3D/Scripts/ZAxisLayoutGroup3D.cs` `AutoLayout3D.ZAxisLayoutGroup3D.UpdateLayout` -> `sum` (eventSubscription, line 26)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `HideFlags.DontSave` (staticMemberAccess, line 66)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `TextureFormat.ARGB32` (staticMemberAccess, line 68)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `noiseTexture.hideFlags` (memberAccessReference, line 69)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `HideFlags.DontSave` (staticMemberAccess, line 69)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `RenderTexture` (constructorCall, line 73)
- `Assets/AddOn/GlitchFx-master/Assets/GlitchFx/GlitchFx.cs` `GlitchFx.SetUpObjects` -> `RenderTexture` (constructorCall, line 74)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 35)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 36)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 37)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 38)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 39)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.modeLabels` -> `GUIContent` (constructorCall, line 40)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.GetExpansionLevel` -> `mode.hasMultipleDifferentValues` (memberAccessReference, line 48)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.GetExpansionLevel` -> `mode.enumValueIndex` (memberAccessReference, line 50)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.GetExpansionLevel` -> `mode.enumValueIndex` (memberAccessReference, line 52)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.GetPropertyHeight` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 60)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.GetPropertyHeight` -> `EditorGUIUtility.standardVerticalSpacing` (staticMemberAccess, line 61)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 68)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 69)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUIUtility.standardVerticalSpacing` (staticMemberAccess, line 69)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUI.IntPopup` (methodInvocation, line 72)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `GUIContent` (constructorCall, line 91)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 92)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `EditorGUIUtility.standardVerticalSpacing` (staticMemberAccess, line 92)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionElementDrawer.OnGUI` -> `GUIContent` (constructorCall, line 95)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/ConstantMotionEditor.cs` `Reaktion.ConstantMotionEditor.OnEnable` -> `GUIContent` (constructorCall, line 115)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/JitterMotionEditor.cs` `Reaktion.JitterMotionEditor.OnEnable` -> `GUIContent` (constructorCall, line 62)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/JitterMotionEditor.cs` `Reaktion.JitterMotionEditor.OnEnable` -> `GUIContent` (constructorCall, line 63)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/JitterMotionEditor.cs` `Reaktion.JitterMotionEditor.OnEnable` -> `GUIContent` (constructorCall, line 64)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/JitterMotionEditor.cs` `Reaktion.JitterMotionEditor.OnInspectorGUI` -> `EditorGUILayout.LabelField` (methodInvocation, line 71)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Editor/Utility/JitterMotionEditor.cs` `Reaktion.JitterMotionEditor.OnInspectorGUI` -> `EditorGUILayout.LabelField` (methodInvocation, line 79)
- `Assets/AddOn/GlitchFx-master/Assets/Reaktion/Utility/ConstantMotion.cs` `Reaktion.ConstantMotion.TransformElement.Initialize` -> `Random.onUnitSphere` (staticMemberAccess, line 52)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.OnEnable` -> `PS.main` (staticMemberAccess, line 26)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.MakeDNA` -> `addp` (eventSubscription, line 55)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.MakeLadder` -> `addp` (eventSubscription, line 84)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.MakeLadder` -> `addp` (eventSubscription, line 93)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.Update` -> `PS.particleCount` (staticMemberAccess, line 113)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.Update` -> `PS.GetParticles` (methodInvocation, line 114)
- `Assets/AddOn/NucleusDNAEffect/Scripts/DNA.cs` `DNA.Update` -> `PS.SetParticles` (methodInvocation, line 126)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.FindObjectOfType` -> `AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .FirstOrDefault` (methodInvocation, line 32)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.FindObjectOfType` -> `AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany` (methodInvocation, line 32)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.FindObjectOfType` -> `AppDomain.CurrentDomain` (staticMemberAccess, line 32)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.FindObjectOfType` -> `x.GetTypes` (methodInvocation, line 33)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.Update` -> `BindingFlags.NonPublic` (staticMemberAccess, line 50)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.Update` -> `BindingFlags.Public` (staticMemberAccess, line 50)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Cartoon FX & War FX Demo/CFX_Demo_With_UIParticle.cs` `CFX_Demo_With_UIParticle.SetCanvasRenderOverlay` -> `Camera.main` (staticMemberAccess, line 75)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Demo/UIParticle_Demo.cs` `Coffee.UIExtensions.Demo.UIParticle_Demo.m_ScalingByUIParticles` -> `List<UIParticle>` (constructorCall, line 12)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Demo/UIParticle_Demo.cs` `Coffee.UIExtensions.Demo.UIParticle_Demo.EnableTrailRibbon` -> `p.trails` (memberAccessReference, line 23)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Demo/UIParticle_Demo.cs` `Coffee.UIExtensions.Demo.UIParticle_Demo.EnableTrailRibbon` -> `ParticleSystemTrailMode.Ribbon` (staticMemberAccess, line 24)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Demo/UIParticle_Demo.cs` `Coffee.UIExtensions.Demo.UIParticle_Demo.EnableTrailRibbon` -> `ParticleSystemTrailMode.PerParticle` (staticMemberAccess, line 24)
- `Assets/AddOn/ParticleEffectForUGUI-main/Samples~/Demo/UIParticle_Demo.cs` `Coffee.UIExtensions.Demo.UIParticle_Demo.SetScale` -> `x.localScale` (memberAccessReference, line 55)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/AnimatableProperty.cs` `Coffee.UIExtensions.AnimatableProperty.UpdateMaterialProperties` -> `mpb.GetVector` (methodInvocation, line 38)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/AnimatableProperty.cs` `Coffee.UIExtensions.AnimatableProperty.UpdateMaterialProperties` -> `mpb.GetFloat` (methodInvocation, line 44)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/AnimatableProperty.cs` `Coffee.UIExtensions.AnimatableProperty.OnAfterDeserialize` -> `Shader.PropertyToID` (methodInvocation, line 62)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/BakingCamera.cs` `Coffee.UIParticleExtensions.BakingCamera.Create` -> `HideFlags.HideAndDontSave` (staticMemberAccess, line 60)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/BakingCamera.cs` `Coffee.UIParticleExtensions.BakingCamera.GetCamera` -> `Camera.main` (staticMemberAccess, line 81)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/BakingCamera.cs` `Coffee.UIParticleExtensions.BakingCamera.GetCamera` -> `Instance._camera.farClipPlane` (memberAccessReference, line 97)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/CombineInstanceEx.cs` `Coffee.UIParticleExtensions.CombineInstanceEx.combineInstances` -> `List<CombineInstance>` (constructorCall, line 11)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/CombineInstanceEx.cs` `Coffee.UIParticleExtensions.CombineInstanceEx.Combine` -> `mesh.CombineMeshes` (methodInvocation, line 29)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/CombineInstanceEx.cs` `Coffee.UIParticleExtensions.CombineInstanceEx.Push` -> `CombineInstance` (constructorCall, line 59)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.s_ActiveNames` -> `List<string>` (constructorCall, line 11)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.s_Sb` -> `System.Text.StringBuilder` (constructorCall, line 12)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.s_Names` -> `HashSet<string>` (constructorCall, line 13)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.CollectActiveNames` -> `sp.GetArrayElementAtIndex` (methodInvocation, line 23)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.CollectActiveNames` -> `result.Aggregate` (methodInvocation, line 36)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.CollectActiveNames` -> `s_Sb.AppendFormat` (methodInvocation, line 36)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `GUILayout.ExpandWidth` (methodInvocation, line 46)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `EditorGUI.PrefixLabel` (methodInvocation, line 48)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `EditorGUILayout.GetControlRect` (methodInvocation, line 48)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `GUIContent` (constructorCall, line 48)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `sp.hasMultipleDifferentValues` (memberAccessReference, line 49)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `GenericMenu` (constructorCall, line 55)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `GUIContent` (constructorCall, line 56)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `sp.ClearArray` (methodInvocation, line 58)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `sp.hasMultipleDifferentValues` (memberAccessReference, line 63)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `sp.GetArrayElementAtIndex` (methodInvocation, line 67)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `ShaderUtil.GetPropertyCount` (methodInvocation, line 79)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `ShaderUtil.GetPropertyName` (methodInvocation, line 81)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.DrawAnimatableProperties` -> `gm.ShowAsContext` (methodInvocation, line 97)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.AddMenu` -> `GUIContent` (constructorCall, line 104)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.AddMenu` -> `sp.DeleteArrayElementAtIndex` (methodInvocation, line 109)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.AddMenu` -> `sp.InsertArrayElementAtIndex` (methodInvocation, line 113)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/AnimatedPropertiesEditor.cs` `Coffee.UIExtensions.AnimatedPropertiesEditor.AddMenu` -> `sp.GetArrayElementAtIndex` (methodInvocation, line 114)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `AssetDatabase.GUIDToAssetPath` (methodInvocation, line 28)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `Path.GetDirectoryName` (methodInvocation, line 29)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `Regex.Match(json, "\"version\"\\s*:\\s*\"([^\"]+)\"").Groups` (staticMemberAccess, line 31)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `string.IsNullOrEmpty` (methodInvocation, line 37)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `EditorUtility.DisplayDialog` (methodInvocation, line 42)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `FileUtil.DeleteFileOrDirectory` (methodInvocation, line 45)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `FileUtil.DeleteFileOrDirectory` (methodInvocation, line 49)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `FileUtil.DeleteFileOrDirectory` (methodInvocation, line 53)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `Path.GetDirectoryName` (methodInvocation, line 55)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `FileUtil.CopyFileOrDirectory` (methodInvocation, line 60)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `DirectoryNotFoundException` (constructorCall, line 62)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.ImportSample` -> `ImportAssetOptions.ImportRecursive` (staticMemberAccess, line 64)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.GetPreviousSamplePath` -> `DirectoryInfo` (constructorCall, line 70)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.GetPreviousSamplePath` -> `sampleRootInfo.GetDirectories()
                .Select(versionDir => Path.Combine(versionDir.ToString(), sampleName))
                .FirstOrDefault` (methodInvocation, line 73)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/ImportSampleMenu.cs` `Coffee.UIExtensions.ImportSampleMenu_UIParticle.GetPreviousSamplePath` -> `sampleRootInfo.GetDirectories` (methodInvocation, line 73)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentRenderingOrder` -> `GUIContent` (constructorCall, line 20)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentRefresh` -> `GUIContent` (constructorCall, line 21)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentFix` -> `GUIContent` (constructorCall, line 22)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentMaterial` -> `GUIContent` (constructorCall, line 23)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentTrailMaterial` -> `GUIContent` (constructorCall, line 24)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_Content3D` -> `GUIContent` (constructorCall, line 25)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_ContentScale` -> `GUIContent` (constructorCall, line 26)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.s_MaskablePropertyNames` -> `List<string>` (constructorCall, line 38)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `ReorderableList` (constructorCall, line 66)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `_ro.elementHeight` (memberAccessReference, line 67)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 67)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `_ro.elementHeightCallback` (memberAccessReference, line 68)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 69)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 70)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `_ro.drawElementCallback` (memberAccessReference, line 71)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUI.BeginDisabledGroup` (methodInvocation, line 73)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `sp.hasMultipleDifferentValues` (memberAccessReference, line 73)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUIUtility.singleLineHeight` (staticMemberAccess, line 75)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `sp.GetArrayElementAtIndex` (methodInvocation, line 76)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUI.ObjectField` (methodInvocation, line 77)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `p.objectReferenceValue` (memberAccessReference, line 82)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `SerializedObject` (constructorCall, line 84)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUI.EndDisabledGroup` (methodInvocation, line 90)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `_ro.drawHeaderCallback` (eventSubscription, line 96)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `EditorGUI.LabelField` (methodInvocation, line 101)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `Rect` (constructorCall, line 101)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `Rect` (constructorCall, line 104)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnEnable` -> `Rect` (constructorCall, line 106)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.MaterialField` -> `EditorGUI.BeginDisabledGroup` (methodInvocation, line 120)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.MaterialField` -> `EditorGUI.ObjectField` (methodInvocation, line 121)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.MaterialField` -> `EditorGUI.EndDisabledGroup` (methodInvocation, line 122)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.MaterialField` -> `sp.GetArrayElementAtIndex` (methodInvocation, line 126)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `EditorGUI.ChangeCheckScope` (constructorCall, line 144)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `current.particles
                .Where` (methodInvocation, line 160)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `EditorGUI.BeginChangeCheck` (methodInvocation, line 167)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `EditorGUI.EndChangeCheck` (methodInvocation, line 169)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `_ro.DoLayoutList` (methodInvocation, line 179)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `mat.HasProperty` (methodInvocation, line 192)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.OnInspectorGUI` -> `EditorGUILayout.HelpBox` (methodInvocation, line 194)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.FixButton` -> `GUILayout.ExpandWidth` (methodInvocation, line 234)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.FixButton` -> `EditorGUILayout.HelpBox` (methodInvocation, line 236)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.FixButton` -> `EditorGUILayout.VerticalScope` (constructorCall, line 237)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `y.hasMultipleDifferentValues` (memberAccessReference, line 252)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `z.hasMultipleDifferentValues` (memberAccessReference, line 253)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUILayout.BeginHorizontal` (methodInvocation, line 255)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.BeginChangeCheck` (methodInvocation, line 258)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.EndChangeCheck` (methodInvocation, line 260)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.BeginChangeCheck` (methodInvocation, line 269)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.EndChangeCheck` (methodInvocation, line 271)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.BeginChangeCheck` (methodInvocation, line 279)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/Editor/UIParticleEditor.cs` `Coffee.UIExtensions.UIParticleEditor.DrawFloatOrVector3Field` -> `EditorGUI.EndChangeCheck` (methodInvocation, line 281)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.Init` -> `List<bool>` (constructorCall, line 15)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.MeshHelper` -> `List<CombineInstanceEx>` (constructorCall, line 20)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.Push` -> `Profiler.BeginSample` (methodInvocation, line 60)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.Push` -> `Profiler.EndSample` (methodInvocation, line 62)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.Push` -> `Profiler.BeginSample` (methodInvocation, line 64)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.Push` -> `Profiler.EndSample` (methodInvocation, line 66)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `Profiler.BeginSample` (methodInvocation, line 87)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `Profiler.EndSample` (methodInvocation, line 89)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `Profiler.BeginSample` (methodInvocation, line 92)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `result.CombineMeshes` (methodInvocation, line 94)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `Profiler.EndSample` (methodInvocation, line 96)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/MeshHelper.cs` `Coffee.UIParticleExtensions.MeshHelper.CombineMesh` -> `result.RecalculateBounds` (methodInvocation, line 98)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.m_Particles` -> `List<ParticleSystem>` (constructorCall, line 43)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle._activeMeshIndices` -> `List<bool>` (constructorCall, line 57)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.s_Components` -> `List<Component>` (constructorCall, line 63)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.s_ParticleSystems` -> `List<ParticleSystem>` (constructorCall, line 64)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.SetParticleSystemInstance` -> `tr.localPosition` (memberAccessReference, line 211)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.RefreshParticles` -> `tsa.uvChannelMask` (memberAccessReference, line 237)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.RefreshParticles` -> `tsa.uvChannelMask` (memberAccessReference, line 238)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.GetModifiedMaterial` -> `StencilOp.Keep` (staticMemberAccess, line 331)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.UpdateMaterialProperties` -> `r.GetPropertyBlock` (methodInvocation, line 375)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.Start` -> `p.subEmitters` (memberAccessReference, line 423)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticle.cs` `Coffee.UIExtensions.UIParticle.Start` -> `p.main` (memberAccessReference, line 423)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.s_ActiveParticles` -> `List<UIParticle>` (constructorCall, line 11)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.BeginSample` (methodInvocation, line 49)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.EndSample` (methodInvocation, line 62)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.BeginSample` (methodInvocation, line 69)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.EndSample` (methodInvocation, line 71)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.BeginSample` (methodInvocation, line 73)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.EndSample` (methodInvocation, line 75)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.BeginSample` (methodInvocation, line 84)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.EndSample` (methodInvocation, line 86)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.BeginSample` (methodInvocation, line 88)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.Refresh` -> `Profiler.EndSample` (methodInvocation, line 90)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.GetScaledMatrix` -> `particle.main` (memberAccessReference, line 114)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.GetScaledMatrix` -> `main.simulationSpace` (memberAccessReference, line 115)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.GetScaledMatrix` -> `main.customSimulationSpace` (memberAccessReference, line 116)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.GetScaledMatrix` -> `main.customSimulationSpace` (memberAccessReference, line 129)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `Profiler.BeginSample` (methodInvocation, line 138)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `Profiler.EndSample` (methodInvocation, line 141)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `root.lossyScale` (memberAccessReference, line 147)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `Profiler.BeginSample` (methodInvocation, line 167)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `Profiler.EndSample` (methodInvocation, line 170)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `currentPs.IsAlive` (methodInvocation, line 174)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `currentPs.particleCount` (memberAccessReference, line 174)
- `Assets/AddOn/ParticleEffectForUGUI-main/Scripts/UIParticleUpdater.cs` `Coffee.UIExtensions.UIParticleUpdater.BakeMesh` -> `Profiler.BeginSample` (methodInvocation, line 179)

## 다음 Goal 06 추천 입력

- `docs/generated/as-is-restart/asis-csharp-call-graph.json`
- `docs/generated/as-is-restart/asis-csharp-call-edge-index.json`
- `docs/generated/as-is-restart/asis-csharp-unresolved-calls.json`
- `docs/generated/as-is-restart/asis-csharp-call-graph-summary.json`

## 금지 범위 준수

- `src/` 아래 C# 코드는 수정하지 않았다.
- headless engine 구현은 수행하지 않았다.
- AS-IS 원본은 수정하지 않았다.
- CardEffect body 구현은 수행하지 않았다.
- C0039 이후 card-porting은 실행하지 않았다.
- headless 필요 여부 최종 판정은 수행하지 않았다.
- capability 분석/구현은 수행하지 않았다.
- existing implementation trust audit은 수행하지 않았다.
- Verified 판정은 수행하지 않았다.
- commit/push는 수행하지 않았다.
