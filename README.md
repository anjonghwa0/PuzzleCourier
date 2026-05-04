# Laser Courier

Laser Courier is a terminal puzzle game written in F# for .NET 10. Move the cursor, rotate mirrors, and route the laser beam so every target is energized.

## Requirements

- .NET 10 SDK

## How to Run

From this repository root:

```bash
dotnet run
```

Start from a specific level:

```bash
dotnet run -- --level 4
```

Helper scripts are also provided:

```bash
./run.sh
```

```bat
run.bat
```

## Controls

| Key | Action |
| --- | --- |
| `W` | Move up |
| `A` | Move left |
| `S` | Move down |
| `D` | Move right |
| `E` | Rotate the mirror under the cursor |
| `R` | Reset the current level |
| `Q` | Quit |

Commands are case-insensitive.

## Symbols

| Symbol | Meaning |
| --- | --- |
| `#` | Wall |
| `.` | Empty floor |
| `@` | Player cursor |
| `>`, `<`, `^`, `v` | Laser source direction |
| `/`, `\` | Mirrors |
| `T` | Target not energized |
| `X` | Energized target |
| `*` | Beam path |

## Objective

Each level has one laser source and one or more targets. The laser travels in a straight line until it hits a wall or mirror. Mirrors reflect the beam. Clear a level by rotating mirrors until every target is energized.

The game includes six fixed levels, per-level move and rotation counters, total counters after victory, reset support, and level selection with `--level N`.

## Project Structure

| Path | Purpose |
| --- | --- |
| `LaserCourier.fsproj` | F# .NET 10 project file |
| `Program.fs` | Entry point, argument parsing, and main game loop |
| `LaserCourier/Types.fs` | Core game types |
| `LaserCourier/Levels.fs` | Fixed level definitions and level parser |
| `LaserCourier/Beam.fs` | Beam tracing and target energizing logic |
| `LaserCourier/Game.fs` | Player commands, movement, mirror rotation, reset |
| `LaserCourier/Renderer.fs` | Terminal rendering |
| `requirements.md` | Source requirements document |
| `LaserCourier_Requirements.pdf` | PDF requirements document |

## Requirement Changes

This repository originally explored an earlier `Puzzle Courier` box-pushing idea. Before finalizing the revised proposal, the project was changed to `Laser Courier` to make the core mechanic more distinctive while keeping the implementation deterministic and testable.

For the revised submission, `requirements.md` and `LaserCourier_Requirements.pdf` are the authoritative requirements. The current implementation follows those requirements.

## LLM Usage

An LLM was used to brainstorm project ideas, compare scope risks, draft requirements, implement the F# code, and review the project critically. The project was manually tested and revised through repeated review. The main parts that required manual correction were level design, keeping the requirements concrete enough for grading, and avoiding unnecessary scope that would make final implementation risky.
