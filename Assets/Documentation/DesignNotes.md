# Retail Turret Shooter - DesignNotes.md

## Table of Contents
1. [Game Overview](#game-overview)
2. [Core Mechanics](#core-mechanics)
3. [Narrative & Theme](#narrative--theme)
4. [Visual Style & Art Direction](#visual-style--art-direction)
5. [Audio Plan](#audio-plan)
6. [Technical Goals & Tools](#technical-goals--tools)
7. [Gameplay Loop & Progression](#gameplay-loop--progression)
8. [Scope & Constraints](#scope--constraints)
9. [References & Inspirations](#references--inspirations)
10. [Milestones & Roadmap](#milestones--roadmap)

---

## Game Overview
**High-Level Concept:**  
This is a first-person turret-based shooter set in a stylized, 3D retail store environment. The player controls a ceiling-mounted turret that rides along a rail, surveilling aisles packed with customers. The objective is to identify and swiftly eliminate “problem customers” (e.g., thieves, loud troublemakers) before they can cause chaos, while avoiding harming innocent shoppers. The tone is satirical and humorous, highlighting the absurdities of modern retail through exaggerated security measures.

**Key Pillars:**
- **Satirical Humor:** Over-the-top scenarios that parody common retail frustrations.
- **Precision & Identification:** The challenge lies not just in shooting but in correctly identifying targets among a crowd.
- **Replayability:** Randomized customer behaviors and dynamic waves ensure each session feels unique.
- **Light Progression:** Simple upgrades and wave-based difficulty scaling encourage skill improvement and strategic play.

**What Makes It Stand Out:**
- A unique twist on shooter gameplay: scanning crowds from a turret rather than free movement.
- A whimsical narrative that turns everyday retail annoyances into absurd security scenarios.
- Satisfying feedback loops involving scoring, combos, and wave progression.

---

## Core Mechanics
**Turret Movement & Controls:**
- **Rail-Based Movement:** The turret moves back and forth along a fixed ceiling rail spanning multiple aisles.  
- **360° Rotation & Tilt:** Full rotational freedom allows coverage of the entire store floor. Vertical tilt is limited to maintain a coherent field of view and prevent nausea.
- **Smooth Input Handling:** Utilizing Unity’s Input System for responsive mouse/keyboard and optional gamepad support.

**Aiming & Shooting:**
- **Single-Shot Firing:** Each shot requires precision. A brief cooldown prevents rapid spamming.
- **Hit Detection:** Raycast-based shooting. Destroying a target provides immediate feedback (score increments, sound effects).
- **Penalties for Mistakes:** Hitting innocent shoppers reduces score and can trigger panic, complicating the scene.

**Customer AI & Behavior:**
- **Randomized Roles:** Each customer is assigned a role at spawn: innocent or problem customer (stealing, hiding items, being rude).  
- **Subtle & Overt Behaviors:** Some misbehaviors are obvious (shoplifter tucking items in coat), others subtle (customer quietly stashing goods behind shelves).
- **False Positives:** Certain customers appear suspicious but do nothing wrong, testing the player’s judgment.

**Scoring & Combos:**
- **Accurate Eliminations:** Earn points for correctly identified targets. Higher-value points for more challenging identifications.
- **Combos:** Consecutive hits on correct targets without errors build a combo meter, increasing score multipliers and unlocking brief visual/sound celebrations.
- **Penalties:** Hitting innocents deducts points, breaks combos, and may cause crowds to scatter or hide, increasing difficulty.

**Upgrades & Enhancements:**
- **Turret Improvements:** Faster rail movement, reduced shooting cooldown, optional zoom lens for better identification.
- **Surveillance Aids:** Highlight suspicious customers briefly, or add a low-latency warning signal for high-risk behaviors.

---

## Narrative & Theme
**Tone & Style:**
- **Satirical Retail Hell:** Imagine a Black Friday rush every day, where customers enact minor villainies (stealing candy, yelling at staff). The turret’s role is comically extreme—security gone wild.
- **Minimal Story, Strong Context:** The game doesn’t follow a linear plot but uses environmental storytelling (announcements, store layout changes) to suggest a world on the brink of retail madness.
- **Exaggerated Characterization:** Problem customers aren’t subtle; they might tiptoe cartoonishly or break items while whistling innocently. Innocents are over-dramatic when startled, enhancing the chaotic humor.

**Narrative Hooks:**
- **Intercom Announcements:** A sarcastic store PA system commenting on the player’s performance, welcoming “valued customers” or lamenting “inventory shrinkage.”
- **Evolving Environment:** Later waves might reference previous chaos (“After yesterday’s incident in Aisle 3, we’ve reinforced shelves…”).

---

## Visual Style & Art Direction
**Artistic Goals:**
- **Stylized Realism:** A clean, low-poly yet detailed style. Think simplified models with bright colors and exaggerated proportions. Enough realism to resemble a retail store, but stylized for humor.
- **Readable Silhouettes:** Customers and props should be easily distinguishable. Problem customers might wear distinct color-coded clothing or exaggerated props (a big coat for shoplifters).
- **Store Layout & Props:** Aisles are well-organized yet cluttered with products. Posters and signs add humor (e.g., “Buy One, Panic for Free!”).

**Lighting & Color Palette:**
- **Bright, Retail-Appropriate Lighting:** Even lighting with occasional flickering lights for tension.  
- **Palette:** Soft pastels for store interiors contrasted with bright accent colors on signage and shelf labels. Subtle variation between aisles for visual interest.

**UI & Feedback:**
- **HUD Elements:** Minimalistic score counters and combo bars using TextMeshPro.  
- **Visual Feedback for Hits:** Confetti bursts for combos, a subtle red tint on the screen when hitting innocents.

---

## Audio Plan
**Music:**
- **Upbeat, Chaotic Tracks:** Light, fast-tempo background music that intensifies as the wave timer runs low.  
- **Adaptive Layers:** Additional percussion or brass sections kick in when combos rise or events occur (e.g., flash sales).

**Sound Effects:**
- **Gunshots:** Snappy, cartoonish “pew” rather than harsh realism, maintaining a humorous tone.  
- **Customer Reactions:** Muffled yelps, frustrated muttering, comedic gasps when items break.  
- **Environmental Ambience:** Subtle crowd chatter, distant checkout beeps, and PA announcements for immersion.

**Voice & PA Announcements:**
- **Sarcastic Intercom:** A witty announcer making remarks about customer behavior, teasing the player after mistakes.  
- **Distinct Cues:** Short voice lines triggered upon wave start/end, special events (flash sale, blackout).

---

## Technical Goals & Tools
**Engine & Version:**
- **Unity LTS:** Ensuring stability and access to modern features (Input System, HDRP/URP if desired).

**Key Packages & Rationale:**
- **Input System:** Flexible, modern input handling for smooth turret controls.  
- **ProBuilder:** Rapid prototyping of store layouts. Adjust aisles and shelving on the fly.  
- **TextMeshPro:** Crisp, professional UI elements.  
- **Post-Processing:** Simple effects (bloom, color grading) to enhance the stylized look without heavy asset creation.

**AI Implementation:**
- **NavMesh for Pathfinding:** Let customers navigate aisles autonomously.  
- **State Machines for Behaviors:** Simple decision trees: Idle → Move → Act (steal, hide item, etc.).

**Optimization Considerations:**
- **Object Pooling:** For spawning and despawning customers efficiently.  
- **Level of Detail (LOD):** Simple LOD or culling for distant customers to maintain frame rate.

---

## Gameplay Loop & Progression
**Wave Structure:**
1. **Start of Wave:** A short countdown before customers flood in.  
2. **Active Wave (60–90s):** Customers spawn at intervals, wander aisles, commit acts. Player scans and shoots identified targets.
3. **End of Wave:** Score tallied, upgrade menu appears (if upgrades are earned).  
4. **Next Wave:** Increased difficulty: more customers, subtler behaviors, environmental hindrances (dim lighting, special events).

**Difficulty Scaling:**
- **Increased Customer Density:** More targets, more innocents, and shorter decision-making windows.  
- **Advanced Behaviors:** Customers become better at hiding their misdeeds. Innocents mimic suspicious actions occasionally.
- **Events & Modifiers:** Flash sales (sudden rush), partial blackouts (dimming aisles), forcing reliance on upgrades.

**Player Progression:**
- **Upgrades Unlock After Certain Scores:** Players spend earned points on turret mobility ex: Rails buying to access new parts, cooldown reduction, or a brief scanning highlight.  
- **Mastery Encouraged:** High combos lead to scoring multipliers, pushing players to refine accuracy and speed.

---

## Scope & Constraints
**Timeline:**
- 25 days of development, focusing on a polished vertical slice suitable for portfolio demonstration.

**Scope Limitations:**
- **No Extensive Story Mode:** Emphasis on wave-based arcade action over narrative depth.
- **Minimal Art Assets:** Rely on stylized store packs from Asset Store and simple character models to save time.
- **AI Complexity:** Keep behaviors simple and pattern-based. No advanced machine learning or procedural generation.

**Quality vs. Quantity:**
- Focus on tight core loop, fun gameplay, and polish. Avoid feature creep.
- Ensure stable performance on mid-range PCs and WebGL builds.

---

## References & Inspirations
**Inspirations:**
- **Arcade Shooters (e.g., Time Crisis, House of the Dead):** For fast-paced shooting and wave structure.
- **Overcooked (Humor & Chaos):** Light-hearted tone, chaotic crowd behavior, and frantic pacing.
- **Papers, Please (Identification Mechanic):** Skill in distinguishing culprits from innocents under time pressure.

**Visual & Tonal Inspirations:**
- **Cartoonish Styles (Team Fortress 2, Splatoon):** Saturated colors, readable silhouettes.
- **Retail Comedy (Sitcoms like “Superstore”):** Sarcastic jabs at retail culture, exaggerated customer types.

---

## Milestones & Roadmap
**Overall Timeline:** 25 days total. Key milestones ensure consistent progress and a polished final result.

- **By Day 3:**  
  - **Deliverables:** Fully functional turret movement, basic shooting mechanic, initial Git setup and folder structure.  
  - **Verification:** Player can move turret along rail and fire shots at placeholders without errors.

- **By Day 7:**  
  - **Deliverables:** Basic customer AI, simple scoring system, and a playable test wave.  
  - **Verification:** Customers spawn, wander, player can score points by hitting correct targets, and waves end properly.

- **By Day 12 (Playable Prototype):**  
  - **Deliverables:** Core loop stable (turret, shooting, waves, scoring, AI variations), basic upgrades implemented.  
  - **Verification:** A fully playable demo where multiple waves run consecutively, upgrades can be purchased, and scoring/combos function reliably.

- **By Day 18 (Polish & Visual/Audio Integration):**  
  - **Deliverables:** Store environment fleshed out with stylized assets, audio integrated (music, SFX, voices), basic post-processing effects.  
  - **Verification:** Game looks and sounds cohesive. Customers animate, environment props react, and the aesthetic matches the intended style.

- **By Day 23 (Final Balancing & Bug Fixes):**  
  - **Deliverables:** Difficulty balanced, performance optimized, final tweaks to scoring/AI, no major known bugs.  
  - **Verification:** Smooth framerate, fair difficulty curve, stable gameplay over multiple sessions.

- **By Day 25 (Portfolio-Ready Build):**  
  - **Deliverables:** A final polished build ready for showcasing, with recorded gameplay video, screenshots, and a clear instructions screen.  
  - **Verification:** Can present the game to potential employers or audiences as a finished, polished prototype.

**Stretch Goals (If Time Permits):**
- **Extra Events:** More variety in random store events (e.g., VIP customer who must not be harmed).  
- **Thematic Customization:** Optional selectable store themes (e.g., futuristic store, medieval fantasy market parody).
- **Endless Mode:** A survival wave mode for high-score chasing.

---

# Verification & Review
**Questions to Ask:**
- Does the core mechanic (identifying and shooting targets) feel clear and fun?  
- Are the chosen tools (Input System, ProBuilder) well justified for the timeline and scope?  
- Is the humor and tone consistent across narrative, visuals, and audio?  
- Does the difficulty scaling and progression system encourage repeated play?

**Final Check:**
- The document provides a full blueprint, covering gameplay, art, audio, technical plans, and milestones.
- All sections are actionable and guide the development process.
- The roadmap clearly states expected deliverables, ensuring transparent development steps.

---

**Conclusion:**  
This DesignNotes.md file serves as a comprehensive, detailed reference for the entire retail turret shooter project. It outlines the vision, mechanics, aesthetics, technical requirements, progression systems, and development milestones, ensuring a cohesive and focused effort over the 25-day timeline. With this blueprint, the development team can proceed confidently, knowing the goals and standards expected from the final product.
