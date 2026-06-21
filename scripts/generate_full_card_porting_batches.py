# -*- coding: utf-8 -*-
"""Generate dependency-aware full-card porting batch queue prompts.

Queue 66 is planning-only. It reads the full-card scaffold and validation
baseline, then creates concrete subqueue prompts without implementing effects.
"""

from __future__ import annotations

import argparse
import json
import sys
from collections import Counter, defaultdict
from pathlib import Path
from typing import Any, Iterable

SCRIPT_DIR = Path(__file__).resolve().parent
if str(SCRIPT_DIR) not in sys.path:
    sys.path.insert(0, str(SCRIPT_DIR))

from generate_full_card_pool_manifest import (  # noqa: E402
    assert_current_source_fingerprint,
    assert_source_lock,
    file_sha256,
)


SCHEMA_VERSION = "dcgo.full-card-porting-batches.66.v1"
SCAFFOLD_INDEX = "docs/generated/full-card-source-scaffold/index.json"
BASELINE_REPORT = "docs/generated/full-card-pool-validation-baseline-65.json"
OUTPUT_MANIFEST = "docs/generated/full-card-porting-batches-66.json"
OUTPUT_SUMMARY = "docs/rl-engine/full-card-porting-batches-66.md"
PROMPT_DIR = "docs/codex-prompts/prompts/generated/full-card"
QUEUE_PATH = "docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md"
PROGRESS_PATH = "docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md"
GOAL_PATH = "docs/codex-prompts/GOAL_FULL_CARD_PORTING_BATCHES.md"
ACTIVE_PATH = "docs/codex-prompts/ACTIVE/RUN_NEXT_FULL_CARD_PORTING_BATCHES.md"

MAX_SOURCES_PER_BATCH = 10
MAX_CARDS_PER_BATCH = 35

CATEGORY_ORDER = [
    "existing-layer",
    "draw-search-reveal-hidden",
    "zone-security-recovery",
    "continuous-duration-inherited-linked",
    "attack-security-timing",
    "simultaneous-trigger-priority",
    "replacement-counter-cut-in",
    "special-digivolution-play",
    "high-risk-source-review",
]

CATEGORY_LABELS = {
    "existing-layer": "기존 layer로 즉시 구현 후보",
    "draw-search-reveal-hidden": "draw/search/reveal/hidden information",
    "zone-security-recovery": "zone/security/recovery",
    "continuous-duration-inherited-linked": "continuous/duration/inherited/linked",
    "attack-security-timing": "attack/security timing",
    "simultaneous-trigger-priority": "simultaneous trigger/priority",
    "replacement-counter-cut-in": "replacement/counter/cut-in",
    "special-digivolution-play": "special digivolution/play mechanics",
    "high-risk-source-review": "source 불명확 review",
}

MECHANIC_CATEGORY_KEYWORDS = {
    "replacement-counter-cut-in": ["armor", "barrier", "counter", "decoy", "evade", "fortitude", "materialsave", "partition", "replacement", "save", "would"],
    "special-digivolution-play": ["ace", "appfusion", "ascension", "blast", "burst", "decode", "digixros", "digi-xros", "execute", "fragment", "iceclad", "jogress", "link", "mindlink", "overflow", "progress"],
    "attack-security-timing": ["alliance", "attack", "blitz", "block", "collision", "jamming", "overclock", "piercing", "raid", "retaliation", "rush", "security"],
    "simultaneous-trigger-priority": ["trigger", "priority", "multiple", "aftereffects"],
    "continuous-duration-inherited-linked": ["inherited", "linked", "duration", "modifier", "static", "continuous"],
    "zone-security-recovery": ["zone", "trash", "hand", "deck", "security", "recovery"],
    "draw-search-reveal-hidden": ["draw", "search", "reveal", "selectcard", "selectdeck", "selecthand"],
}


def read_json(path: Path) -> Any:
    with path.open("r", encoding="utf-8-sig") as handle:
        return json.load(handle)


def write_json(path: Path, value: Any) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, ensure_ascii=False, indent=2, sort_keys=False) + "\n", encoding="utf-8")


