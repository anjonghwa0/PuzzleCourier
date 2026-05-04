# Laser Courier Requirements

## Project Title

Laser Courier

## Overview

Laser Courier is a deterministic command-line puzzle game. The player controls a cursor inside a wall-bordered grid and rotates mirrors so that a laser beam reaches every target in the current level. Each level contains a fixed source, fixed targets, and movable cursor-controlled mirror rotations. The game contains exactly six fixed levels. A level is cleared when all targets are energized by the current beam path.

The game is played entirely in the terminal using text commands.

## Game Symbols

| Symbol | Meaning |
| --- | --- |
| `#` | Wall |
| `.` | Empty floor |
| `@` | Player cursor |
| `>` | Laser source facing right |
| `<` | Laser source facing left |
| `^` | Laser source facing up |
| `v` | Laser source facing down |
| `/` | Slash mirror |
| `\` | Backslash mirror |
| `T` | Target that is not energized |
| `X` | Target energized by the beam |
| `*` | Beam path through an ordinary floor cell |

## Requirements

1. When the game starts, it prints the title screen and then prints level 1 by default.
2. The game contains exactly six fixed levels.
3. The player can start from a specific level by running the program with `--level N`, where `N` is an integer from 1 to 6.
4. If `--level N` is absent or invalid, the game starts from level 1.
5. Every level is a rectangular grid.
6. Every level is bordered by wall cells.
7. Every level contains exactly one player cursor.
8. Every level contains exactly one laser source.
9. Every level contains at least one mirror.
10. Every level contains at least one target.
11. The game displays the current level number, level name, move counter, rotation counter, and energized target count whenever it renders the board.
12. The player can enter `W`, `A`, `S`, or `D` to move the cursor one cell up, left, down, or right.
13. Movement commands are case-insensitive.
14. If the destination cell is not a wall, the cursor moves to that cell.
15. If the destination cell is a wall, the cursor does not move.
16. The move counter increases by 1 only when the cursor position changes.
17. The move counter does not increase after a wall collision, invalid command, reset, rotation, or quit command.
18. The player can enter `E` to rotate the mirror under the cursor.
19. The rotate command is case-insensitive.
20. A slash mirror `/` rotates into a backslash mirror `\`.
21. A backslash mirror `\` rotates into a slash mirror `/`.
22. The rotation counter increases by 1 only when a mirror is actually rotated.
23. If the cursor is not on a mirror and the player enters `E`, no mirror changes and the rotation counter does not increase.
24. The laser beam starts at the source and travels one cell at a time in the source direction.
25. The laser beam stops when it reaches a wall or leaves the board.
26. The cursor is shown as `@` on its current cell, even if the cursor is standing on a mirror, target, source, or beam path.
27. The laser beam is shown with `*` on ordinary floor cells in its path.
28. Mirror cells in the beam path are still displayed as `/` or `\`.
29. A target reached by the beam is shown as `X` unless the cursor is standing on that target.
30. A target not reached by the beam is shown as `T` unless the cursor is standing on that target.
31. When the beam enters a slash mirror `/`, it reflects as follows: up becomes right, right becomes up, down becomes left, and left becomes down.
32. When the beam enters a backslash mirror `\`, it reflects as follows: up becomes left, left becomes up, down becomes right, and right becomes down.
33. If the beam returns to a previously visited position with the same direction, the beam trace stops to prevent an infinite loop.
34. A level is cleared when every target in the level is energized by the current beam path.
35. When a level is cleared, the game prints a level clear message containing that level's move and rotation counts.
36. After levels 1 through 5 are cleared, the game automatically loads and prints the next level.
37. After level 6 is cleared, the game prints a final victory message, total move count, total rotation count, and then exits.
38. The player can enter `R` to reset the current level.
39. The reset command is case-insensitive.
40. Resetting restores the current level's original cursor position, mirror orientations, beam path, move counter, and rotation counter.
41. The player can enter `Q` to quit the game immediately.
42. The quit command is case-insensitive.
43. If the player enters any input other than `W`, `A`, `S`, `D`, `E`, `R`, or `Q`, ignoring case and surrounding whitespace, the game prints an invalid input message.
44. Invalid input does not change the board state, move counter, rotation counter, current level, or total counters.

## Example Interaction

The game starts by printing the title screen and level 1.

```text
========================================
            LASER COURIER
========================================
Rotate mirrors to deliver the beam.

Level 1 - First Reflection
Moves: 0  Rotations: 0
Targets: 0/1 energized

########
#@...T.#
#......#
#>***\.#
########

Command (W/A/S/D move, E rotate, R reset, Q quit):
```

If the player moves the cursor onto the mirror and enters `E`, the mirror rotates. If the new beam path reaches the target, the target is displayed as `X`.

```text
Level 1 - First Reflection
Moves: 6  Rotations: 1
Targets: 1/1 energized

########
#....X.#
#....*.#
#>***@.#
########
```

The game then prints a level clear message and loads the next level. After level 6 is cleared, the game prints the final total move and rotation counts before terminating.
