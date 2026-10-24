# GitHub Copilot Instructions for Luciferium Rain Mod

## Mod Overview and Purpose

Luciferium Rain is a RimWorld mod that introduces a new world condition called "Luciferium rain." The mod updates the original AgentBlacs mod and adds a new scenario where glitterworld nannites are transported by a spacer vessel and are set upon by an unknown entity. The nannites are pulled into the planet's atmosphere and activate upon entry, targeting living things as potential hosts.

## Key Features and Systems

- **Luciferium Rain World Condition:** A new world condition that affects living things outside when the event begins. It targets all living things, including humans, animals, and insects, as potential hosts for the nannites. The event is extremely rare, occurring a little over a year into the game and lasting only a couple of minutes at most.

- **Nanite Activation:** The nannites are dormant until they enter the planet's atmosphere and are activated upon entry. Once activated, they target living things as potential hosts.

- **XML Integration:** The mod includes several XML files that define the new world condition, hediff, incident, and stat. These files are used to integrate the new features into the game.

- **Harmony Patching:** The mod uses Harmony patching to modify the game's behavior. It patches the `GameCondition` class to add the new `LuciferiumRain` condition and the `Pawn` class to add the new `LuciferiumHigh` hediff.

## Coding Patterns and Conventions

- **Namespaces:** All C# files are part of the `LuciferiumRain` namespace.

- **Classes:** All C# files define one or more classes that represent the mod's features. For example, the `GameCondition_LuciferiumRain` class represents the new world condition.

- **Methods:** All methods in the C# files are public, with the exception of the constructor and any private helper methods.

- **Constants:** All constants used in the C# files are defined in their respective classes.

## XML Integration

The mod includes several XML files that define the new world condition, hediff, incident, and stat. These files are located in the `Source/TranslationTemplate` directory.

- **GameConditions_Misc.xml:** Defines the new `LuciferiumRain` world condition.

- **Hediffs_Global_Misc.xml:** Defines the new `LuciferiumHigh` hediff.

- **Incidents_Map_Misc.xml:** Defines the new `LuciferiumRain` incident.

- **Stats_Pawns_General.xml:** Defines the new `LuciSensitivity` stat.

## Harmony Patching

The mod uses Harmony patching to modify the game's behavior. The `HarmonyPatch` attribute is used to specify the target method and the patch method. The `Patch` class defines the patch method, which is called when the target method is executed.

- **GameCondition_LuciferiumRain:** Patches the `GameCondition` class to add the new `LuciferiumRain` condition.

- **Pawn:** Patches the `Pawn` class to add the new `LuciferiumHigh` hediff.

## Suggestions for Copilot

- **World Condition:** Use the `GameCondition` class as a reference when creating a new world condition.

- **Hediff:** Use the `Hediff` class as a reference when creating a new hediff.

- **Incident:** Use the `Incident` class as a reference when creating a new incident.

- **Stat:** Use the `Stat` class as a reference when creating a new stat.

- **Harmony Patching:** Use the `HarmonyPatch` attribute and the `Patch` class as a reference when creating a new Harmony patch.

- **XML Integration:** Use the XML files as a reference when creating new XML definitions.

- **C# Naming Conventions:** Use the C# naming conventions when writing new code.

- **Mod Testing:** Test the mod thoroughly before releasing it to ensure that it works as expected.

- **Mod Documentation:** Document the mod's features and how to use them.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

