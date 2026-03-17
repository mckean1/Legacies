# SIMULATION_LOOP.md

## Purpose

This document defines the universal monthly simulation pipeline for Legacies.

This loop should be the backbone for both World Generation and Active Play.

---

## Core Rule

Legacies advances in monthly ticks.

A month is the canonical simulation step because it is:
- frequent enough for ecological and social evolution
- coarse enough to remain performant
- readable enough for player-facing history

Seasonality can be layered on top of monthly simulation rather than replacing it.

---

## Universal Monthly Loop

The SimulationEngine should process each month in a stable order.

The engine owns this ordering. Systems do not decide runtime order themselves, and the client does not orchestrate the pipeline.

## Canonical Phase 1 Order

### 1. CalendarSystem
Update current month/year/seasonal context.

Outputs:
- current world date
- seasonal context flags if needed

### 2. EnvironmentSystem
Update regional environmental state.

Examples:
- climate pressure
- seasonal support changes
- local fertility/productivity shifts
- hazards or stressors

### 3. EcologySystem
Resolve what each region can currently support.

Examples:
- food/support capacity
- ecological pressure
- scarcity or abundance signals
- pressure on mobile vs anchored populations

### 4. PopulationSystem
Update biological and demographic state.

Examples:
- population change
- health/viability pressure
- local persistence
- species continuity/extinction pressure

### 5. MovementSystem
Resolve how populations respond spatially.

Examples:
- migration
- seasonal movement
- retreat
- clustering
- route usage
- home-range stabilization

### 6. SocialSystem
Resolve social cohesion and group identity.

Examples:
- band/tribe/society continuity
- splits/merges
- identity persistence
- social organization deepening

### 7. SettlementSystem
Resolve anchored occupation and settlement state.

Examples:
- camp continuity
- recurring sites
- permanent settlements
- abandonment
- territorial core formation

### 8. KnowledgeSystem
Resolve new learned capabilities or retained knowledge.

Examples:
- adaptation
- discovery spread
- production improvements
- organizational development

### 9. InteractionSystem
Resolve contact between societies/polities/populations.

Examples:
- exchange
- competition
- intimidation
- conflict
- coexistence
- influence

### 10. PoliticalSystem
Resolve deeper organizational outcomes.

Examples:
- society maturation
- polity emergence
- authority structure strengthening
- internal cohesion changes

### 11. ChronicleSystem
Identify meaningful world changes worth surfacing to the player.

This stage should produce structured event candidates, not raw text first.

### 12. EvaluationSystem
Run observer/evaluator logic needed by world generation and other meta systems.

Examples:
- readiness checks
- focal candidate evaluation
- worldgen stopping logic
- other derived snapshots

---

## Why This Order

This order preserves cause-and-effect:

- environment affects support
- support affects populations
- populations affect movement
- movement affects social and settlement stability
- stability affects discovery, interaction, and politics
- outcomes then feed the Chronicle and evaluation layers

Later systems should slot into this causal spine rather than bypass it.

---

## World Generation vs Active Play

### World Generation
Uses the same monthly loop.
The difference is:
- no player directives yet
- special evaluation layers may watch the world
- a stop-condition system determines when player selection becomes possible

### Active Play
Also uses the same monthly loop.
The difference is:
- player-facing views are active
- player influence/control may exist
- the same world keeps evolving without reset

---

## Event and Chronicle Rule

Chronicle text should not be generated directly inside core simulation logic where possible.

Prefer:
1. systems detect meaningful outcomes
2. systems emit structured event candidates
3. chronicle rules decide what is worth surfacing
4. the client renders player-facing text

In Phase 1, the monthly tick returns a `SimulationStepResult` containing:
- start and end date
- executed systems in order
- structured chronicle events emitted that month
- lightweight region and population change summaries
- optional notes

This keeps simulation truth and presentation cleaner.

---

## Determinism Expectations

The monthly loop should be deterministic for the same:
- seed
- initial world
- run configuration
- player inputs

Avoid:
- unstable iteration order
- hidden time-based randomness
- client-driven world mutations

Phase 1 rule:
- systems are ordered first by canonical phase and then by stable registration order

---

## Early Implementation Goal

The first implementation of the monthly loop does not need every future subsystem.

Phase 1 currently implements real behavior for:
- date progression
- environmental update
- ecology/support update
- population update
- chronicle event generation

The remaining broad systems are wired into the same ordered pipeline as placeholders so later work extends the same backbone instead of replacing it.

That is enough to prove the backbone.

---

## Success Criteria

The loop is working well when:
- one month leads naturally into the next
- outcomes are explainable from prior state
- adding systems does not require changing the entire pipeline
- world generation and active play use the same backbone