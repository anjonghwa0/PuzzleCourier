# Puzzle Courier Requirements

## Project Title

Puzzle Courier

## Overview

Puzzle Courier is a command-line grid puzzle game. The player controls a courier in a small warehouse and pushes boxes onto goal tiles. The game contains three fixed levels. The player clears a level by placing every box on a goal tile. After the third level is cleared, the game ends with a final victory message.

The game is played entirely in the terminal and is controlled by keyboard text input.

## Game Symbols

The game board is printed as a rectangular grid using the following symbols.

| Symbol | Meaning |
| --- | --- |
| `#` | Wall |
| `.` | Empty floor |
| `P` | Player |
| `B` | Box |
| `G` | Goal tile |
| `*` | Box on a goal tile |
| `+` | Player on a goal tile |

## Requirements

1. When the game starts, the first level is printed in the terminal.
2. The game contains exactly three fixed levels.
3. Each level is a rectangular grid containing walls, empty floor cells, one player, one or more boxes, and one or more goal tiles.
4. The number of boxes in a level is equal to the number of goal tiles in that level.
5. The player can enter `W`, `A`, `S`, or `D` to move one cell up, left, down, or right.
6. The game accepts both uppercase and lowercase movement commands.
7. If the player tries to move into an empty floor cell, the player moves to that cell.
8. If the player tries to move into a goal tile, the player moves to that cell and is displayed as `+`.
9. If the player tries to move into a wall, the player does not move.
10. If the player tries to move into a box, the game checks the cell immediately behind the box in the same direction.
11. If the cell behind the box is an empty floor cell, the box moves one cell in that direction and the player moves into the box's previous cell.
12. If the cell behind the box is a goal tile, the box moves onto the goal tile and is displayed as `*`; the player moves into the box's previous cell.
13. If the cell behind the box is a wall, another box, or outside the board, neither the player nor the box moves.
14. The game displays the current level number and the current level's move counter after each board update.
15. The move counter increases by 1 only when the player's position changes.
16. The move counter does not increase after an invalid command, a wall collision, or a blocked box push.
17. A level is cleared when every box in the level is on a goal tile.
18. When a level is cleared, the game prints a level clear message.
19. After level 1 or level 2 is cleared, the game loads and prints the next level.
20. After level 3 is cleared, the game prints a final victory message and terminates.
21. The player can enter `R` or `r` to reset the current level to its initial state.
22. After a reset, the current level's move counter is set to 0.
23. The player can enter `Q` or `q` to quit the game immediately.
24. If the player enters any input other than `W`, `A`, `S`, `D`, `R`, or `Q`, ignoring case, the game prints an invalid input message.
25. Invalid input does not change the board state and does not increase the move counter.

## Example Interaction

The game starts by printing level 1 and prompting the player for a command.

```text
=== Puzzle Courier ===
Level 1
Moves: 0

#######
#P.BG.#
#.....#
#######

Command (W/A/S/D, R, Q):
```

If the player enters `D` and the cell to the right is empty, the player moves right and the move counter increases by 1.

```text
Level 1
Moves: 1

#######
#.PBG.#
#.....#
#######
```

If the player enters `D` again, the player pushes the box one cell to the right, and the move counter increases by 1.

```text
Level 1
Moves: 2

#######
#..P*.#
#.....#
#######
```

If all boxes in the level are on goal tiles, the game prints a level clear message and loads the next level. After the third level is cleared, the game prints a final victory message and exits.
