open System // <- to using in C#

// single line comments use a double slash
(* multi line comments use (* . . . *) 
pair -end of multi line comment- *)

// let binding
module binding =
    let myInt = 42
    let mutable myFloat = 3.14
    let myString = "hello functional programming"
    let myFunction = fun number -> number * number

    // Create mutable types – mutable and ref
    let mutable myNumber = 42
    myNumber  <-  51

    let myRefVar = ref 42
    myRefVar := 53
    printfn "%d" !myRefVar


    //  val printSum : a:int -> b:int -> unit
    let printSum (a: int) (b: int) = 
        let result = a + b
        printfn "Result = %d" result
       

module basic =

    let num = 9
    let fl = 10.2
    let st = "hello"
    let myt = 3, "hello", true


    let n = Some 7

    let _, v = (7, "Hello")

    let myt = (40, 50)

    let a = snd myt

    let printNum num =
      match num with
      | 1 -> printfn "number 1"
      | n when n > 10 -> printfn "number %d" n
      | _ -> printfn "this is not what I was looking"


// Functions as first class types
module FunctionFirstClass =
    
 
    let name = "ricky" // val name : string = "ricky"
    let square x = x * x // val square : x:int -> int
    let plusOne (x: int) = x + 1 // val plusOne : x:int -> int
    let isEven x = x % 2 = 0

    let add a b = a + b

    7 |> add 6
    add  6 7 
 

    // Composition - Pipe and Composition operators
    // let inline (|>) x f = f x
    // let inline (>>) f g x = g(f x)
    7 |> square 
    let squarePlusOne x =  x |> square |> plusOne
    
    let plusOneIsEven = plusOne >> isEven
    plusOneIsEven 9

// Delegates
module Delegates =
    // type delegate typename = delegate of typeA -> typeB

    type MyDelegate = delegate of (int * int) -> int
    let add (a, b) = a + b
    let addDelegate = MyDelegate(add)
    let result = addDelegate.Invoke(33, 9)


// Special String definition
module Strings =
    let myStr = "ricky"
    let verbatimHtml = @"<input type=\""submit\"" value=\""Submit\"">"
    let tripleHTML = """<input type="submit" value="Submit">"""

    let world = "World"
    let ``hello world`` = $"Hello %s{world}"

    Console.WriteLine ``hello world``

module MyMod = 
    type Person(name) = 
        member this.name = name

    module Person = 
        let printName (p:Person) = p.name 

// Tuple
module Tuple =
    let tuple = (1, "Hello")
    let tripleTuple = ("one", "two", "three")

    let tupleStruct = struct (1, "Hello")

    let (a, b) = tuple
    let swap (a, b) = (b, a)

    let one = fst tuple
    let hello = snd tuple



// Record-Types
module RecordTypes =
    
    type Person = { FirstName : string; LastName : string; Age : int }

    let fred = { FirstName = "Fred"; LastName = "Flintstone"; Age = 42 }

    let fn = fred.FirstName






    type Person with
        member this.FullName = sprintf "%s %s" this.FirstName this.LastName

    let olderFred = { fred with Age = fred.Age + 1 }

    [<Struct>]
    type Person_Struct = { FirstName : string; LastName : string; Age : int }

// Discriminated Unions
module Discriminated_Unions =
    type Suit = Hearts | Clubs | Diamonds | Spades

    type Rank =
            | Value of int
            | Ace
            | King
            | Queen
            | Jack
            static member GetAllRanks() =
                [ yield Ace
                  for i in 2 .. 10 do yield Value i
                  yield Jack
                  yield Queen
                  yield King ]

    type Card = { Suit:Suit; Rank:Rank }

    let fullDeck =
            [ for suit in [ Hearts; Diamonds; Clubs; Spades] do
                  for rank in Rank.GetAllRanks() do
                      yield { Suit=suit; Rank=rank } ]

