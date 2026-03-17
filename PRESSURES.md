# PRESSURES.md

## Purpose

This document is the canonical design guidance for the pressure model in Legacies.

It defines the intended semantics, ownership, propagation, and long-term direction of pressure-first simulation.

Current implementation is still partial. The repository has early regional, population, and movement pressure-like state in code, but it does not yet implement the full pressure framework described here across all systems.

Read this document as canonical design direction for how the simulation should work, not as a claim that every pressure behavior below is already fully implemented.

Pressures are the primary causal substrate of the simulation.

Legacies should not make important simulation decisions through isolated one-off rules when those decisions can instead emerge from pressure state. Systems and entities should read, update, accumulate, relieve, and react to pressures over time.

This preserves:
- cause-and-effect
- explainability
- determinism
- simulation consistency across world generation and active play

---

## Core Philosophy

### Pressure-First Simulation

All major simulation decisions should be driven by pressures.

Examples:
- populations do not migrate because of a hardcoded “migration event”
- they migrate because movement pressure rises above a meaningful threshold
- a society does not fragment because a rule randomly fired
- it fragments because cohesion pressure, scarcity pressure, legitimacy pressure, and security pressure produced that outcome

The simulation should answer:
- what pressures existed
- why they existed
- which system updated them
- which later system reacted to them

---

## Core Rules

### 1. Events Modify Pressures; Pressures Drive Decisions

Events and system outcomes should generally not skip directly to downstream consequences.

Preferred pattern:
1. something happens
2. that thing modifies one or more pressures
3. later systems evaluate the new pressure state
4. entity behavior changes because of that pressure state

Example:
- drought increases ecological scarcity pressure
- scarcity increases survival pressure on populations
- survival pressure increases movement pressure and cohesion strain
- movement and social outcomes happen later because those pressures rose

This keeps causality clean.

---

### 2. Each Entity Owns Its Own Pressure State

Each major simulation entity should own the pressures relevant to its layer of existence.

Examples:
- regions own environmental and ecological pressures
- populations own survival and movement pressures
- societies own cohesion and security pressures
- settlements own site viability and abandonment pressures
- polities own structural, legitimacy, and external pressure profiles

The pressure framework should be shared, but the pressure sets should be entity-appropriate.

Do not make one giant global pressure bag for the whole world.

---

### 3. Systems Update Their Own Layer of Pressure

Each broad system should primarily write the pressure state associated with its own simulation layer.

Examples:
- `EnvironmentSystem` writes environmental pressure
- `EcologySystem` writes ecological support / scarcity pressure
- `PopulationSystem` writes survival and demographic pressure
- `MovementSystem` writes movement disposition / displacement pressure
- `SocialSystem` writes cohesion / fragmentation pressure
- `SettlementSystem` writes settlement viability pressure
- `InteractionSystem` writes competition / contact / conflict pressure
- `PoliticalSystem` writes authority / legitimacy / structural pressure

Systems may read upstream pressures, but they should mainly own their own pressure outputs.

---

### 4. Pressures Must Be Explainable

A pressure value should not just exist as an opaque number.

Pressures should be attributable to causes.

Good:
- scarcity pressure increased because regional support fell, winter severity rose, and crowding increased

Bad:
- scarcity pressure = 72 for unclear reasons

Where practical, pressures should preserve source information or source contributions.

---

### 5. Pressures Are Persistent but Mutable

Pressures should usually have continuity across months.

Most pressures should not reset to zero every tick unless that makes domain sense.

Instead, pressures may:
- accumulate
- decay
- spike
- stabilize
- be relieved
- be redirected into other pressures

This allows history to matter.

---

### 6. Pressures Are Not the Same as Events

Pressures are simulation state.

Events are meaningful outcomes or surfaced consequences.

Examples:
- pressure: growing scarcity pressure in a region
- event: a population abandons the region
- chronicle: “The Ashen Valley grew thin, and many families departed its dry interior.”

Do not confuse underlying pressure state with player-facing narration.

---

## Pressure Model Structure

## Pressure Categories

Legacies should use domain-meaningful pressure categories rather than a single generic score.

Example categories by layer:

### Regional / Environmental Pressures
- Climate Pressure
- Seasonal Severity Pressure
- Hazard Pressure
- Ecological Scarcity Pressure
- Ecological Recovery Pressure
- Crowding Pressure

