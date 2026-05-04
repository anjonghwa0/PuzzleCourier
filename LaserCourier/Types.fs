namespace LaserCourier

type Position =
    { Row: int
      Col: int }

type Direction =
    | Up
    | Down
    | Left
    | Right

type Mirror =
    | Slash
    | Backslash

type Tile =
    | Wall
    | Floor
    | Target
    | Source of Direction

type Command =
    | Move of Direction
    | Rotate
    | Reset
    | Quit

type GameState =
    { LevelIndex: int
      Cursor: Position
      Tiles: Tile array array
      Mirrors: Map<Position, Mirror>
      Targets: Set<Position>
      Source: Position * Direction
      Moves: int
      Rotations: int }

type BeamTrace =
    { Path: Set<Position>
      EnergizedTargets: Set<Position> }

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

module Mirror =
    let rotate mirror =
        match mirror with
        | Slash -> Backslash
        | Backslash -> Slash

    let reflect mirror direction =
        match mirror, direction with
        | Slash, Up -> Right
        | Slash, Right -> Up
        | Slash, Down -> Left
        | Slash, Left -> Down
        | Backslash, Up -> Left
        | Backslash, Left -> Up
        | Backslash, Down -> Right
        | Backslash, Right -> Down

    let toChar mirror =
        match mirror with
        | Slash -> '/'
        | Backslash -> '\\'

module Direction =
    let sourceChar direction =
        match direction with
        | Up -> '^'
        | Down -> 'v'
        | Left -> '<'
        | Right -> '>'
