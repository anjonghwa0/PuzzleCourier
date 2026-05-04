namespace LaserCourier

module Game =
    let parseCommand (input: string) =
        match input.Trim().ToUpperInvariant() with
        | "W" -> Some(Move Up)
        | "A" -> Some(Move Left)
        | "S" -> Some(Move Down)
        | "D" -> Some(Move Right)
        | "E" -> Some Rotate
        | "R" -> Some Reset
        | "Q" -> Some Quit
        | _ -> None

    let private isOutside state position =
        position.Row < 0
        || position.Row >= state.Tiles.Length
        || position.Col < 0
        || position.Col >= state.Tiles.[position.Row].Length

    let private tileAt state position =
        if isOutside state position then
            None
        else
            Some state.Tiles.[position.Row].[position.Col]

    let tryMove state direction =
        let target = Position.add state.Cursor (Position.delta direction)

        match tileAt state target with
        | None
        | Some Wall -> state, Some "Blocked: wall."
        | Some(Floor | Target | Source _) ->
            { state with
                Cursor = target
                Moves = state.Moves + 1 },
            None

    let rotateMirror state =
        match state.Mirrors |> Map.tryFind state.Cursor with
        | None -> state, Some "There is no mirror here."
        | Some mirror ->
            { state with
                Mirrors = state.Mirrors |> Map.add state.Cursor (Mirror.rotate mirror)
                Rotations = state.Rotations + 1 },
            None

    let resetLevel state = Levels.loadLevel state.LevelIndex
