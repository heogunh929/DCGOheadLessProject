# 66D_card_effect_capability_dependency_graph

66B 이후 계획 항목이다. 이번 66B 작업에서는 실행하지 않는다.

## 목표

- 각 source effect의 `requiredCapabilities`를 생성하고 유지한다.
- 모든 required capability가 `Verified`인 카드 batch만 executable로 계산한다.
- coarse category dependency만으로 card-porting batch를 실행하지 않는다.
- source effect capability blocker와 affected card count를 machine-readable graph로 제공한다.

## 완료 조건

- source effect별 required capability가 registry와 연결된다.
- generated card batch는 unresolved capability가 있으면 실행되지 않는다.
- blocker 문서화만으로 card-porting batch가 완료 처리되지 않는다.
