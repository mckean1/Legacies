# ARCHITECTURE.md

## Purpose

This document defines the high-level architecture for Legacies.

Legacies is a simulation-first command-line game built around a shared SimulationEngine that powers both World Generation and Active Play.

---

## Core Principles

### Simulation First
The world exists as an evolving simulation, not as a set of scripted game states.

### Shared Engine
World Generation and Active Play must operate on the same core simulation rules.

### Deterministic Foundations
Given the same seed and the same inputs, the simulation should produce the same results.

### Explainable State
Major events and outcomes should be traceable to prior simulation conditions.

### Pressure-First Causality
The intended architecture is pressure-first as defined in `PRESSURES.md`: systems should generally modify entity-owned pressures first, and later systems should react to that updated pressure state. Current code only implements the early regional and population foundation of that model.

### Layered Responsibility
The domain should own world truth. The client should own presentation and interaction.

---

## High-Level Architecture

### 1. Domain Layer
The Domain layer contains the simulation truth.

Responsibilities:
- world state
- time progression
- geography/regions
- ecology and support
- species and populations
- social formation
- settlements and routes
- polity relationships
- discoveries and advancements
- chronicle event generation
- world generation evaluation and selection logic

This layer should remain usable without a console UI.

### 2. Client Layer
The Client layer presents the world to the player.

Responsibilities:
- starting the simulation
- choosing seeds/configuration
- running world generation
- showing progress/state to the player
- presenting chronicle output
- showing world/polity/species/region views
- accepting player input during active play

The client should not become the source of world truth.

---

## Core Runtime Model

At a high level, Legacies runs like this:

1. Create or load a world.
2. Run the SimulationEngine month by month.
3. Systems update the world in a stable universal order, primarily by establishing and reacting to pressures.
4. State changes produce world consequences and chronicleable outcomes.
5. During world generation, readiness/evaluation logic determines when the world can hand off to player selection.
6. The player selects a real generated start.
7. Active play continues on the same world state without re-authoring reality.

---

## Major Conceptual Subsystems

### SimulationEngine
The canonical monthly simulation runner.

Responsibilities:
- store explicitly registered broad systems
- sort systems by canonical phase and stable registration order
- advance time through the `CalendarSystem`
- execute the monthly pipeline in universal order
- return a `SimulationStepResult` for every tick
- maintain deterministic sequencing
- expose structured execution output for logging and Chronicle rendering

### World Model
The persistent simulation state.

Includes:
- calendar/time
- regions
- species
- populations
- societies/polities
- settlements/routes
- environmental support conditions
- discoveries/advancements
- structured chronicle history

### World Generation
World Generation is not a different game mode with fake rules.
It is a bounded period of running the same simulation until the world is ready for player start selection.

### Chronicle Pipeline
The Chronicle converts meaningful world outcomes into player-facing historical messages.

### Active Play Layer
After the player selects a start, the game continues from the true current world state.
Active play adds player-facing control and views without replacing the simulation foundation.

---

## Initial Package / Project Direction

### `Legacies.Domain`
Owns:
- domain entities
- value objects
- simulation systems
- world generation
- chronicle/event modeling

### `Legacies.Client`
Owns:
- application entrypoint
- command-line presentation
- rendering
- keypress/input flow
- player-facing run loop

---

## Recommended Early Namespace Direction

Example starter structure:

- `Legacies.Domain.World`
- `Legacies.Domain.Time`
- `Legacies.Domain.Simulation`
- `Legacies.Domain.Geography`
- `Legacies.Domain.Ecology`
- `Legacies.Domain.Species`
- `Legacies.Domain.Populations`
- `Legacies.Domain.Societies`
- `Legacies.Domain.Settlements`
- `Legacies.Domain.Discovery`
- `Legacies.Domain.Chronicle`
- `Legacies.Domain.WorldGeneration`

This can evolve, but the separation of concerns should remain.

---

## Early Vertical Slice Goal

The first real vertical slice should prove:

- a world can be generated
- time advances monthly
- ecological/support conditions affect populations
- populations move or stabilize
- at least one society can emerge
- chronicle output explains what happened

Everything beyond that should build on this slice, not replace it.

---

## Architectural Guardrails

### Avoid Client-Owned Simulation Logic
Do not place world rules in the console layer.

### Keep Pacing in the Client
The domain should not contain `Thread.Sleep` or presentation pacing.
If a demo run wants delay, the client owns it.

### Avoid Duplicate Rule Paths
Do not create separate “worldgen-only truth” and “active-play truth” unless absolutely necessary and documented.

### Avoid Debug-Driven Player Output
Player-facing text should be authored as game output, not raw debug state dumps.

### Prefer Honest Incompleteness
A smaller truthful system is better than a larger fake one.

---

## Future Expansion Areas

These are expected later, but should build on the same architecture:

- trade networks
- diplomacy
- warfare
- religion/mythology
- traditions/culture
- knowledge propagation
- internal governance
- player directives
- map and visibility systems
- save/load and long-run persistence

---

## Architecture Success Criteria

The architecture is succeeding when:

- systems are easy to reason about
- the monthly pipeline order is obvious from the engine and system metadata
- state changes are explainable
- world generation and active play feel like the same world
- new systems slot into the monthly loop cleanly
- the Chronicle remains coherent and meaningful