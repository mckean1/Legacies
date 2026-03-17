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

As of the current repo baseline, Legacies is in project bootstrap stage.

Primary near-term goal:
establish the simulation foundation and first vertical slice.

---

## Priority Roadmap

### Phase 0 — Documentation and Canonical Foundation
Status: `IN PROGRESS`

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
Status: `PLANNED`

Goals:
- introduce `SimulationEngine`
- introduce `World` and `WorldDate`
- support monthly ticking
- define stable system execution order
- return structured step results

Success criteria:
- a world can advance deterministically month by month

---

### Phase 2 — Geography and Regional Support Foundation
Status: `PLANNED`

Goals:
- introduce `Region`
- represent regional support/environment state
- establish adjacency/connectivity
- support regional pressure differences

Success criteria:
- regions differ meaningfully in support conditions

---

### Phase 3 — Species and Population Foundation
Status: `PLANNED`

Goals:
- introduce `Species`
- introduce `PopulationGroup`
- support survival/growth/decline pressure
- support persistence and local continuity

Success criteria:
- populations can survive, expand, shrink, or fail based on conditions

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
Status: `PLANNED`

Goals:
- introduce structured chronicle events
- render player-facing chronicle messages
- suppress low-value duplicates
- establish initialization-vs-change rules

Success criteria:
- the simulation produces meaningful readable historical output

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