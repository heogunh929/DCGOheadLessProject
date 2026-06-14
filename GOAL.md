# Codex /goal 추천 문구

Codex CLI에서 아래처럼 짧게 설정한다.

```text
/goal docs/codex-prompts/README.md의 단계 순서대로 DCGO Unity battle 로직을 RL headless engine으로 이식한다. 신규 개발이 아니라 원본 이식이며, 한 번에 한 단계만 수행하고 분석/요약/테스트 결과는 한국어로 보고한다. 엔진 완성 전 trace는 학습 데이터가 아니라 검증 데이터로만 사용한다.
```

주의:

- `/goal`은 압축파일이나 폴더 안의 프롬프트를 자동 실행하는 명령이 아니다.
- 실제 작업은 `prompts/*.md`를 하나씩 `/mention`해서 진행한다.
