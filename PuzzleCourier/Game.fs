namespace PuzzleCourier

module Game =
    let parseCommand (input: string) =
        match input.Trim().ToUpperInvariant() with
        | "W" -> Some(Move Up)
        | "A" -> Some(Move Left)
        | "S" -> Some(Move Down)
        | "D" -> Some(Move Right)
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

    let private movePlayer state position =
        { state with
            Player = position
            Moves = state.Moves + 1 }

    let private tryPushBox state boxPosition direction =
        let offset = Position.delta direction
        let target = Position.add boxPosition offset

        match tileAt state target with
        | None -> state, Some "Blocked: the box cannot be pushed outside the board."
        | Some Wall -> state, Some "Blocked: the box cannot be pushed into a wall."
        | Some (Floor | Goal) when state.Boxes.Contains target ->
            state, Some "Blocked: the box cannot be pushed into another box."
        | Some(Floor | Goal) ->
            let updatedBoxes =
                state.Boxes
                |> Set.remove boxPosition
                |> Set.add target

            { state with
                Player = boxPosition
                Boxes = updatedBoxes
                Moves = state.Moves + 1 },
            None

    let tryMove state direction =
        let offset = Position.delta direction
        let target = Position.add state.Player offset

        match tileAt state target with
        | None -> state, Some "Blocked: you cannot move outside the board."
        | Some Wall -> state, Some "Blocked: wall."
        | Some(Floor | Goal) when state.Boxes.Contains target -> tryPushBox state target direction
        | Some(Floor | Goal) -> movePlayer state target, None

    let isLevelCleared state =
        state.Boxes |> Set.forall (fun box -> state.Goals.Contains box)

    let resetLevel state = Levels.loadLevel state.LevelIndex
