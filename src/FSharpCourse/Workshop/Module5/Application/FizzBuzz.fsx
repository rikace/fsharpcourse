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

module ``FizzBuzz with Active Patterns`` =

    // LAB
    //  Active patterns
    //  Implement the "Fizz Buzz" game using Active Patterns
    //  Fizz Buzz is an algorithm game the test a set of number and print :
    //      - "Fizz" if the number is divisible by 3
    //      - "Buzz" if the number is divisible by 5
    //      - "FizzBuzz" if the number is divisible by 3 and 5
    //      - the number value itself if the number otherwise

    // Convert this "fizzBuzz" to use the Active patterns instead
    let fizzBuzz n =
        let divisibleBy m = n % m = 0
        match divisibleBy 3,divisibleBy 5 with
            | true, false -> "Fizz"
            | false, true -> "Buzz"
            | true, true -> "FizzBuzz"
            | false, false -> sprintf "%d" n
