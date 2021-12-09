open System

module Loops =

    let data = [1..100]

    for item in data do
        printfn "Item value %A" item

    for i = 0 to 50 do
        let item = data.[i]
        printfn "Item value %A" item

    let mutable index = 0

    while index < data.Length - 1 do
        let item = data.[index]
        printfn "Item value %A" item
        index <- index + 1


module Recursion =

    let rec factorial number =
        if number = 0 then 1
        else
            //printfn "Number %d" number
            number * factorial (number - 1)

    let tailRecFactorial number =
        let rec fact number acc =
            if number = 0 then acc
            else
                printfn "Number %d" number
                fact (number - 1) acc * number
        fact number 1

    factorial 500000
    tailRecFactorial 5


    let rec quicksort list =
       match list with
       | [] ->                            // If the list is empty
            []                            // return an empty list
       | firstElem::otherElements ->      // If the list is not empty
            let smallerElements =         // extract the smaller ones
                otherElements
                |> List.filter (fun e -> e < firstElem)
                |> quicksort              // and sort them
            let largerElements =          // extract the large ones
                otherElements
                |> List.filter (fun e -> e >= firstElem)
                |> quicksort              // and sort them
            // Combine the 3 parts into a new list and return it
            List.concat [smallerElements; [firstElem]; largerElements]



module ActivePatterns =

    let(|ParseNumber|_|) (text: string) =
        match Int32.TryParse(text) with
        | true, num -> Some(sprintf "Number %d" num)
        | _ -> None

    let (|ParseExpression|_|) (text:string) =
        if text.StartsWith("=") then
            Some(sprintf "Formula %s" text)
        else None

    let (|Lenght|) (text:string) =
        text.Length

    let parseFormula text =
        match text with
        | Lenght 0 -> "Empty"
        | ParseNumber s -> s
        | ParseExpression s -> s
        | _ -> "Error"


   let (|Long|Medium|Short|) (value:string) =
       if value.Length < 5 then Short
       elif value.Length < 10 then Medium
       else Long

   let test () =
       match "Hello" with
       | Short -> "This is a short string!"
       | Medium -> "This is a medium string!"
       | Long -> "This is a long string!"


    // create an active pattern
    open System.Text.RegularExpressions
    let (|FirstRegexGroup|_|) pattern input =
       let m = Regex.Match(input,pattern)
       if (m.Success) then Some m.Groups.[1].Value else None