def write_text(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8")


def load_records(workspace: Path, relative_paths: Iterable[str]) -> list[dict[str, Any]]:
    records: list[dict[str, Any]] = []
    for relative_path in relative_paths:
        records.extend(read_json(workspace / relative_path)["records"])
    return records


def clean_prompt_dir(workspace: Path) -> None:
    prompt_dir = workspace / PROMPT_DIR
    prompt_dir.mkdir(parents=True, exist_ok=True)
    for existing in prompt_dir.glob("*.md"):
        try:
            existing.unlink()
        except PermissionError:
            # Windows can briefly hold generated markdown files after prior test
            # reads. The current run overwrites every referenced prompt below;
            # stale files are ignored by the generated queue/manifest.
            pass


def classify_source(source: dict[str, Any]) -> str:
    flags = set(name for name, enabled in source.get("flags", {}).items() if enabled)
    timings = set(source.get("timings") or [])
    selections = set(source.get("selectionKinds") or [])
    root_zones = set(source.get("rootZones") or [])
    api_text = " ".join(
        list(source.get("factoryApi") or [])
        + list(source.get("commonsApi") or [])
        + list(source.get("classApi") or [])
        + list(source.get("setMethods") or []))
    lower_api = api_text.lower()

    if "replacement_or_cut_in" in flags or "counter" in flags or "OnCounterTiming" in timings:
        return "replacement-counter-cut-in"
    if selections & {"SelectJogress", "SelectDigiXros", "SelectAssembly", "SelectAppFusion", "SelectBurstDigivolution"}:
        return "special-digivolution-play"
    if any(token in lower_api for token in ["jogress", "digixros", "digi-xros", "assembly", "appfusion", "burst", "link"]):
        return "special-digivolution-play"
    if "trigger_priority" in flags or "background" in flags or "AfterEffectsActivate" in timings:
        return "simultaneous-trigger-priority"
    if selections & {"SelectAttackTarget"} or timings & {"OnAllyAttack", "OnBlockAnyone", "OnEndAttack", "OnAttackTargetChanged"}:
        return "attack-security-timing"
    if "SecuritySkill" in timings or selections & {"SelectSecurity"} or "Security" in root_zones:
        return "zone-security-recovery"
    if "zone_movement" in flags or root_zones & {"Hand", "Trash", "Deck", "Security"}:
        return "zone-security-recovery"
    if flags & {"static_or_continuous", "inherited", "linked", "modifier_duration"}:
        return "continuous-duration-inherited-linked"
    if selections & {"SelectCard", "SelectDeck", "SelectHand"} or any(token in lower_api for token in ["draw", "search", "reveal"]):
        return "draw-search-reveal-hidden"
    return "existing-layer"


def classify_mechanic(section: str, item: dict[str, Any]) -> str:
    text = f"{section} {item.get('name', '')}".lower().replace("_", "")
    for category, keywords in MECHANIC_CATEGORY_KEYWORDS.items():
        if any(keyword in text for keyword in keywords):
            return category
    return "existing-layer"


def compact_card(card: dict[str, Any]) -> dict[str, Any]:
    return {
        "definitionStableId": card["definitionStableId"],
        "cardId": card["cardId"],
        "cardIndex": card["cardIndex"],
        "variantKey": card["variantKey"],
        "assetPath": card["assetPath"],
        "status": card.get("mappingStatus", card.get("status")),
    }


def source_cards(source: dict[str, Any]) -> list[dict[str, Any]]:
    return [compact_card(card) for card in source.get("affectedCards") or []]


def batch_chunks(sources: list[dict[str, Any]]) -> list[list[dict[str, Any]]]:
    chunks: list[list[dict[str, Any]]] = []
    current: list[dict[str, Any]] = []
    current_card_count = 0
    for source in sources:
        cards = source.get("affectedCards") or []
        proposed_source_count = len(current) + 1
        proposed_card_count = current_card_count + len(cards)
        if current and (proposed_source_count > MAX_SOURCES_PER_BATCH or proposed_card_count > MAX_CARDS_PER_BATCH):
            chunks.append(current)
            current = []
            current_card_count = 0
        current.append(source)
        current_card_count += len(cards)
    if current:
        chunks.append(current)
    return chunks


def make_batch_id(index: int, kind: str, category: str) -> str:
    prefix = {
        "mechanic-layer": "L",
        "card-porting": "C",
        "source-review": "R",
    }[kind]
    return f"{prefix}{index:04d}_{category.replace('-', '_')}"


def build_batches(
    cards: list[dict[str, Any]],
    sources: list[dict[str, Any]],
    inventory: dict[str, Any],
    full_manifest_records: list[dict[str, Any]]) -> list[dict[str, Any]]:
    batches: list[dict[str, Any]] = []
    index = 1
    card_mapping_by_id = {card["definitionStableId"]: card for card in cards}
    keyword_cards: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for record in full_manifest_records:
        mapping = card_mapping_by_id.get(record["definitionStableId"])
        if mapping is None:
            continue
        for keyword in record.get("keywords") or []:
            keyword_cards[str(keyword)].append(compact_card(mapping))

    mechanic_items: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for section in ["timings", "features", "selections", "specialMechanics", "keywords"]:
        for item in inventory.get(section, []):
            if item.get("mappingStatus") in {"Unsupported", "PartiallyImplemented", "NeedsSourceReview"}:
                category = classify_mechanic(section, item)
                affected_cards = [compact_card(card) for card in item.get("affectedCards", [])[:25]]
                if section == "keywords" and not affected_cards:
                    affected_cards = keyword_cards.get(item.get("name"), [])[:25]
                mechanic_items[category].append({
                    "section": section,
                    "name": item.get("name"),
                    "mappingStatus": item.get("mappingStatus"),
                    "affectedCardCount": item.get("affectedCardCount", item.get("cardTextKeywordRecordCount", len(affected_cards))),
                    "sourceFileCount": item.get("sourceFileCount", item.get("sourceScopeFileCount", 0)),
                    "affectedCards": affected_cards,
                })

    sources_by_category: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for source in sources:
        sources_by_category[classify_source(source)].append(source)

    needs_review_cards = [card for card in cards if card.get("mappingStatus") == "NeedsSourceReview"]
    review_by_set: dict[str, list[dict[str, Any]]] = defaultdict(list)
    for card in needs_review_cards:
        review_by_set[card["setId"]].append(card)

    for category in CATEGORY_ORDER:
        if category == "high-risk-source-review":
            continue

        mechanics = sorted(mechanic_items.get(category, []), key=lambda item: (item["section"], item["name"] or ""))
        for offset in range(0, len(mechanics), 8):
            items = mechanics[offset:offset + 8]
            if not items:
                continue
            batch_id = make_batch_id(index, "mechanic-layer", category)
            batches.append({
                "batchId": batch_id,
                "kind": "mechanic-layer",
                "category": category,
                "status": "todo",
                "title": f"{CATEGORY_LABELS[category]} common layer blocker {offset // 8 + 1}",
                "dependencyBatchIds": [],
                "mechanicItems": items,
                "sourceEffects": [],
                "cards": sorted({card["definitionStableId"]: card for item in items for card in item.get("affectedCards", [])}.values(), key=lambda card: card["definitionStableId"]),
            })
            index += 1

        category_sources = sorted(
            sources_by_category.get(category, []),
            key=lambda source: (source.get("sourceEffectClassName", ""), source.get("sourcePath", "")))
        layer_dependencies = [batch["batchId"] for batch in batches if batch["kind"] == "mechanic-layer" and batch["category"] == category]
        for chunk_number, chunk in enumerate(batch_chunks(category_sources), start=1):
            batch_id = make_batch_id(index, "card-porting", category)
            source_records = [
                {
                    "sourceScaffoldId": source["sourceScaffoldId"],
                    "sourceEffectClassName": source["sourceEffectClassName"],
                    "sourcePath": source["sourcePath"],
                    "plannedRlSourceFile": source["plannedRlSourceFile"],
                    "timings": source.get("timings", []),
                    "flags": sorted([name for name, enabled in source.get("flags", {}).items() if enabled]),
                    "selectionKinds": source.get("selectionKinds", []),
                    "affectedCardCount": source.get("affectedCardCount", 0),
                }
                for source in chunk
            ]
            cards_for_batch = sorted(
                {card["definitionStableId"]: card for source in chunk for card in source_cards(source)}.values(),
                key=lambda card: card["definitionStableId"])
            batches.append({
                "batchId": batch_id,
                "kind": "card-porting",
                "category": category,
                "status": "todo",
                "title": f"{CATEGORY_LABELS[category]} card porting {chunk_number}",
                "dependencyBatchIds": layer_dependencies,
                "mechanicItems": [],
                "sourceEffects": source_records,
                "cards": cards_for_batch,
            })
            index += 1

    for set_id in sorted(review_by_set):
        cards_for_set = sorted(review_by_set[set_id], key=lambda card: (card["cardId"], card["cardIndex"], card["variantKey"]))
        for offset in range(0, len(cards_for_set), 10):
            chunk = cards_for_set[offset:offset + 10]
            batch_id = make_batch_id(index, "source-review", "high-risk-source-review")
            batches.append({
                "batchId": batch_id,
                "kind": "source-review",
                "category": "high-risk-source-review",
                "status": "todo",
                "title": f"{set_id} source body review {offset // 10 + 1}",
                "dependencyBatchIds": [],
                "mechanicItems": [],
                "sourceEffects": [],
                "cards": [compact_card(card) | {
                    "cardEffectClassName": card.get("cardEffectClassName"),
                    "sourceEffectPaths": card.get("sourceEffectPaths", []),
                    "statusReason": card.get("statusReason", ""),
                } for card in chunk],
            })
            index += 1

    return batches


def prompt_path_for(batch: dict[str, Any]) -> str:
    return f"{PROMPT_DIR}/{batch['batchId']}.md"


def write_batch_prompt(workspace: Path, batch: dict[str, Any]) -> None:
    cards = batch["cards"]
    sources = batch["sourceEffects"]
    mechanics = batch["mechanicItems"]
    lines = [
        f"# {batch['batchId']} - {batch['title']}",
        "",
        "AGENTS.md, docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md, 현재 /goal을 따르라.",
        "",
        "- `DCGO/Assets` 원본은 수정하지 마라.",
        "- Codex는 commit과 push를 하지 마라.",
        "- Full DCGO Snapshot Completion Gate 전에는 RL 구성요소를 구현하지 마라.",
        "- 카드별 effect body는 원본 CardEffect 경로에 대응되는 카드별 파일에 둔다.",
        "- core service, Catalog, 공통 validator에 특정 CardId 분기를 추가하지 마라.",
        "- source body가 불명확하면 추측 구현하지 말고 blocker로 유지하라.",
        "",
        "## Batch Metadata",
        "",
        f"- BatchId: `{batch['batchId']}`",
        f"- Kind: `{batch['kind']}`",
        f"- Category: `{batch['category']}` / {CATEGORY_LABELS[batch['category']]}",
        f"- Dependencies: {', '.join(f'`{dep}`' for dep in batch['dependencyBatchIds']) if batch['dependencyBatchIds'] else 'none'}",
        f"- Card identity count: {len(cards)}",
        f"- Source effect count: {len(sources)}",
        f"- Mechanic blocker count: {len(mechanics)}",
        "",
    ]
    if mechanics:
        lines.extend(["## Mechanic / Layer Items", ""])
        lines.extend(["| Section | Name | Status | Affected cards | Source files |", "| --- | --- | --- | ---: | ---: |"])
        for item in mechanics:
            lines.append(f"| `{item['section']}` | `{item['name']}` | `{item['mappingStatus']}` | {item['affectedCardCount']} | {item['sourceFileCount']} |")
        lines.append("")
    if sources:
        lines.extend(["## Source Effects", ""])
        lines.extend(["| SourceEffectClassName | Source path | Timings | Flags | Selections | Affected cards |", "| --- | --- | --- | --- | --- | ---: |"])
        for source in sources:
            lines.append(
                f"| `{source['sourceEffectClassName']}` | `{source['sourcePath']}` | `{', '.join(source['timings']) or '-'}` | `{', '.join(source['flags']) or '-'}` | `{', '.join(source['selectionKinds']) or '-'}` | {source['affectedCardCount']} |")
        lines.append("")
    if cards:
        lines.extend(["## Card / Variant Identities", ""])
        lines.extend(["| DefinitionStableId | CardId | CardIndex | Variant | Status | Asset path |", "| --- | --- | ---: | --- | --- | --- |"])
        for card in cards:
            lines.append(f"| `{card['definitionStableId']}` | `{card['cardId']}` | {card['cardIndex']} | `{card['variantKey']}` | `{card.get('status', '')}` | `{card['assetPath']}` |")
        lines.append("")
    lines.extend([
        "## Required Work",
        "",
    ])
    if batch["kind"] == "source-review":
        lines.extend([
            "- 원본 source body가 실제로 존재하는지 asset path, CardEffectClassName, source lookup alias를 다시 대조한다.",
            "- source body가 없으면 `NeedsSourceReview`를 유지하고 구현하지 않는다.",
            "- source body를 찾으면 64 scaffold/65 baseline의 source mapping을 갱신하는 별도 queue 항목으로 넘긴다.",
        ])
    elif batch["kind"] == "mechanic-layer":
        lines.extend([
            "- 나열된 mechanic/layer 항목의 DCGO 원본 source 위치와 RL.Engine 공통 layer 대응을 먼저 문서화한다.",
            "- 공통 service/primitive/selection/trigger boundary가 필요한 경우 카드 구현 전에 layer를 먼저 추가한다.",
            "- 이 batch에서 카드별 effect body를 대량 구현하지 말고, layer와 대표 fixture만 검증한다.",
        ])
    else:
        lines.extend([
            "- Source Effects 표의 원본 파일을 먼저 읽고, 대응 카드별 RL effect 파일 또는 wrapper/mapping을 만든다.",
            "- 같은 source effect를 여러 variant가 참조해도 CardId만으로 평탄화하지 않는다.",
            "- 필요한 공통 helper는 source 의미를 숨기지 않는 범위에서만 추가한다.",
            "- 구현 후 65 baseline에서 이 batch의 `Unsupported` blocker가 줄어드는지 확인한다.",
        ])
    lines.extend([
        "",
        "## Tests / Validation",
        "",
        "- 대상 카드별 unit/golden/replay 테스트를 추가하거나 갱신한다.",
        "- `FullCardPoolValidator` deck subset 또는 batch-specific validation을 추가한다.",
        "- 전체 regression을 실행한다.",
        "- `DCGO/Assets/Scripts` 변경 없음과 tracked `bin/obj` 없음 확인을 보고한다.",
        "",
        "## Blocker Conditions",
        "",
        "- source body missing/ambiguous",
        "- 원본 timing/selection/ordering 의미 미확인",
        "- 공통 layer가 없는 mechanic을 카드별 workaround로 구현해야 하는 상황",
        "- core service에 CardId 분기를 넣어야만 통과하는 설계",
    ])
    write_text(workspace / prompt_path_for(batch), "\n".join(lines) + "\n")


def write_control_files(workspace: Path, batches: list[dict[str, Any]], manifest: dict[str, Any]) -> None:
    write_text(workspace / GOAL_PATH, "\n".join([
        "# GOAL_FULL_CARD_PORTING_BATCHES",
        "",
        "전체 DCGO snapshot card/source scaffold를 source-aligned RL.Engine card effects로 포팅하기 위한 generated subqueue다.",
        "",
        "- 이 subqueue는 66번 산출물이며, 각 batch는 하나의 작은 카드/공통 layer/review 범위를 다룬다.",
        "- `CardId#CardIndex@VariantKey` identity를 유지하고 CardId만으로 variant를 평탄화하지 않는다.",
        "- source 불명확 항목은 review batch에서만 다루며 추측 구현하지 않는다.",
        "- Full DCGO Snapshot Completion Gate 전에는 RL Environment/Observation/Reward/Dataset/Trainer를 구현하지 않는다.",
        "",
        f"- Batch count: {manifest['summary']['batchCount']}",
        f"- Card-porting batches: {manifest['summary']['cardPortingBatchCount']}",
        f"- Mechanic-layer batches: {manifest['summary']['mechanicLayerBatchCount']}",
        f"- Source-review batches: {manifest['summary']['sourceReviewBatchCount']}",
    ]) + "\n")

    write_text(workspace / ACTIVE_PATH, "\n".join([
        "# RUN_NEXT_FULL_CARD_PORTING_BATCHES",
        "",
        "사용자가 다음처럼 입력하면:",
        "",
        "```text",
        "다음 full-card-porting batch 작업을 진행해.",
        "```",
        "",
        "Codex는 다음 절차를 따른다.",
        "",
        "1. `docs/codex-prompts/state/QUEUE_FULL_CARD_PORTING_BATCHES.md`를 위에서 아래로 읽는다.",
        "2. 가장 먼저 등장하는 `todo` 항목 하나만 `in-progress`로 바꾼다.",
        "3. 해당 prompt 파일과 `docs/codex-prompts/templates/dcgo_full_card_pool_constraints.md`를 읽는다.",
        "4. 한 batch만 수행한다.",
        "5. 완료 시 `done`, `blocked`, `needs-review` 중 하나로 상태를 바꾼다.",
        "6. `docs/codex-prompts/state/PROGRESS_FULL_CARD_PORTING_BATCHES.md`에 변경, 테스트, blocker를 기록한다.",
        "7. main `QUEUE_DCGO_FULL_CARD_POOL.md`의 67번은 generated subqueue가 모두 끝난 뒤 실행한다.",
        "8. commit/push는 하지 않고 추천 commit message만 보고한다.",
    ]) + "\n")

    queue_lines = [
        "# QUEUE_FULL_CARD_PORTING_BATCHES",
        "",
        "상태 값: `todo`, `in-progress`, `done`, `blocked`, `needs-review`.",
        "",
        "| Order | Status | Prompt file | Kind | Category | Cards | Sources | Purpose |",
        "| --- | --- | --- | --- | --- | ---: | ---: | --- |",
    ]
    for order, batch in enumerate(batches, start=1):
        queue_lines.append(
            f"| {order:04d} | todo | {prompt_path_for(batch)} | {batch['kind']} | {batch['category']} | {len(batch['cards'])} | {len(batch['sourceEffects'])} | {batch['title']} |")
    write_text(workspace / QUEUE_PATH, "\n".join(queue_lines) + "\n")

    write_text(workspace / PROGRESS_PATH, "\n".join([
        "# PROGRESS_FULL_CARD_PORTING_BATCHES",
        "",
        "| Date | Batch | Summary | Source snapshot | Tests | Blockers / Next |",
        "| --- | --- | --- | --- | --- | --- |",
        f"| 2026-06-21 | 66 bootstrap | Generated {manifest['summary']['batchCount']} full-card porting batches from 62~65 manifests. | local manifest | `python .\\scripts\\generate_full_card_porting_batches.py --workspace .` | 첫 todo batch부터 진행 |",
    ]) + "\n")


def write_summary(workspace: Path, manifest: dict[str, Any]) -> None:
    summary = manifest["summary"]
    lines = [
        "# Full Card Porting Batches - Queue 66",
        "",
        "66번은 전체 카드풀 포팅을 직접 수행하지 않고, 62~65 산출물에서 dependency-aware generated subqueue를 만든다.",
        "",
        "## Summary",
        "",
        f"- Batch count: {summary['batchCount']}",
        f"- Mechanic-layer batches: {summary['mechanicLayerBatchCount']}",
        f"- Card-porting batches: {summary['cardPortingBatchCount']}",
        f"- Source-review batches: {summary['sourceReviewBatchCount']}",
        f"- Source scaffolds assigned: {summary['sourceScaffoldAssignedCount']} / {summary['sourceScaffoldTotalCount']}",
        f"- NeedsSourceReview cards assigned: {summary['needsSourceReviewAssignedCount']} / {summary['needsSourceReviewTotalCount']}",
        "",
        "## Category Counts",
        "",
    ]
    for category, count_value in summary["categoryCounts"].items():
        lines.append(f"- `{category}`: {count_value}")
    lines.extend([
        "",
        "## Generated Control Files",
        "",
        f"- `{GOAL_PATH}`",
        f"- `{ACTIVE_PATH}`",
        f"- `{QUEUE_PATH}`",
        f"- `{PROGRESS_PATH}`",
        f"- `{PROMPT_DIR}/*.md`",
        "",
        f"Machine-readable manifest: `{OUTPUT_MANIFEST}`",
    ])
    write_text(workspace / OUTPUT_SUMMARY, "\n".join(lines) + "\n")


def build_manifest(workspace: Path) -> dict[str, Any]:
    lock = assert_source_lock(workspace)
    assert_current_source_fingerprint(workspace, lock)

    index = read_json(workspace / SCAFFOLD_INDEX)
    baseline = read_json(workspace / BASELINE_REPORT)
    inventory = read_json(workspace / index["inputManifests"]["fullMechanicInventory"]["path"])
    full_card_manifest = read_json(workspace / index["inputManifests"]["fullCardPoolManifest"]["path"])
    cards = load_records(workspace, index["generatedFiles"]["cardMappings"])
    sources = load_records(workspace, index["generatedFiles"]["sourceScaffolds"])
    batches = build_batches(cards, sources, inventory, full_card_manifest["records"])

    for batch in batches:
        batch["promptFile"] = prompt_path_for(batch)

    source_ids_assigned = {
        source["sourceScaffoldId"]
        for batch in batches
        for source in batch["sourceEffects"]
    }
    needs_review_total = [card for card in cards if card.get("mappingStatus") == "NeedsSourceReview"]
    needs_review_assigned = {
        card["definitionStableId"]
        for batch in batches
        if batch["kind"] == "source-review"
        for card in batch["cards"]
    }
    category_counts = count([batch["category"] for batch in batches])
    kind_counts = count([batch["kind"] for batch in batches])

    return {
        "schemaVersion": SCHEMA_VERSION,
        "inputManifests": {
            "scaffoldIndex": {"path": SCAFFOLD_INDEX, "sha256": file_sha256(workspace / SCAFFOLD_INDEX)},
            "validationBaseline": {"path": BASELINE_REPORT, "sha256": file_sha256(workspace / BASELINE_REPORT)},
            "fullMechanicInventory": index["inputManifests"]["fullMechanicInventory"],
        },
        "batchPolicy": {
            "maxSourcesPerBatch": MAX_SOURCES_PER_BATCH,
            "maxCardsPerBatch": MAX_CARDS_PER_BATCH,
            "categoryOrder": CATEGORY_ORDER,
            "sourceUnclearCardsAreReviewOnly": True,
            "effectBodiesChanged": False,
            "rlComponentsGenerated": False,
        },
        "summary": {
            "batchCount": len(batches),
            "mechanicLayerBatchCount": kind_counts.get("mechanic-layer", 0),
            "cardPortingBatchCount": kind_counts.get("card-porting", 0),
            "sourceReviewBatchCount": kind_counts.get("source-review", 0),
            "sourceScaffoldTotalCount": len(sources),
            "sourceScaffoldAssignedCount": len(source_ids_assigned),
            "needsSourceReviewTotalCount": len(needs_review_total),
            "needsSourceReviewAssignedCount": len(needs_review_assigned),
            "baselineBlockingIssueTotal": baseline["blockingIssueTotal"],
            "categoryCounts": category_counts,
        },
        "batches": batches,
    }


def count(values: Iterable[str]) -> dict[str, int]:
    return dict(sorted(Counter(values).items()))


def main() -> None:
    parser = argparse.ArgumentParser()
    parser.add_argument("--workspace", default=".", help="Workspace root")
    args = parser.parse_args()

    workspace = Path(args.workspace).resolve()
    clean_prompt_dir(workspace)
    manifest = build_manifest(workspace)
    for batch in manifest["batches"]:
        write_batch_prompt(workspace, batch)
    write_json(workspace / OUTPUT_MANIFEST, manifest)
    write_control_files(workspace, manifest["batches"], manifest)
    write_summary(workspace, manifest)
    print(json.dumps(manifest["summary"], ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
