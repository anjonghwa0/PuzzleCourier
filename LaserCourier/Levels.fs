namespace LaserCourier

module Levels =
    let private names =
        [| "First Reflection"
           "Upper Route"
           "Two Targets"
           "Double Turn"
           "Vertical Lift"
           "Final Relay" |]

    let private rawLevels =
        [| [| "########"
              "#@...T.#"
              "#......#"
              "#>...\\.#"
              "########" |]
           [| "##########"
              "#@.......#"
              "#.../...T#"
              "#........#"
              "#>..\\....#"
              "##########" |]
           [| "###########"
              "#@........#"
              "#.T..\\....#"
              "#....T....#"
              "#>...\\....#"
              "#.........#"
              "###########" |]
           [| "############"
              "#@.........#"
              "#...\\.....T#"
              "#..........#"
              "#>..\\......#"
              "############" |]
           [| "############"
              "#@.........#"
              "#..........#"
              "#...\\..T...#"
              "#...T......#"
              "#..........#"
              "#...^......#"
              "############" |]
           [| "#############"
              "#@..........#"
              "#....\\..T./.#"
              "#...........#"
              "#....T....T.#"
              "#>...\\......#"
              "#...........#"
              "#############" |] |]

    let levelCount = rawLevels.Length

    let levelName levelIndex =
        if levelIndex < 0 || levelIndex >= levelCount then
            failwithf "Level %d does not exist." (levelIndex + 1)

        names.[levelIndex]

    let private parseLevel levelIndex (rows: string array) =
        if rows.Length = 0 then
            failwith "A level must contain at least one row."

        let width = rows.[0].Length

        if rows |> Array.exists (fun row -> row.Length <> width) then
            failwithf "Level %d is not rectangular." (levelIndex + 1)

        let mutable cursor = None
        let mutable source = None
        let mutable mirrors = Map.empty
        let mutable targets = Set.empty

        let tiles =
            rows
            |> Array.mapi (fun rowIndex row ->
                row.ToCharArray()
                |> Array.mapi (fun colIndex symbol ->
                    let position = { Row = rowIndex; Col = colIndex }

                    match symbol with
                    | '#' -> Wall
                    | '.' -> Floor
                    | '@' ->
                        cursor <- Some position
                        Floor
                    | '/' ->
                        mirrors <- mirrors |> Map.add position Slash
                        Floor
                    | '\\' ->
                        mirrors <- mirrors |> Map.add position Backslash
                        Floor
                    | 'T' ->
                        targets <- targets |> Set.add position
                        Target
                    | '^' ->
                        source <- Some(position, Up)
                        Source Up
                    | 'v' ->
                        source <- Some(position, Down)
                        Source Down
                    | '<' ->
                        source <- Some(position, Left)
                        Source Left
                    | '>' ->
                        source <- Some(position, Right)
                        Source Right
                    | other -> failwithf "Unsupported level symbol '%c'." other))

        let cursorPosition =
            match cursor with
            | Some position -> position
            | None -> failwithf "Level %d does not contain a cursor." (levelIndex + 1)

        let sourcePosition =
            match source with
            | Some source -> source
            | None -> failwithf "Level %d does not contain a source." (levelIndex + 1)

        if targets.IsEmpty then
            failwithf "Level %d does not contain a target." (levelIndex + 1)

        { LevelIndex = levelIndex
          Cursor = cursorPosition
          Tiles = tiles
          Mirrors = mirrors
          Targets = targets
          Source = sourcePosition
          Moves = 0
          Rotations = 0 }

    let loadLevel levelIndex =
        if levelIndex < 0 || levelIndex >= levelCount then
            failwithf "Level %d does not exist." (levelIndex + 1)

        parseLevel levelIndex rawLevels.[levelIndex]
