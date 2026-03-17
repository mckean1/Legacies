# IMPLEMENTED_SYSTEMS_LIST.md

## Purpose

This file is the canonical source of truth for Legacies implementation status and near-term phase ordering.

A feature is not fully complete until:
- the code is implemented
- the relevant docs are updated
- this file reflects the current truth

---

## Status Legend

- `PLANNED` — agreed direction, not started
- `IN PROGRESS` — active work underway
- `PARTIAL` — implemented in some form but incomplete
- `COMPLETE` — implemented and doc-synced
- `BLOCKED` — known target but currently blocked

---

## Current Project State

Legacies now has a real Phase 1 simulation backbone.

Primary near-term goal:
extend the deterministic backbone into deeper geography, movement, society, settlement, and world generation behavior.

---

## Priority Roadmap

### Phase 0 — Documentation and Canonical Foundation
Status: `COMPLETE`

Goals:
- establish project docs
- define architecture boundaries
- define the universal monthly simulation loop
- define core domain concepts
- define world generation philosophy
- define chronicle philosophy
- establish contributor/agent guidance

Deliverables:
- `AGENTS.md`
- `ARCHITECTURE.md`
- `SIMULATION_LOOP.md`
- `DATA_MODEL.md`
- `WORLD_GENERATION.md`
- `CHRONICLE.md`
- `IMPLEMENTED_SYSTEMS_LIST.md`

---

### Phase 1 — SimulationEngine Backbone
Status: `COMPLETE`

Goals:
- introduce `SimulationEngine`
- introduce `World` and `WorldDate`
- support monthly ticking
- define stable system execution order
- return structured step results

Success criteria:
- a world can advance deterministically month by month

Implemented now:
- explicit `ISimulationSystem` contract
- canonical ordered monthly pipeline owned by `SimulationEngine`
- deterministic ordering by system phase and stable registration order
- `SimulationContext` and `SimulationStepResult`
- pacing removed from the domain
- structured chronicle events rendered by the client

---

### Phase 2 — Geography and Regional Support Foundation
Status: `PARTIAL`

Goals:
- introduce `Region`
- represent regional support/environment state
- establish adjacency/connectivity
- support regional pressure differences

Success criteria:
- regions differ meaningfully in support conditions

Current status notes:
- `Region` supports base ecological support, seasonal volatility, environmental pressure, monthly support, and support bands
- `EnvironmentSystem` and `EcologySystem` update regional support deterministically each month
- adjacency/connectivity is not implemented yet

---

### Phase 3 — Species and Population Foundation
Status: `PARTIAL`

Goals:
- introduce `Species`
- introduce `PopulationGroup`
- support survival/growth/decline pressure
- support persistence and local continuity

Success criteria:
- populations can survive, expand, shrink, or fail based on conditions

Current status notes:
- `Species` has simple monthly growth and scarcity decline rates
- `PopulationGroup` tracks size, support pressure, and health
- `PopulationSystem` applies deterministic ecological response each month

---

### Phase 4 — Movement and Spatial Organization
Status: `PLANNED`

Goals:
- allow populations to move between regions
- introduce basic routes/home ranges
- support clustering or relocation under pressure

Success criteria:
- populations respond spatially to ecological reality

---

### Phase 5 — Society Emergence
Status: `PLANNED`

Goals:
- introduce `Society`
- support continuity of social identity
- support formation/splitting/merging where appropriate

Success criteria:
- at least one society can emerge from simulated conditions

---

### Phase 6 — Settlement Foundation
Status: `PLANNED`

Goals:
- introduce `Settlement`
- support anchored occupation
- support founding, persistence, and abandonment

Success criteria:
- the world can produce durable places, not just moving populations

---

### Phase 7 — Chronicle Foundation
Status: `PARTIAL`

Goals:
- introduce structured chronicle events
- render player-facing chronicle messages
- suppress low-value duplicates
- establish initialization-vs-change rules

Success criteria:
- the simulation produces meaningful readable historical output

Current status notes:
- `ChronicleEvent` is a structured domain output
- `ChronicleSystem` emits events from regional and population changes
- the client renders monthly chronicle lines from `SimulationStepResult`
- duplicate suppression is still basic and lens-specific Chronicle curation is deferred

---

### Phase 8 — Basic World Generation Flow
Status: `PLANNED`

Goals:
- initialize a raw world
- run monthly simulation for generation
- detect simple viable starts
- stop generation on honest criteria

Success criteria:
- the game can produce at least one truthful player-start candidate on a basic world

---

### Phase 9 — Focal Selection and Active-Play Handoff
Status: `PLANNED`

Goals:
- present real start candidates
- preserve actual world state
- transition cleanly into active play

Success criteria:
- the player can choose a generated start and continue the same world

---

## Completed Work

### Bootstrap Repository Setup
Status: `COMPLETE`

Notes:
- solution and initial projects exist
- repo is ready for foundational design/docs work

### Phase 1 Backbone Slice
Status: `COMPLETE`

Notes:
- broad simulation systems are registered explicitly and executed in canonical order
- only `CalendarSystem`, `EnvironmentSystem`, `EcologySystem`, `PopulationSystem`, and `ChronicleSystem` have real behavior so far
- `MovementSystem`, `SocialSystem`, `SettlementSystem`, `KnowledgeSystem`, `InteractionSystem`, `PoliticalSystem`, and `EvaluationSystem` are wired placeholders inside the same backbone