open System
open LaserCourier

let private parseStartLevel (args: string array) =
    let rec loop index =
        if index >= args.Length then
            0
        elif args.[index] = "--level" && index + 1 < args.Length then
            match Int32.TryParse args.[index + 1] with
            | true, level when level >= 1 && level <= Levels.levelCount -> level - 1
            | _ -> 0
        else
            loop (index + 1)

    loop 0

let private counted noun count =
    let suffix = if count = 1 then "" else "s"
    sprintf "%d %s%s" count noun suffix

let rec private gameLoop totalMoves totalRotations state =
    Renderer.prompt ()
    let input = Console.ReadLine()

    if isNull input then
        Renderer.renderMessage "Input closed. Goodbye."
        0
    else
        match Game.parseCommand input with
        | None ->
            Renderer.renderMessage "Invalid input. Use W, A, S, D, E, R, or Q."
            Renderer.render state
            gameLoop totalMoves totalRotations state
        | Some Quit ->
            Renderer.renderMessage "Goodbye."
            0
        | Some Reset ->
            let resetState = Game.resetLevel state
            Renderer.renderMessage "Level reset."
            Renderer.render resetState
            gameLoop totalMoves totalRotations resetState
        | Some Rotate ->
            let nextState, message = Game.rotateMirror state
            message |> Option.iter Renderer.renderMessage
            Renderer.render nextState
            continueAfterUpdate totalMoves totalRotations nextState
        | Some(Move direction) ->
            let nextState, message = Game.tryMove state direction
            message |> Option.iter Renderer.renderMessage
            Renderer.render nextState
            continueAfterUpdate totalMoves totalRotations nextState

and private continueAfterUpdate totalMoves totalRotations state =
    if Beam.allTargetsEnergized state then
        let updatedTotalMoves = totalMoves + state.Moves
        let updatedTotalRotations = totalRotations + state.Rotations

        Renderer.renderMessage (
            sprintf
                "Level %d - %s cleared in %s and %s."
                (state.LevelIndex + 1)
                (Levels.levelName state.LevelIndex)
                (counted "move" state.Moves)
                (counted "rotation" state.Rotations)
        )

        if state.LevelIndex + 1 = Levels.levelCount then
            Renderer.renderMessage "All signals delivered. You win!"
            Renderer.renderMessage (sprintf "Total moves: %d" updatedTotalMoves)
            Renderer.renderMessage (sprintf "Total rotations: %d" updatedTotalRotations)
            0
        else
            let nextLevel = Levels.loadLevel (state.LevelIndex + 1)
            Renderer.render nextLevel
            gameLoop updatedTotalMoves updatedTotalRotations nextLevel
    else
        gameLoop totalMoves totalRotations state

[<EntryPoint>]
let main args =
    Renderer.renderTitle ()
    let initialState = Levels.loadLevel (parseStartLevel args)
    Renderer.render initialState
    gameLoop 0 0 initialState
