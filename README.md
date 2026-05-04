# Puzzle Courier

Puzzle Courier is a command-line grid puzzle game written in F# for .NET 10. The player controls a courier in a small warehouse and pushes boxes onto goal tiles.

## Requirements

- .NET 10 SDK

Check your SDK version with:

```bash
dotnet --version
```

## How to Run

From the repository root:

```bash
dotnet run
```

On Windows, you may also run:

```bat
run.bat
```

On macOS or Linux:

```bash
chmod +x run.sh
./run.sh
```

## How to Build

```bash
dotnet build
```

## Controls

| Input | Action |
| --- | --- |
| `W` or `w` | Move up |
| `A` or `a` | Move left |
| `S` or `s` | Move down |
| `D` or `d` | Move right |
| `R` or `r` | Reset the current level |
| `Q` or `q` | Quit immediately |

## Symbols

| Symbol | Meaning |
| --- | --- |
| `#` | Wall |
| `.` | Empty floor |
| `P` | Player |
| `B` | Box |
| `G` | Goal tile |
| `*` | Box on a goal tile |
| `+` | Player on a goal tile |

## Game Rules

The game contains exactly five fixed levels. A level is cleared when every box is on a goal tile. The move counter increases only when the player's position changes. Invalid input, wall collisions, and blocked box pushes do not increase the move counter.

If the player moves into a box, the box is pushed only when the cell behind it is empty floor or a goal tile. Boxes cannot be pushed into walls or other boxes.

## Example Level 1 Solution

Level 1 can be solved with:

```text
D
D
```

## Full Game Solution For Review

The following input sequence clears all five levels:

```text
D
D
D
D
D
S
S
S
D
D
D
D
W
W
W
A
A
A
S
D
D
D
S
S
D
D
D
S
S
D
D
D
A
A
S
D
D
A
A
S
D
D
```

## Manual Testing Checklist

Use these short scenarios to verify the proposal requirements.

| Scenario | Input sequence | Expected result |
| --- | --- | --- |
| Invalid input | `x` | The game prints an invalid input message, the board stays the same, and `Moves` remains `0`. |
| Wall collision | `W` | The player does not move and `Moves` remains `0`. |
| Box blocked by wall | `S D D W` from Level 1 start | The box is not pushed into the top wall and the final blocked move does not increase `Moves`. |
| Reset | `D R` from Level 1 start | Level 1 returns to its initial board and `Moves` becomes `0`. |
| Quit | `Q` | The game prints `Goodbye.` and exits. |
| Box blocked by another box | Clear Level 1 with `D D`, clear Level 2 with `D D D S`, then enter `D` in Level 3 | The game prints that the box cannot be pushed into another box, and `Moves` remains `0`. |
| Player on goal | On Level 3, enter `S D` | The player is displayed as `+`. |

## Project Structure

```text
PuzzleCourier.fsproj
README.md
requirements.md
run.bat
run.sh
PuzzleCourier/
  Types.fs
  Levels.fs
  Game.fs
  Renderer.fs
Program.fs
```

## Module Overview

| Module | Responsibility |
| --- | --- |
| `Types` | Core types for positions, directions, tiles, commands, and game state |
| `Levels` | Fixed level layouts, level parsing, and level loading |
| `Game` | Command parsing, movement rules, box pushing, reset, and clear checks |
| `Renderer` | Terminal rendering for boards, messages, title, and input prompt |
| `Program` | Main game loop, level transitions, quit handling, and final victory flow |

## Requirement Changes

No requirement changes after proposal submission.

The implemented levels are fixed and solvable. The exact level layouts are implementation details and remain within the proposal requirement that the game contains exactly five fixed levels.

## LLM Usage

An LLM was used to compare project ideas, draft the requirements document, plan the implementation structure, and assist with F# implementation. The requirements, level layouts, code, and README were manually reviewed.

Manual changes were needed to keep the project scope small, simplify the later levels so reviewers can clear the game quickly, and fix F# array indexing syntax during build verification. The main point the LLM could not do correctly without manual checking was guaranteeing that all proposed puzzle levels were solvable and appropriate for review.
