# Codex 사용 가이드

## 기본 흐름

1. `headlessDCGO` 루트에서 Codex 실행
2. `/goal`로 전체 목표 설정
3. `docs/codex-prompts/prompts/*.md`를 하나씩 `/mention`
4. 각 단계 완료 후 diff, 테스트, 요약 확인
5. 문제가 있으면 다음 단계로 넘어가지 않고 수정

## 추천 /goal

```text
/goal docs/codex-prompts/README.md의 단계 순서대로 DCGO Unity battle 로직을 RL headless engine으로 이식한다. 신규 개발이 아니라 원본 이식이며, 한 번에 한 단계만 수행하고 한국어로 보고한다. 엔진 완성 전 trace는 학습 데이터가 아니라 검증 데이터로만 사용한다.
```

## 주의

- 압축파일째 `/goal`로 실행하지 않는다.
- 압축은 풀어서 폴더로 둔다.
- `/goal`은 자동 실행 명령이 아니라 지속 목표다.
- 실제 실행은 단계별 프롬프트를 명시해서 진행한다.