### Population Pressures
- Survival Pressure
- Reproductive Pressure
- Movement Pressure
- Displacement Pressure
- Health Pressure
- Opportunity Pressure

### Society Pressures
- Cohesion Pressure
- Fragmentation Pressure
- Rootedness Pressure
- Identity Continuity Pressure
- External Threat Pressure
- Internal Strain Pressure

### Settlement Pressures
- Site Viability Pressure
- Resource Access Pressure
- Exposure Pressure
- Abandonment Pressure
- Growth Pressure

### Polity / Political Pressures
- Legitimacy Pressure
- Authority Pressure
- Stability Pressure
- Expansion Pressure
- Rival Pressure
- Administrative Strain Pressure

Not every category needs to exist immediately.
The model should grow as systems become real.

---

## Pressure Shape

A pressure should generally include:

- category/type
- current magnitude
- recent trend
- source contributions
- optional decay/recovery behavior
- optional last-updated month
- optional severity band

Possible conceptual shape:

- `PressureType`
- `Value`
- `Trend`
- `SourceContributions`
- `LastUpdatedMonth`

Keep implementation simple at first.

---

## Pressure Sources

Each pressure may have one or more source contributions.

Examples:
- scarcity pressure from:
  - low regional support
  - winter severity
  - incoming migrants
- cohesion pressure from:
  - repeated relocation
  - resource competition
  - recent conflict
- legitimacy pressure from:
  - military failure
  - repeated scarcity
  - elite fragmentation

Source-awareness is strongly preferred because it improves:
- debugging
- chronicle generation
- player explanation
- future AI decision-making
- post-tick evaluation

---

## Pressure Lifecycle

A typical pressure lifecycle may look like:

1. **Creation**  
   A system introduces or updates pressure based on current conditions.

2. **Accumulation**  
   Repeated bad conditions raise the pressure.

3. **Propagation**  
   The pressure contributes to downstream pressures in later systems.

4. **Reaction**  
   One or more systems make decisions because the pressure crossed a threshold or won against competing pressures.

5. **Relief / Decay**  
   Conditions improve, and the pressure weakens over time.

6. **Stabilization**  
   The entity settles into a new equilibrium.

This lifecycle should feel natural and causal.

---

## Decision-Making from Pressures

### General Rule

Systems should prefer evaluating pressure balances rather than isolated booleans.

Example:
A population deciding whether to move should consider:
- movement pressure
- survival pressure
- rootedness pressure
- destination opportunity pressure
- travel cost / spatial friction

The outcome should reflect the net pressure profile, not one single trigger.

---

### Pressure Thresholds

Thresholds are acceptable, but they should represent meaningful transitions.

Examples:
- low pressure: no major behavior change
- medium pressure: growing strain, limited adaptation
- high pressure: significant behavior change likely
- critical pressure: crisis behavior, collapse, or forced transition

Thresholds should be:
- domain-meaningful
- stable
- documented
- not arbitrary magic numbers scattered everywhere

---

### Competing Pressures

Entities will often experience conflicting pressures.

Example:
A population may have:
- high movement pressure due to scarcity
- high rootedness pressure due to strong home attachment
- high external threat pressure in neighboring regions

The system should resolve behavior from the balance of these pressures.

This is a major source of believable simulation behavior.

---

## Entity Pressure Ownership

## Regions

Regions are pure regions and are the canonical spatial unit of simulation.

They are not a hybrid of regions and hidden cells.

Regions should own pressures related to:
- climate
- ecological support
- hazard exposure
- crowding
- recoverability
- local opportunity

A region may have richer descriptors later, but the simulation should still think in regions as the primary spatial truth.

Movement should happen across region adjacency, not a hidden cell grid.

---

## Populations

Populations should own pressures related to:
- survival
- movement
- displacement
- reproduction
- local opportunity
- stress and recovery

Population decisions should emerge from these pressures.

---

## Societies

Societies should own pressures related to:
- cohesion
- fragmentation
- continuity
- rootedness
- external threat
- internal stress
- integration strain

Society-level decisions should not bypass these pressures.

---

## Settlements

