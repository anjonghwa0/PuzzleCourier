open System
open PuzzleCourier

let rec private gameLoop state =
    Renderer.prompt ()
    let input = Console.ReadLine()

    if isNull input then
        Renderer.renderMessage "Input closed. Goodbye."
        0
    else
        match Game.parseCommand input with
        | None ->
            Renderer.renderMessage "Invalid input. Use W, A, S, D, R, or Q."
            Renderer.render state
            gameLoop state
        | Some Quit ->
            Renderer.renderMessage "Goodbye."
            0
        | Some Reset ->
            let resetState = Game.resetLevel state
            Renderer.renderMessage "Level reset."
            Renderer.render resetState
            gameLoop resetState
        | Some(Move direction) ->
            let nextState, message = Game.tryMove state direction

            message |> Option.iter Renderer.renderMessage
            Renderer.render nextState

            if Game.isLevelCleared nextState then
                Renderer.renderMessage (sprintf "Level %d cleared!" (nextState.LevelIndex + 1))

                if nextState.LevelIndex + 1 = Levels.levelCount then
                    Renderer.renderMessage "All deliveries complete. You win!"
                    0
                else
                    let nextLevel = Levels.loadLevel (nextState.LevelIndex + 1)
                    Renderer.render nextLevel
                    gameLoop nextLevel
            else
                gameLoop nextState

[<EntryPoint>]
let main _ =
    Renderer.renderTitle ()
    let initialState = Levels.loadLevel 0
    Renderer.render initialState
    gameLoop initialState