// Pattern matching
module Pattern_matching =

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


    //  Active patterns
    let (|Contains|_|) (value: string) (search: string) =
        if search.Contains(value) then Some () else None

    let test value =
        match value with
        | Contains "one" -> printfn "1"
        | Contains "two" -> printfn "2"
        | _ -> printfn "nope"


    let (|FileExtension|) (filePath: string) = System.IO.Path.GetExtension(filePath)

    let determinateFileExt (filePath: string) =
        match filePath with
        | FileExtension ".text" -> "this file is an text"
        | FileExtension ".doc" when String.IsNullOrEmpty(filePath) -> "this file is a document"
        | FileExtension (".jpg" | ".png" | ".gif") -> "this file is an image"
        | ext -> "unknown"


    Console.WriteLine ( determinateFileExt "file.doc" )
    Console.WriteLine ( determinateFileExt "file.png" )
    Console.WriteLine ( determinateFileExt "file.xml" )



    // Better Fizz Buzz

    let (|DivisibleBy|_|) divideBy n =
       if n % divideBy = 0 then Some DivisibleBy else None


    let fizzBuzz'' n =
        match n with
        | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
        | DivisibleBy 3 -> "Fizz"
        | DivisibleBy 5 -> "Buzz"
        | _ -> sprintf "%d" n

    [1..20] |> List.iter(fun s -> printfn "%s" (fizzBuzz'' s))

    let (|Fizz|Buzz|FizzBuzz|Val|) n =
        match n % 3, n % 5 with
        | 0, 0 -> FizzBuzz
        | 0, _ -> Fizz
        | _, 0 -> Buzz
        | _ -> Val n

// Arrays
module Arrays =
    let emptyArray1 = Array.empty
    let emptyArray2 = [| |]
    let arrayOfFiveElements = [| 1; 2; 3; 4; 5 |]
    let arrayFromTwoToTen= [| 2..10 |]
    let appendTwoArrays = emptyArray1 |> Array.append arrayFromTwoToTen
    let evenNumbers = arrayFromTwoToTen |> Array.filter(fun n -> n % 2 = 0)
    let squareNumbers = evenNumbers |> Array.map(fun n -> n * n)

    let arr = Array.init 10 (fun i -> i * i)
    arr.[1] <- 42
    arr.[7] <- 91

    let arrOfBytes = Array.create 42 0uy
    let arrOfSquare = Array.init 42 (fun i -> i * i)
    let arrOfIntegers = Array.zeroCreate<int> 42

// Sequences
module Sequences =
    let emptySeq = Seq.empty
    let seqFromTwoToFive = seq { yield 2; yield 3; yield 4; yield 5 }
    let seqOfFiveElements = seq { 1 .. 5 }
    let concatenateTwoSeqs = emptySeq |> Seq.append seqOfFiveElements
    let oddNumbers = seqFromTwoToFive |> Seq.filter(fun n -> n % 2 <> 0)
    let doubleNumbers = oddNumbers |> Seq.map(fun n -> n + n)

// Lists
module Lists =
    let emptyList1 = List.empty
    let emptyList2 = [ ]
    let listOfFiveElements = [ 1; 2; 3; 4; 5 ]
    let listFromTwoToTen = [ 2..10 ]
    let appendOneToEmptyList = 1::emptyList1
    let concatenateTwoLists = listOfFiveElements @ listFromTwoToTen
    let evenNumbers = listOfFiveElements |> List.filter(fun n -> n % 2 = 0)
    let squareNumbers = evenNumbers |> List.map(fun n -> n * n)

// Sets
module Sets =
    let emptySet = Set.empty<int>
    let setWithOneItem = emptySet.Add 8
    let setFromList = [ 1..10 ] |> Set.ofList

// Maps
module Maps =
    let emptyMap = Map.empty<int, string>
    let mapWithOneItem = emptyMap.Add(42, "the answer to the meaning of life")
    let mapFromList = [ (1, "Hello"), (2, "World") ] |> Map.ofSeq

// Loops
module Loops =
    let mutable a = 10
    while (a < 20) do
       printfn "value of a: %d" a
       a <- a + 1

    for i = 1 to 10 do
        printf "%d " i

    for i in [1..10] do
       printfn "%d" i

// Class and inheritance
module Class_and_inheritance =

    type Person(firstName, lastName, age) =
        member this.FirstName = firstName
        member this.LastName = lastName
        member this.Age = age

        member this.UpdateAge(n:int) =
            Person(firstName, lastName, age + n)

        override this.ToString() =
            sprintf "%s %s" firstName lastName


    type Student(firstName, lastName, age, grade) =
        inherit Person(firstName, lastName, age)

        member this.Grade = grade

// Abstract classes and inheritance
module Abstract_class_and_inheritance =
    [<AbstractClass>]
    type Shape(weight :float, height :float) =
        member this.Weight = weight
        member this.Height = height

        abstract member Area : unit -> float
        default this.Area() = weight * height


    type Rectangle(weight :float, height :float) =
        inherit Shape(weight, height)


    type Circle(radius :float) =
        inherit Shape(radius, radius)
        override this.Area() = radius * radius * Math.PI


