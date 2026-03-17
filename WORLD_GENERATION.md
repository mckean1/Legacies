# WORLD_GENERATION.md

## Purpose

This document defines the role of World Generation in Legacies.

World Generation is not a separate fake simulation.
It is a bounded run of the real SimulationEngine until the world reaches a truthful player-start condition.

---

## Core Rule

World Generation and Active Play run on the same simulation backbone.

World Generation exists to:
- create the initial world state
- let life and societies emerge through simulation
- evaluate when viable player starts exist
- hand the player a real start drawn from current world truth

It must not invent a polished world by bypassing causality.

---

## High-Level Flow

### 1. Seed World Initialization
Create the initial raw world.

Examples:
- geography
- climate
- regional support baselines
- primitive life / early species starting conditions

### 2. Run Monthly Simulation
Advance the world month by month through the SimulationEngine.

### 3. Observe World State
Derived observer/evaluator layers inspect the world without rewriting it.

Examples:
- candidate viability
- readiness state
- world diversity
- world structure maturity

### 4. Determine Stop Condition
When the world reaches a legitimate player-start condition, stop world generation.

### 5. Present Start Candidates
Show the player viable candidate starts drawn from real world state.

### 6. Handoff to Active Play
Continue from the same world state without simulating an extra “transition month” that rewrites reality.

---

## World Generation Goals

World Generation should produce:
- a living world with history
- real ecological and population continuity
- real social emergence
- real regional differences
- real start candidates with meaningful strengths and weaknesses

It should not aim to force all worlds into the same shape.

---

## Early Implementation Target

The first world generation implementation should be modest.

It only needs to prove:
- a world can initialize
- monthly time progression works
- populations survive, move, or fail based on support
- at least some social groups persist or emerge
- meaningful chronicle-worthy history appears
- one or more candidate societies can eventually exist

Do not overbuild evaluation before the world itself can live.

---

## Honest World Rule

World Generation must remain honest.

Do not:
- fabricate healthy starts where none exist
- disguise world weakness as success
- auto-upgrade fragile groups into stronger political forms without evidence
- hide worldgen failure behind arbitrary fallback promotions

If a world is thin, the game should report that truthfully.

---

## World Generation Outputs

At minimum, World Generation should produce:

- final world state
- world age reached
- chronicle history generated during pre-player simulation
- evaluated start candidate list
- summary of why the world stopped
- summary of weak-world/failure reasons if applicable

---

## Stop Condition Philosophy

World Generation should stop because the world is ready, not because a timer expired alone.

Possible categories later include:
- biological readiness
- social emergence readiness
- world structure readiness
- candidate readiness
- variety readiness
- agency readiness

For now, keep the first implementation simpler:
- minimum age reached
- at least one viable candidate exists
- the world is not in obvious collapse

---

## Weak World / Failure Handling

Sometimes the world may not produce good starts.

That must be handled honestly.

Potential outcomes:
- continue simulating
- stop with thin but real candidates
- fail generation if no truthful candidate exists by the maximum allowed limit

Do not paper over a broken world with fake maturity.

---

## Relationship to Chronicle

World Generation should produce meaningful inherited history.
The player should feel that the selected start comes from an already-living world.

However:
- the player should not be dumped into unreadable debug spam
- the inherited history shown later should be compact and curated

---

## Future Expansion Areas

Later versions of world generation may include:
- richer readiness categories
- candidate scoring and diversity logic
- weak-world handling states
- improved visibility during generation
- better selection presentation
- generation logs and diagnostics

These should build on truthful simulation first.