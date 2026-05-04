namespace PuzzleCourier

type Position =
    { Row: int
      Col: int }

type Direction =
    | Up
    | Down
    | Left
    | Right

type Tile =
    | Wall
    | Floor
    | Goal

type Command =
    | Move of Direction
    | Reset
    | Quit

type GameState =
    { LevelIndex: int
      Player: Position
      Boxes: Set<Position>
      Goals: Set<Position>
      Tiles: Tile array array
      Moves: int }

module Position =
    let delta direction =
        match direction with
        | Up -> (-1, 0)
        | Down -> (1, 0)
        | Left -> (0, -1)
        | Right -> (0, 1)

    let add position (rowDelta, colDelta) =
        { Row = position.Row + rowDelta
          Col = position.Col + colDelta }
