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

Legacies now has a real Phase 1 simulation backbone and a canonical pressure-first design foundation.

The pressure model is part of the project canon through `PRESSURES.md`, but the full framework is still only partially implemented in code.

Current implemented pressure foundation:
- regions have early environmental/support state updated by `EnvironmentSystem` and `EcologySystem`
- regions now also have explicit adjacency/connectivity for downstream spatial systems
- populations have early support-pressure/health response updated by `PopulationSystem`
- populations also carry early movement/displacement/opportunity state updated by `MovementSystem`
- pressure-driven social, settlement, interaction, and political behavior remain future work

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
- define the pressure-first causal model
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
- `PRESSURES.md`
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
- this is the early regional pressure foundation, not full region pressure ownership as described in `PRESSURES.md`
- `EnvironmentSystem` and `EcologySystem` update regional support deterministically each month
- `Region` now has explicit deterministic adjacency for movement and later spatial systems

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
- `PopulationGroup` tracks size, support pressure, health, home-region anchoring, recent movement, and early movement/displacement/opportunity state
- this is still an early population pressure foundation, not the full survival/movement/recovery pressure ownership described in `PRESSURES.md`
- `PopulationSystem` applies deterministic ecological response each month

---

### Phase 4 — Movement and Spatial Organization
Status: `PARTIAL`

Goals:
- allow populations to move between regions
- introduce basic routes/home ranges
- support clustering or relocation under pressure

Success criteria:
- populations respond spatially to ecological reality

Current status notes:
- `MovementSystem` now performs deterministic full-group relocation across region adjacency
- movement decisions are pressure-driven from local support strain, health loss, neighboring opportunity, move friction, home anchoring, and recent-move inertia
- chronicle output now surfaces meaningful relocation and displacement outcomes
- routes, home ranges, and deeper spatial organization are still future work

---

### Phase 5 — Society Emergence
Status: `PLANNED`

Goals:
- introduce `Society`
- support continuity of social identity
- support formation/splitting/merging where appropriate

Success criteria:
- at least one society can emerge from simulated conditions

Current status notes:
- pressure-driven social behavior is still future work

---

### Phase 6 — Settlement Foundation
Status: `PLANNED`

Goals:
- introduce `Settlement`
- support anchored occupation
- support founding, persistence, and abandonment

Success criteria:
- the world can produce durable places, not just moving populations

Current status notes:
- pressure-driven settlement viability, founding, and abandonment behavior are still future work

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
- `ChronicleSystem` emits events from regional, population, and movement changes
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
- `CalendarSystem`, `EnvironmentSystem`, `EcologySystem`, `PopulationSystem`, `MovementSystem`, and `ChronicleSystem` now have real behavior
- `SocialSystem`, `SettlementSystem`, `KnowledgeSystem`, `InteractionSystem`, `PoliticalSystem`, and `EvaluationSystem` remain wired placeholders inside the same backbone
- the current pressure-first implementation now spans early environmental, ecological, population, and movement behavior rather than a full end-to-end pressure framework
