namespace PuzzleCourier

open System

module Renderer =
    let private characterAt state position =
        if state.Player = position then
            if state.Goals.Contains position then '+' else 'P'
        elif state.Boxes.Contains position then
            if state.Goals.Contains position then '*' else 'B'
        else
            match state.Tiles.[position.Row].[position.Col] with
            | Wall -> '#'
            | Floor -> '.'
            | Goal -> 'G'

    let render state =
        printfn "Level %d - %s" (state.LevelIndex + 1) (Levels.levelName state.LevelIndex)
        printfn "Moves: %d" state.Moves
        printfn ""

        for row in 0 .. state.Tiles.Length - 1 do
            let cells =
                [| for col in 0 .. state.Tiles.[row].Length - 1 ->
                       characterAt state { Row = row; Col = col } |]

            printfn "%s" (String cells)

        printfn ""

    let renderTitle () =
        printfn "========================================"
        printfn "           PUZZLE COURIER"
        printfn "========================================"
        printfn "Deliver every box to a goal tile."
        printfn ""

    let renderMessage message =
        printfn "%s" message
        printfn ""

    let prompt () =
        printf "Command (W/A/S/D, R, Q): "
