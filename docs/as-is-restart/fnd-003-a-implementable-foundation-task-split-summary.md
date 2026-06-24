# FND-003-A Summary

## Progress Update

- `FND-003-B` `OnRemovedField` primitive scope completed after this split.
- `FND-003-C` `AfterPayCost` runtime scope completed after `FND-003-B`.
- `FND-003-D` `OnDiscardSecurity` primitive event scope completed after `FND-003-C`.
- `FND-003-E` `OnAddSecurity` primitive event scope completed after `FND-003-D`.
- `FND-003-F` `OnDiscardLibrary` primitive event scope completed after `FND-003-E`.
- `FND-003-G` `OnUseOption` runtime scope completed after `FND-003-F`.
- `FND-003-H` `OnUnTappedAnyone` primitive event scope completed after `FND-003-G`.
- `FND-003-I` `OnMove` primitive event scope completed after `FND-003-H`.
- `FND-003-J` `OnAddDigivolutionCards` primitive source-add event scope completed after `FND-003-I`.
- `FND-003-K` `OnDigivolutionCardDiscarded` primitive source-trash event scope completed after `FND-003-J`.
- `FND-003-L` `OnEndBattle` battle-result runtime scope completed after `FND-003-K`.
- `FND-003-M` `OnDetermineDoSecurityCheck` battle security-check decision runtime scope completed after `FND-003-L`.
- `FND-003-N` `BeforePayCost` pre-cost runtime scope completed after `FND-003-M`.
- `FND-003-O` `OnTappedAnyone` actual suspend primitive and attack/block immediate-drain runtime scope completed after `FND-003-N`.
- `FND-003-P` `OnDeclaration` legal action and selected declaration execution scope completed after `FND-003-O`.
- Verification: `.\\.dotnet\\dotnet.exe run --project .\\src\\DCGO.RL.Engine.Tests\\DCGO.RL.Engine.Tests.csproj` -> `All 633 tests passed.`
- Foundation Gate remains `OpenCodeReady=false`.
- Next program goal recommendation: `FND-001-A` `ContinuousOrStaticEffect`.

- ImplementableFoundationTask: 15
- Split child goals: 45
- SourceContract: 15
- RuntimeIntegration: 15
- TestAndParityCandidate: 15
- Low/Medium/High parent goals: 4 / 6 / 5
- DATA-001 policy used: true
- OpenCodeReady: `False`

## 異붿쿇 ?ㅽ뻾 ?쒖꽌

1. `FND-003-B` `OnRemovedField` - affected 4, source files 2
2. `FND-003-C` `AfterPayCost` - affected 15, source files 7
3. `FND-003-D` `OnDiscardSecurity` - affected 23, source files 14
4. `FND-003-E` `OnAddSecurity` - affected 38, source files 14
5. `FND-003-F` `OnDiscardLibrary` - affected 51, source files 20
6. `FND-003-G` `OnUseOption` - affected 65, source files 30
7. `FND-003-H` `OnUnTappedAnyone` - affected 70, source files 29
8. `FND-003-I` `OnMove` - affected 79, source files 30
9. `FND-003-J` `OnAddDigivolutionCards` - affected 102, source files 50
10. `FND-003-K` `OnDigivolutionCardDiscarded` - affected 121, source files 53
11. `FND-003-L` `OnEndBattle` - affected 160, source files 84
12. `FND-003-M` `OnDetermineDoSecurityCheck` - affected 228, source files 119
13. `FND-003-N` `BeforePayCost` - affected 284, source files 141
14. `FND-003-O` `OnTappedAnyone` - affected 306, source files 139
15. `FND-003-P` `OnDeclaration` - affected 578, source files 298

## ?ㅼ쓬 利됱떆 ?꾨낫

- `FND-001-A` `ContinuousOrStaticEffect` CloseableFoundationTask 첫 묶음을 처리한다.

추천 commit message: `feat: add OnDeclaration foundation activation path`
