# 로컬 Git 사용 원칙

- 이 작업공간은 remote 없는 로컬 Git만 사용해 변경 추적, diff 검수, 단계별 복구 지점을 확보한다.
- GitHub 업로드를 하지 않는다.
- `git remote add`, `git push`, `git fetch`, `git pull`은 사용하지 않는다.
- remote가 이미 존재하면 제거하지 말고 보고만 한다.
- commit은 사용자 명시 승인 후에만 만든다.
- 각 prompt 단계 시작 전과 완료 후 `git status --short`, `git diff --stat`, `git diff --name-only` 결과를 확인하고 한국어로 보고한다.
- `DCGO/` 하위 Unity 원본 파일은 Source of Truth로 취급하며 명시 요청 없이 수정하지 않는다.
