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

The game contains exactly three fixed levels. A level is cleared when every box is on a goal tile. The move counter increases only when the player's position changes. Invalid input, wall collisions, and blocked box pushes do not increase the move counter.

If the player moves into a box, the box is pushed only when the cell behind it is empty floor or a goal tile. Boxes cannot be pushed into walls, other boxes, or outside the board.

## Example Level 1 Solution

Level 1 can be solved with:

```text
D
D
```

## Full Game Solution For Review

The following input sequence clears all three levels:

```text
D
D
D
D
D
S
S
D
D
S
D
D
```

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

## Requirement Changes

No requirement changes after proposal submission.

The implemented levels are fixed and solvable. The exact level layouts are implementation details and remain within the proposal requirement that the game contains exactly three fixed levels.

## LLM Usage

An LLM was used to compare project ideas, draft the requirements document, plan the implementation structure, and assist with F# implementation. The requirements, level layouts, code, and README were manually reviewed.

Manual changes were needed to keep the project scope small, simplify the later levels so reviewers can clear the game quickly, and fix F# array indexing syntax during build verification. The main point the LLM could not do correctly without manual checking was guaranteeing that all proposed puzzle levels were solvable and appropriate for review.
