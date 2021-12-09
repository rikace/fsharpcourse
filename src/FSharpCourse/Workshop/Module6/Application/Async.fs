module AsyncModule

open System
open System.IO
open System.Net
open System.Threading

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

let httpAsync (url : string) = async {
    let req = WebRequest.Create(url)
    let! resp = req.AsyncGetResponse()
    use stream = resp.GetResponseStream()
    use reader = new StreamReader(stream)
    let! text = reader.ReadToEndAsync() |> Async.AwaitTask
    return text
}

// TODO LAB
//  run in parallel
//  Parallel Asynchronous computations
//  Note:  how can we apply defensive programming to handle
//         exceptions? (For example when a "site" does not exist
//  Note:  how can we cancelled the async operation using a "CancellationToken"
let runAsync () =
    sites
    // TODO LAB add missing code jere


// TODO LAB
type RequestGate(n: int) =
    let semaphore = new SemaphoreSlim(n, n)

    member x.Acquire(?timeout: TimeSpan) =
        // TODO LAB
        // implement the logic to coordinate the access to resources
        // using "semaphore". Keep async semantic for the "acquire" and "release" of the handle
        // throw new Exception("No implemented");
        //
        // Note: the "SemaphoreSlim" class could help with the implementation
        // Note: to release the resources, the "SemaphoreSlim" should be release.
        //       Use object-expression to implement and IDisposable interface that when is disposed, the semaphore is released.
        async {
               // TODO LAB
               // Add missing code here
               return! failwith "couldn't acquire a semaphore"
        }


// TODO LAB
//      create a function that throttles the async operations
//      The RequestGate is a good approach (but not the only one)
//      for example "let gate = RequestGate(2)"
//
let httpAsyncThrottle (throttle: int) (url : string) = async {
    // TODO use the throttle value to
    //      limit the number of concurrent operations using the "RequestGate" implementation
    let req = WebRequest.Create(url)
    let! resp = req.AsyncGetResponse()
    use stream = resp.GetResponseStream()
    use reader = new StreamReader(stream)
    let! text = reader.ReadToEndAsync() |> Async.AwaitTask
    return text
}

let runAsyncThrottle () =
    sites
    // |> Seq.map httpAsync
    |> Seq.map (httpAsyncThrottle 2)
    |> Async.Parallel
    |> Async.RunSynchronously


