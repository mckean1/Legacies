# DATA_MODEL.md

## Purpose

This document defines the initial conceptual data model for Legacies.

This is not meant to freeze every class immediately.
It defines the core simulation nouns and their ownership boundaries.

---

## Data Modeling Principles

### 1. World Truth Lives in the Domain
The domain owns state. The client reads and presents it.

### 2. Prefer Explicit State
Important simulation state should be represented directly, not implied through fragile side effects.

### 3. Model the World in Terms of Meaning
Use domain concepts, not generic technical buckets.

### 4. Keep Early Models Small
Start with the minimum state needed to support the first vertical slice.

### 5. Design for Deterministic Simulation
The model should support consistent iteration and explainable state changes.

---

## Core Root Objects

### World
The root simulation object.

Owns:
- current date
- regions
- species
- populations
- societies/polities
- settlements/routes
- discoveries
- global identifiers / registries if needed
- chronicle history

### WorldDate
Represents simulation time.

Current implemented fields:
- year
- month
- absolute month index
- derived season helper

---

## Geography Layer

### Region
A simulation area where ecology, populations, and societies exist.

Current Phase 1 shape:
- id
- name
- base ecological support
- base environmental pressure
- seasonal volatility
- seasonal peak month
- current environmental pressure
- current monthly support
- current support band

### RegionConnection
Represents movement/contact adjacency between regions.

Suggested purpose:
- travel difficulty
- connectivity
- route suitability

---

## Biology / Population Layer

### Species
Represents a biological lineage/type in the world.

Current Phase 1 shape:
- id
- name
- monthly growth rate
- scarcity decline rate

### PopulationGroup
Represents a concrete population presence of a species in the world.

Current Phase 1 shape:
- id
- species id
- current region id
- size
- support pressure
- health
- affiliated society id if applicable

This is likely more important early than abstract total species counts.

---

## Social Layer

### Society
Represents a persistent social identity/grouping emerging from populations.

Current Phase 1 shape:
- id
- name
- optional home region id

In early development, this can be the first controllable social/political layer.

### Polity
Represents a more structured political form when warranted by simulation truth.

Important rule:
Do not force all societies to become polities early.
A polity should emerge from actual organization, stability, and structure.

---

## Spatial Organization Layer

### Settlement
Represents an anchored site of occupation.

Current Phase 1 shape:
- id
- name
- region id
- optional owning society id

### Route
Represents a recurring movement or connection pattern.

Suggested early fields:
- id
- owning society id
- connected regions
- route stability
- seasonal usage pattern if applicable

---

## Knowledge Layer

### Discovery
Represents learned knowledge or capability.

Suggested early fields:
- id
- name
- category
- prerequisites
- effects or unlocks
- spread state

This can start simple and grow later.

---

## Chronicle Layer

### ChronicleEvent
Represents a structured, player-facing historical event candidate or final event.

Current Phase 1 shape:
- date
- event type
- importance
- message
- optional region, population, species, society, and settlement references

### SimulationStepResult
Represents one explainable monthly step.

Current Phase 1 shape:
- tick index
- start date
- end date
- executed systems in order
- structured chronicle events
- region condition changes
- population change summaries
- optional notes

Prefer structured event data over raw strings alone.

---

## World Generation / Evaluation Layer

### WorldGenerationState
Tracks high-level world generation progress.

Suggested early fields:
- phase/state
- age in months/years
- readiness flags
- candidate evaluation summary

### StartCandidate
Represents a viable player-start option discovered from real world state.

Suggested early fields:
- society/polity id
- qualification reason
- stability summary
- opportunity/risk summary
- evidence summary

---

## Early Minimum Viable Model

For the first vertical slice, the minimum likely looks like:

- `World`
- `WorldDate`
- `Region`
- `Species`
- `PopulationGroup`
- `Society`
- `Settlement`
- `ChronicleEvent`
- `SimulationEngine`
- `SimulationStepResult`

That is enough to produce a world that changes meaningfully.

---

## Modeling Warnings

Avoid these early traps:

### Giant Omniscient Entity Models
Do not create one mega-object that owns every behavior.

### Client-Only State Becoming Truth
Do not let UI rendering models become the real simulation model.

### Fake Advancement Shortcuts
Do not represent “progress” as arbitrary tier jumps disconnected from simulation evidence.

### Raw Strings Everywhere
Use typed concepts and enums/value objects where they carry real meaning.

---

## Open Questions for Later Expansion

These are intentionally deferred:
- exact species trait model
- exact ecology/resource model
- exact polity/governance data shape
- exact religion/mythology model
- exact warfare model
- exact save/load serialization strategy

Those should be added after the first vertical slice proves the backbone.