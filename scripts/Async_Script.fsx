open System
open System.IO
open System.Net
open System.Threading
open System.Threading.Tasks

// ===========================================
// Async vs Sync
// ===========================================

let httpsync url =
    let req =  WebRequest.Create(Uri url)
    use resp = req.GetResponse()
    use stream = resp.GetResponseStream()
    use reader = new StreamReader(stream)
    let contents = reader.ReadToEnd()
    contents.Length

let httpasync url =
    async { let req =  WebRequest.Create(Uri url)
            use! resp = req.AsyncGetResponse()
            use stream = resp.GetResponseStream()
            use reader = new StreamReader(stream)
            let! contents = reader.ReadToEndAsync() |> Async.AwaitTask
            return contents.Length }

#time "on"

let sites =
    [   "http://www.live.com"; "http://www.fsharp.org";
        "http://news.live.com"; "http://www.digg.com";
        "http://www.yahoo.com"; "http://www.amazon.com"
        "http://www.google.com"; "http://www.netflix.com";
        "http://www.facebook.com";"http://www.docs.google.com";
        "http://www.youtube.com"; "http://www.gmail.com";
        "http://www.reddit.com"; "http://www.twitter.com"; ]


let htmlOfSitesSync =
    [for site in sites -> httpsync site]

let htmlOfSites =
    sites
    |> Seq.map httpasync
    |> Async.Parallel

htmlOfSites |> Async.RunSynchronously



// ===========================================
// Async Error Handling
// ===========================================


let htmlOfSitesErrorHandling =
    sites
    |> Seq.map (httpasync)
    |> Async.Parallel
    |> Async.Catch
    |> Async.RunSynchronously
    |> function
        | Choice1Of2 result     -> printfn "Async operation completed: %A" result
        | Choice2Of2 (ex : exn) -> printfn "Exception thrown: %s" ex.Message


// ===========================================
// Async Cancellation Handling
// ===========================================

let testLoop = async {
    for i in [1..100] do
    // do something
    printf "%i before.." i
    
    // sleep a bit 
    do! Async.Sleep 10  
    printfn "..after"
    }

// create a cancellation source
let cancellationSource = new CancellationTokenSource()

// start the task, but this time pass in a cancellation token
Async.Start (testLoop,cancellationSource.Token)

// wait a bit
Thread.Sleep(200)  

// cancel after 200ms
cancellationSource.Cancel()




let getCancellationToken() = new System.Threading.CancellationTokenSource()


let cancellationToken = getCancellationToken()
Async.Start(htmlOfSites |> Async.Ignore, cancellationToken=cancellationToken.Token)

cancellationToken.Cancel()



let cancellationToken' = getCancellationToken()

// Callback used when the operation is canceled
let cancelHandler (ex : OperationCanceledException) =
    printfn "The task has been canceled."


let tryCancelledAsyncOp = Async.TryCancelled(htmlOfSites |> Async.Ignore, cancelHandler)

Async.Start(tryCancelledAsyncOp, cancellationToken=cancellationToken'.Token)

cancellationToken'.Cancel()




let cts=new CancellationTokenSource()
let rec loop i=
  async{do! Async.Sleep 1000
        printfn"loop----%d" i
//        if i>5 then failwith "i>5"
        if i>6 then ()
        else return! loop<|i+1
  }
Async.StartWithContinuations(
        loop 0,
        (fun cont->printfn "cont-%A" cont),
        (fun exn->printfn"exception-%s"<|exn.ToString()),
        (fun exn->printfn"cancell-%s"<|exn.ToString()),
        cts.Token)
cts.Cancel() 