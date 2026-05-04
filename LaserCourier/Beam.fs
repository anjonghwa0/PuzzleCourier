namespace LaserCourier

module Beam =
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

    let trace state =
        let sourcePosition, sourceDirection = state.Source

        let rec loop position direction path energizedTargets visited =
            let nextPosition = Position.add position (Position.delta direction)
            let visitKey = nextPosition, direction

            if visited |> Set.contains visitKey then
                { Path = path
                  EnergizedTargets = energizedTargets }
            else
                match tileAt state nextPosition with
                | None
                | Some Wall ->
                    { Path = path
                      EnergizedTargets = energizedTargets }
                | Some(Floor | Target | Source _) ->
                    let nextPath = path |> Set.add nextPosition

                    let nextEnergizedTargets =
                        if state.Targets.Contains nextPosition then
                            energizedTargets |> Set.add nextPosition
                        else
                            energizedTargets

                    let nextDirection =
                        match state.Mirrors |> Map.tryFind nextPosition with
                        | Some mirror -> Mirror.reflect mirror direction
                        | None -> direction

                    loop
                        nextPosition
                        nextDirection
                        nextPath
                        nextEnergizedTargets
                        (visited |> Set.add visitKey)

        loop sourcePosition sourceDirection Set.empty Set.empty Set.empty

    let allTargetsEnergized state =
        let trace = trace state
        state.Targets |> Set.forall (fun target -> trace.EnergizedTargets.Contains target)
