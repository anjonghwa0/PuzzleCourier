namespace PuzzleCourier

module Levels =
    let private rawLevels =
        [| [| "#######"
              "#P.BG.#"
              "#.....#"
              "#######" |]
           [| "########"
              "#P.....#"
              "#.##B..#"
              "#...G..#"
              "########" |]
           [| "#########"
              "#P......#"
              "#.B.G...#"
              "#...B.G.#"
              "#.......#"
              "#########" |] |]

    let levelCount = rawLevels.Length

    let private parseLevel levelIndex (rows: string array) =
        if rows.Length = 0 then
            failwith "A level must contain at least one row."

        let width = rows.[0].Length

        if rows |> Array.exists (fun row -> row.Length <> width) then
            failwithf "Level %d is not rectangular." (levelIndex + 1)

        let mutable player = None
        let mutable boxes = Set.empty
        let mutable goals = Set.empty

        let tiles =
            rows
            |> Array.mapi (fun rowIndex row ->
                row.ToCharArray()
                |> Array.mapi (fun colIndex symbol ->
                    let position = { Row = rowIndex; Col = colIndex }

                    match symbol with
                    | '#' -> Wall
                    | '.' -> Floor
                    | 'P' ->
                        player <- Some position
                        Floor
                    | 'B' ->
                        boxes <- boxes |> Set.add position
                        Floor
                    | 'G' ->
                        goals <- goals |> Set.add position
                        Goal
                    | '*' ->
                        boxes <- boxes |> Set.add position
                        goals <- goals |> Set.add position
                        Goal
                    | '+' ->
                        player <- Some position
                        goals <- goals |> Set.add position
                        Goal
                    | other -> failwithf "Unsupported level symbol '%c'." other))

        let playerPosition =
            match player with
            | Some position -> position
            | None -> failwithf "Level %d does not contain a player." (levelIndex + 1)

        if boxes.IsEmpty then
            failwithf "Level %d does not contain a box." (levelIndex + 1)

        if goals.IsEmpty then
            failwithf "Level %d does not contain a goal." (levelIndex + 1)

        if boxes.Count <> goals.Count then
            failwithf "Level %d has a different number of boxes and goals." (levelIndex + 1)

        { LevelIndex = levelIndex
          Player = playerPosition
          Boxes = boxes
          Goals = goals
          Tiles = tiles
          Moves = 0 }

    let loadLevel levelIndex =
        if levelIndex < 0 || levelIndex >= levelCount then
            failwithf "Level %d does not exist." (levelIndex + 1)

        parseLevel levelIndex rawLevels.[levelIndex]
