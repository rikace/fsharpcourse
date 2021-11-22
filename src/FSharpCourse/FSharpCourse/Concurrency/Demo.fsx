
module AsyncPatterns =

   open System.IO
   open System.Net

   let httpSync (url:string) =
       let req = WebRequest.Create(url)
       let resp = req.GetResponse()
       use stream = resp.GetResponseStream()
       use reader = new StreamReader(stream)
       let text = reader.ReadToEnd()
       (url, text)

   let httpAsync (url : string) = async {
       let req = WebRequest.Create(url)
       let! resp = req.AsyncGetResponse()
       use stream = resp.GetResponseStream()
       use reader = new StreamReader(stream)
       let! text = reader.ReadToEndAsync() |> Async.AwaitTask
       return (url, text)
   }

   let sites =
       [   "http://www.live.com";      "http://www.fsharp.org";
           "http://news.live.com";     "http://www.digg.com";
           "http://www.yahoo.com";     "http://www.amazon.com"
           "http://news.yahoo.com";    "http://www.microsoft.com";
           "http://www.google.com";    "http://www.netflix.com";
           "http://news.google.com";   "http://www.maps.google.com";
           "http://www.bing.com";      "http://www.microsoft.com";
           "http://www.facebook.com";  "http://www.docs.google.com";
           "http://www.youtube.com";   "http://www.gmail.com";
           "http://www.reddit.com";    "http://www.twitter.com";   ]


   #time "on"

   let runSync () =
       sites
       |> List.map httpSync
       |> List.iter(fun (url, html) -> printfn "Downloaded %s - Html size %d" url html.Length)

   runSync()

   let runAsync () =
       sites
       |> Seq.map httpAsync
       |> Async.Parallel
       |> Async.RunSynchronously
       |> Array.iter(fun (url, html) -> printfn "Downloaded %s - Html size %d" url html.Length)

   runAsync ()

module basicAgent =

    type Agent<'T> = MailboxProcessor<'T>


    let printingAgent =
           Agent.Start(fun inbox ->
             async { while true do
                       let! msg = inbox.Receive()
                       if msg = "100000" then
                          printfn "got message %s" msg } )


    printingAgent.Post "three"
    printingAgent.Post "four"

    for i in 0 .. 100000 do
        printingAgent.Post (string i)

    let agents =
       [ for i in 0 .. 100000 ->
           Agent.Start(fun inbox ->
             async { while true do
                       let! msg = inbox.Receive()
                       printfn "%d got message %s" i msg })]

    for agent in agents do
        agent.Post "Hello"

    /// This is a mailbox processing agent that accepts integer messages
    let countingAgent =
        Agent.Start (fun inbox ->
            let rec loop(state) =
                async { printfn "Agent, current state = %d" state
                        let! msg = inbox.Receive()

                        printfn "Agent, current state = %d" state
                        return! loop(state+msg) }

            loop(0)
        )

    countingAgent.Post 3
    countingAgent.Post 4

    for i = 0 to 100 do
        countingAgent.Post i


