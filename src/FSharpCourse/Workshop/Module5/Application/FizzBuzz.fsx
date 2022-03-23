namespace FizzBuzzApplication

module FizzBuzz =

[1..100]
    |> List.map (fun x ->
                match x with
                | _ when x % 15 = 0 ->"fizzbuzz"
                | _ when x % 5 = 0 -> "buzz"
                | _ when x % 3 = 0 -> "fizz"
                | _ ->  x.ToString())
    |> List.iter (fun x -> printfn "%s" x)    

    let fizzBuzz n =
        let divisibleBy m = n % m = 0
        match divisibleBy 3,divisibleBy 5 with
            | true, false -> "Fizz"
            | false, true -> "Buzz"
            | true, true -> "FizzBuzz"
            | false, false -> sprintf "%d" n

    let fizzBuzz' n =
        match n with
        | _ when (n % 15) = 0 -> "FizzBuzz"
        | _ when (n % 3) = 0 -> "Fizz"
        | _ when (n % 5) = 0 -> "Buzz"
        | _ -> sprintf "%d" n

    [1..20] |> List.iter(fun s -> printfn "%s" (fizzBuzz' s))



    // LAB
    //  Active patterns
    //  Implement the "Fizz Buzz" game using Active Patterns
    //  Fizz Buzz is an algorithm game the test a set of number and print :
    //      - "Fizz" if the number is divisible by 3
    //      - "Buzz" if the number is divisible by 5
    //      - "FizzBuzz" if the number is divisible by 3 and 5
    //      - the number value itself if the number otherwise

    //// convert this "fizzbuzz" to use the active patterns instead
    //let fizzbuzz n =
    //    let divisibleby m = n % m = 0
    //    match divisibleby 3,divisibleby 5 with
    //        | true, false -> "fizz"
    //        | false, true -> "buzz"
    //        | true, true -> "fizzbuzz"
    //        | false, false -> sprintf "%d" n
//module FizzBuzz with Active Patterns =
let (|Fizz|None|) x =
  if x % 3 = 0 then Fizz else None

let (|Buzz|None|) x =
  if x % 5 = 0 then Buzz else None

let fizzbuzz x =
  match x with
    | Fizz & Buzz -> "FizzBuzz"
    | Fizz -> "Fizz"
    | Buzz -> "Buzz"
    | _ -> x.ToString()

let start xs =
  for i in xs do
    printfn "%s" (fizzbuzz i)
 
 start {1 .. 15}