Settlements should own pressures related to:
- viability
- sustainability
- exposure
- crowding
- abandonment
- growth potential

Settlement founding, persistence, and abandonment should be pressure-driven.

---

## Polities

Polities should own pressures related to:
- authority
- legitimacy
- stability
- expansion incentive
- external rivalry
- administrative strain

Political outcomes should arise from these pressures over time.

---

## Geography Rule

## Regions Are Pure Regions

Legacies uses regions as the core spatial simulation unit.

Do not create a hidden or hybrid cell-based simulation model that competes with the region model.

Allowed:
- region adjacency graph
- region traits
- region sub-descriptors
- region corridors
- region fertility zones as descriptors
- route links between regions

Not desired:
- a second true spatial layer that becomes the real simulation map while regions become presentation-only wrappers

If the player understands the world through regions, the simulation should primarily operate through regions as well.

---

## Pressure Flow Across the Monthly Pipeline

A pressure-first monthly flow may look like this:

1. `CalendarSystem`
   - updates time context

2. `EnvironmentSystem`
   - updates climate/season/hazard pressures on regions

3. `EcologySystem`
   - converts environmental state into support, scarcity, abundance, and crowding pressures

4. `PopulationSystem`
   - updates biological state and survival pressure proxies on populations

5. `MovementSystem`
   - updates movement/displacement/opportunity pressures and applies spatial changes across region adjacency

6. `SocialSystem`
   - updates cohesion, fragmentation, and continuity pressures based on demographic and spatial outcomes

7. `SettlementSystem`
   - updates site viability, abandonment, and anchoring pressures

8. `KnowledgeSystem`
   - updates adaptation and capability pressures/opportunities

9. `InteractionSystem`
   - updates competition, threat, contact, and conflict pressures between groups

10. `PoliticalSystem`
    - updates authority, legitimacy, and structural pressures

11. `ChronicleSystem`
    - surfaces meaningful outcomes caused by pressure-driven changes

12. `EvaluationSystem`
    - observes the settled month and derives readiness/evaluation conclusions without rewriting truth

This preserves the causal backbone.

---

## Pressure Propagation Principles

Pressures should usually flow forward through the pipeline.

Examples:
- environmental severity -> ecological scarcity
- ecological scarcity -> survival pressure
- survival pressure -> movement pressure
- movement/displacement -> cohesion strain
- cohesion strain + external pressure -> fragmentation risk
- fragmentation + low legitimacy -> political instability

Avoid skipping too many layers unless there is a very good reason.

---

## Chronicle Relationship

The Chronicle should surface meaningful outcomes of pressure-driven change.

The Chronicle should not dump raw pressure values every month.

Good Chronicle use:
- scarcity became severe enough to force migration
- repeated instability fractured a society
- favorable conditions allowed a settlement to take root

Pressures are the underlying truth.
Chronicle events are the readable consequences.

---

## Determinism

Pressure calculations and pressure-driven decisions must remain deterministic.

For the same:
- world seed
- initial state
- run configuration
- player inputs

the same pressures and same resulting decisions should occur.

Avoid:
- hidden random usage
- unstable iteration order
- pressure updates that depend on incidental collection ordering

---

## Early Implementation Guidance

Do not attempt to build the final pressure framework all at once.

Start with a small, usable foundation:

1. define a simple pressure representation
2. assign a small number of pressures to regions and populations
3. make EcologySystem update ecological pressures
4. make PopulationSystem respond to those pressures
5. make MovementSystem read movement and opportunity pressures
6. make ChronicleSystem surface the consequences

Then grow from there.

---

## Guardrails

Do:
- keep pressure semantics clear
- make pressure ownership explicit
- document pressure categories as they are added
- preserve source contributions where practical
- keep regions as the true spatial unit

Do not:
- use events as a shortcut around pressure causality
- create giant undifferentiated pressure blobs
- let every system invent incompatible pressure logic
- hide real spatial simulation in a secondary cell model
- overload the Chronicle with raw pressure telemetry

---

## Success Criteria

The pressure model is succeeding when:
- important simulation outcomes can be explained through prior pressures
- systems remain causally connected
- entities feel responsive to world conditions
- history matters because pressures persist across months
- the Chronicle reflects consequences rather than arbitrary triggers
- geography remains clear and region-based