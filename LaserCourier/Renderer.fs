namespace LaserCourier

open System

module Renderer =
    let private tileChar tile =
        match tile with
        | Wall -> '#'
        | Floor -> '.'
        | Target -> 'T'
        | Source direction -> Direction.sourceChar direction

    let private characterAt state beamTrace position =
        if state.Cursor = position then
            '@'
        elif state.Targets.Contains position && beamTrace.EnergizedTargets.Contains position then
            'X'
        elif state.Mirrors.ContainsKey position then
            Mirror.toChar state.Mirrors.[position]
        elif beamTrace.Path.Contains position then
            '*'
        else
            tileChar state.Tiles.[position.Row].[position.Col]

    let render state =
        let beamTrace = Beam.trace state

        printfn "Level %d - %s" (state.LevelIndex + 1) (Levels.levelName state.LevelIndex)
        printfn "Moves: %d  Rotations: %d" state.Moves state.Rotations
        printfn "Targets: %d/%d energized" beamTrace.EnergizedTargets.Count state.Targets.Count
        printfn ""

        for row in 0 .. state.Tiles.Length - 1 do
            let cells =
                [| for col in 0 .. state.Tiles.[row].Length - 1 ->
                       characterAt state beamTrace { Row = row; Col = col } |]

            printfn "%s" (String cells)

        printfn ""

    let renderTitle () =
        printfn "========================================"
        printfn "            LASER COURIER"
        printfn "========================================"
        printfn "Rotate mirrors to deliver the beam."
        printfn ""

    let renderMessage message =
        printfn "%s" message
        printfn ""

    let prompt () =
        printf "Command (W/A/S/D move, E rotate, R reset, Q quit): "
