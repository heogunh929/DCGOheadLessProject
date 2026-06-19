AGENTS.md, docs/progress/LOCAL_GIT_GUIDE.md, docs/codex-prompts/templates/engine_parity_common_constraints.md, 현재 /goal을 따르라.

기존 DCGO Unity 원본 파일은 수정하지 마라.
remote fetch/pull/push는 사용자 승인 없이 실행하지 마라.
사용자 승인 없는 commit은 만들지 마라.
RL 학습 구성은 구현하지 마라.

작업 시작 전 Git 상태와 `DCGO/Assets/Scripts` 변경 여부를 한국어로 보고하라.

# 90 - ChatGPT Handoff 갱신

이번 작업은 문서 갱신만 수행한다.

`docs/progress/CHATGPT_HANDOFF.md` 최상단에 최신 snapshot을 추가한다.

반드시 포함:

- 현재 queue 번호와 목표
- 최신 commit/branch/remote 상태
- git status/diff
- 원본 DCGO 변경 여부
- 변경 파일
- source mapping
- 구현한 공통 layer
- 구현/변경된 카드
- 테스트 결과
- ST1/ST1~ST3/whole-engine gate 구분
- golden/parity 상태
- 남은 위험
- 다음 queue 항목
- ChatGPT에게 검수받을 질문
- 30~50줄 복사용 요약

과거 handoff는 삭제하지 말고 Historical 섹션으로 유지한다.
새 구현과 commit은 하지 않는다.
