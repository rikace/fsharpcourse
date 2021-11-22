open System
open ParallelWebCrawler

[<EntryPoint>]
let main argv =

    let agent = new ParallelWebCrawler.WebCrawler()
    agent.Submit "https://www.google.com"

    Console.ReadLine() |> ignore
    agent.Dispose()
    0
