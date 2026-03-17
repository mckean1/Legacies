# CHRONICLE.md

## Purpose

This document defines the Chronicle as the main player-facing output of Legacies.

The Chronicle is how the player experiences the ongoing history of their chosen society/polity and the surrounding world.

---

## Core Chronicle Philosophy

The Chronicle should feel like history unfolding, not debug text scrolling by.

It should be:
- readable
- causal
- selective
- evocative
- grounded in actual simulation state

The Chronicle is the storytelling surface of the simulation.

---

## Core Rules

### 1. Chronicle Only Meaningful Change
Not every state update deserves a player-facing message.

Only chronicle things that matter to the player’s understanding of history.

### 2. No Initialization Noise
If something merely exists because the world was initialized, it should not be chronicled as if it happened.

Only post-initialization change should be chronicled.

### 3. No Debug Dumps
Avoid raw internal metrics in normal player-facing messages.

That includes raw pressure telemetry. Chronicle output should usually surface the consequences of pressure-driven change, not the underlying numbers themselves.

### 4. No Redundant Spam
Repeated low-value messages destroy readability.

Chronicle systems should suppress duplicates and near-duplicates where appropriate.

### 5. Keep Cause-and-Effect Intact
Messages should reflect real simulation outcomes, not decorative fiction detached from world truth.

---

## What the Chronicle Should Surface

Examples of chronicle-worthy content:

- migrations or relocations that matter
- settlement foundings or abandonments
- society formation or fragmentation
- discovery of major new practices/capabilities
- important contact with neighbors
- trade openings
- wars, raids, defeats, alliances
- ecological hardship or recovery
- rise or decline of the player’s society/polity
- major turning points in continuity, security, or opportunity

---

## What the Chronicle Should Usually Avoid

Examples of low-value or non-player-facing content:

- raw monthly support values
- repetitive “still exists” notices
- internal evaluator-only statuses
- frequent tiny oscillations with no visible consequence
- setup-state messages caused only by initialization

---

## Structured Event Pipeline

Prefer a layered approach:

1. upstream systems establish or modify pressures
2. later systems detect meaningful outcomes caused by that updated pressure state
3. systems emit structured event candidates
4. chronicle rules decide what is worthy of surfacing
5. final player-facing text is rendered

This avoids mixing simulation truth with direct console string construction too early.

---

## Scope of Chronicle Perspective

The Chronicle should primarily reflect the player’s chosen lens.

That means it should emphasize:
- the chosen group’s internal history
- nearby threats/opportunities
- important known external events

It should not read like omniscient global narration all the time.

---

## Tone

Desired tone:
- historical
- readable
- grounded
- concise
- serious without being sterile

Avoid:
- overly modern slang
- mechanical debug phrasing
- exaggerated fantasy prose disconnected from the sim

---

## Early Chronicle Categories

A good initial set of event categories might include:

- Migration
- Settlement
- Scarcity
- Recovery
- SocietyFormed
- SocietyFragmented
- Discovery
- Contact
- Conflict
- PoliticalShift

This list can expand later.

---

## Chronicle Quality Checks

When evaluating Chronicle output, ask:

- Does this message reflect a real change?
- Would a player care?
- Is it too repetitive?
- Is it understandable without internal code knowledge?
- Does it help the player understand the unfolding story?

If not, it likely should not appear.

---

## Inherited History for Active Play

When the player selects a start after world generation, the game should preserve that the world already has history.

However, the handoff should use:
- a curated prehistory summary
- important inherited events
- not the entire raw generation log

The player should feel continuity without being overwhelmed.

---

## Long-Term Goal

A strong Chronicle should eventually make Legacies playable and interesting even when the player is mostly observing.

That means the Chronicle is not a cosmetic extra.
It is one of the game’s core systems.