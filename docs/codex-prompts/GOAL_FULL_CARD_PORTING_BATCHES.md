# GOAL_FULL_CARD_PORTING_BATCHES

전체 DCGO snapshot card/source scaffold를 source-aligned RL.Engine card effects로 포팅하기 위한 generated subqueue다.

- 이 subqueue는 66번 산출물이며, 각 batch는 하나의 작은 카드/공통 layer/review 범위를 다룬다.
- `CardId#CardIndex@VariantKey` identity를 유지하고 CardId만으로 variant를 평탄화하지 않는다.
- source 불명확 항목은 review batch에서만 다루며 추측 구현하지 않는다.
- Full DCGO Snapshot Completion Gate 전에는 RL Environment/Observation/Reward/Dataset/Trainer를 구현하지 않는다.

- Batch count: 423
- Card-porting batches: 397
- Mechanic-layer batches: 12
- Source-review batches: 14