// Interfaces
module Interfaces =
    type IPerson =
       abstract FirstName : string
       abstract LastName : string
       abstract FullName : unit -> string

    type Person(firstName : string, lastName : string) =
        interface IPerson with
            member this.FirstName = firstName
            member this.LastName = lastName
            member this.FullName() = sprintf "%s %s" firstName lastName

    let fred = Person("Fred", "Flintstone")

    (fred :> IPerson).FullName()
    |> ignore

// Object expressions
module Object_expressions =
    let print color =
        let current = Console.ForegroundColor
        Console.ForegroundColor <- color
        {   new IDisposable with
                 member x.Dispose() =
                    Console.ForegroundColor <- current
        }

    using(print ConsoleColor.Red) (fun _ -> printf "Hello in red!!")
    using(print ConsoleColor.Blue) (fun _ -> printf "Hello in blue!!")

// Casting
module Castings =
    open Interfaces

    let testPersonType (o:obj) =
           match o with
           | :? IPerson as person -> printfn "this object is an IPerson"
           | _ -> printfn "this is not an IPerson"


// Units of Measure
module Measure =
    [<Measure>]
    type m

    [<Measure>]
    type sec

    let distance = 25.0<m>
    let time = 10.0<sec>

    let speed = distance / time


    [<Measure>]
    type mi

    [<Measure>]
    type km

    // define some values
    let mike = [| 6<mi>; 9<mi>; 5<mi>; 18<mi> |]
    let chris = [| 3<km>; 5<km>; 2<km>; 8<km> |]

    // let totalDistance = (Array.append mike chris) |> Array.sum


module OOPfeatures =

   open System

   type Person(age, firstname, surname) =
       let fullName = sprintf "%s %s" firstname surname

       member __.PrintFullName() =
           printfn "%s is %d years old" fullName age

       member this.Age = age
       member that.Name = fullName
       member val FavouriteColour = System.Drawing.Color.Green with get,set


   type IQuack =
       abstract member Quack : unit -> unit

   // A class that implements interfaces and overrides methods
   type Duck() =
       interface IQuack with
           member this.Quack() = printfn "QUACK!"

   module Quackers =
       let superQuack =
           { new IQuack with
               member this.Quack() = printfn "What type of animal am I?" }


   [<AbstractClass>]
   type Employee(name:string) =
       member __.Name = name
       abstract member Work : unit -> string
       member this.DoWork() =
           printfn "%s is working hard: %s!" name (this.Work())

   type ProjectManager(name:string) =
       inherit Employee(name)
       override this.Work() = "Creating a project plan"

   // Exception Handling
   module Exceptions =
       let riskyCode() =
           raise(ApplicationException())
           ()
       let runSafely() =
           try riskyCode()
           with
           | :? ApplicationException as ex -> printfn "Got an application exception! %O" ex
           | :? System.MissingFieldException as ex -> printfn "Got a missing field exception! %O" ex
           | ex -> printfn "Got some other type of exception! %O" ex


   // Resource Management
   module ResourceManagement =
       let createDisposable() =
           printfn "Created!"
           { new IDisposable with member __.Dispose() = printfn "Disposed!" }

       let foo() =
           use x = createDisposable()
           printfn "inside!"

       let bar() =
           using (createDisposable()) (fun x ->
               printfn "inside!")

   // Casting
   module Casting =
       let anException = Exception()
       let upcastToObject = anException :> obj


   // Active Patterns
   module ActivePatterns =
       let (|Long|Medium|Short|) (value:string) =
           if value.Length < 5 then Short
           elif value.Length < 10 then Medium
           else Long

       match "Hello" with
       | Short -> "This is a short string!"
       | Medium -> "This is a medium string!"
       | Long -> "This is a long string!"

   // Lazy Computations
   module Lazy =
       let lazyText =
           lazy
               let x = 5 + 5
               printfn "%O: Hello! Answer is %d" System.DateTime.UtcNow x
               x

       let text = lazyText.Value
       let text2 = lazyText.Value

   // Recursion
   module Recursion =
       let rec factorial number total =
           if number = 1 then total
           else
               printfn "Number %d" number
               factorial (number - 1) (total * number)


       factorial 5 1

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

        //test
       printfn "%A" (quicksort [1;5;23;18;9;1;3])

       let rec quicksort2 = function
           | [] -> []
           | first::rest ->
                let smaller,larger = List.partition ((>=) first) rest
                List.concat [quicksort2 smaller; [first]; quicksort2 larger]

       printfn "%A" (quicksort2 [1;5;23;18;9;1;3])


   module Recursive_DiscriminatedUnion =

        type Expr =
            | Num      of int
            | Add      of Expr * Expr
            | Subtract of Expr * Expr
            | Multiply of Expr * Expr
            | Divide   of Expr * Expr
