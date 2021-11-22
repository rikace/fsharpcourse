namespace ParallelWebCrawler

module AsyncDemo =

    open System.IO
    open System
    open System.Net

    let httpsync url =
        let req =  WebRequest.Create(Uri url)
        use resp = req.GetResponse()
        use stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        let contents = reader.ReadToEnd()
        printfn "(sync) %s - %d" url  contents.Length
        contents.Length

    let httpasync url = async {
        let req =  WebRequest.Create(Uri url)
        use! resp = req.AsyncGetResponse()
        use stream = resp.GetResponseStream()
        use reader = new StreamReader(stream)
        let contents = reader.ReadToEnd()
        printfn "(async) %s - %d" url  contents.Length
        return contents.Length }

    let lenAsync () =
        let len =
            httpasync "http://www.google.com"
            |> Async.RunSynchronously
        printfn "the size of the google.com web page is %d" len

    // lenAsync ()

    let lenSync () =
        let len = httpsync "http://www.google.com"
        printfn "the size of the google.com web page is %d" len

    // lenSync ()


    let sites = [
        "http://www.bing.com";
        "http://www.google.com";
        "http://www.yahoo.com";
        "http://www.facebook.com";
        "http://www.microsoft.com"
        "http://www.bing.com";
        "http://www.google.com";
        "http://www.yahoo.com";
        "http://www.facebook.com";
        "http://www.youtube.com";
        "http://www.reddit.com";
        "http://www.digg.com";
        "http://www.twitter.com";
        "http://www.gmail.com";
        "http://www.docs.google.com";
        "http://www.maps.google.com";
        "http://www.microsoft.com";
        "http://www.netflix.com";
        "http://www.hulu.com" ]

    #time "on"

    let htmlOfSitesSync () =
        [for site in sites -> httpsync site]

    // htmlOfSitesSync () |> ignore

    let htmlOfSites () =
        sites
        |> Seq.map (httpasync)
        |> Async.Parallel
        |> Async.RunSynchronously

    // htmlOfSites () |> ignore


module AgentDemo =

    let printerAgent = MailboxProcessor.Start(fun inbox->
        // the message processing function
        let rec messageLoop() = async{
            // read a message
            let! msg = inbox.Receive()

            do! Async.Sleep(500)

            // process a message
            printfn "message is: %s" msg
            // loop to top
            return! messageLoop()
            }
        // start the loop
        messageLoop()
        )

    printerAgent.Post "hello"
    printerAgent.Post "hello again"
    printerAgent.Post "hello a third time"


    /// Represents different messages
    /// handled by the stats agent
    type StatsMessage =
      | Add of float
      | Clear
      | GetAverage of AsyncReplyChannel<float>

    let stats =
      MailboxProcessor.Start(fun inbox ->
        // Loops, keeping a list of numbers
        let rec loop nums = async {
          let! msg = inbox.Receive()
          match msg with
          | Add num ->
              let newNums = num::nums
              return! loop newNums
          | GetAverage repl ->
              repl.Reply(List.average nums)
              return! loop nums
          | Clear ->
              return! loop [] }

        loop [] )

    // Add error handler
    stats.Error.Add(fun e -> printfn "Oops: %A" e)

    // Post messages
    stats.Post(Add(10.0))
    stats.Post(Add(7.0))

    let average = stats.PostAndReply(GetAverage)
    printfn "%A" average

    stats.Post(Clear)

    let error = stats.PostAndReply(GetAverage)

    open AsyncDemo
    open System.Threading

    let parallelWorker (workers:int) f =
        let agent = new MailboxProcessor<'a>((fun inbox ->
            let agents = Array.init workers (fun _ -> MailboxProcessor.Start(f))
            let rec loop i = async {
                let! msg = inbox.Receive()
                agents.[i].Post(msg)
                return! loop((i+1) % workers)
            }
            loop 0))
        agent.Start()
        agent


    let f =
        fun (inbox: MailboxProcessor<_>) ->
            let rec loop () = async {
                let! msg = inbox.Receive()
                let! len =  httpasync msg
                printfn "the size of the %s web page is %d - Thread id#%d" msg len Thread.CurrentThread.ManagedThreadId
                return! loop ()
            }
            loop ()


    let agentWorker = parallelWorker 8 f

    sites
    |> List.iter agentWorker.Post
