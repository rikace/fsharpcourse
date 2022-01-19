// Demo 1

let a = 1
let b = 2
let sum x y = x + y
let res = sum a b

let myTuple = (42, "hello")
let number, message = myTuple

type MyRecord = { Number: int; Message: string }
let myRecord = { Number = 42; Message = "hello" }
let newRecord = { myRecord with Message = "hi" }


// Demo 2

let compute (x: int) (y: int) (operation: int -> int -> int) = operation x y
let res' = compute 1 2 sum

let addOne = sum 1
let addTwo = sum 2

let res1 = addOne 1
let res2 = addTwo res1

let res2' =
    1
    |> addOne
    |> addTwo

let addThree = addOne >> addTwo
let res2'' = addThree 1


// Demo 3

let divide x y =
    match y with
    | 0 -> None
    | _ -> Some(x/y)

let result = divide 4 2
let result' = divide 4 0

type DivisionResult =
| DivisionSuccess of result: int
| DivisionError of message: string

let divide' x y =
    match y with
    |0 -> DivisionError(message = "Divide by zero")
    |_ -> DivisionSuccess(result = x / y)

let result'' = divide' 4 2
let result''' = divide' 4 0


type Calc =
    | Num of int
    | Add of Calc * Calc
    | Mul of Calc * Calc 

let rec eval exp = 
    match exp with 
    | Num z -> z
    | Add (a, b) -> (eval a) + (eval b)
    | Mul (a, b) -> (eval a) * (eval b)

Add(Num 40, Num 2) |> eval 
Add(Mul(Num 40, Num 2), Add(Num 4, Num 8)) |> eval 







[<Measure>] type m; [<Measure>] type km; [<Measure>] type h
let distanceInMts = 11580.0<m>
let distanceInKms = 87.34<km>
//let totalDistance = distanceInMts + distanceInKms // Error

let convertToKms (mts: float<m>) =
    let m = mts / 1.0<m> // remove unit of measure
    let k = m / 1000.0   // convert
    k * 1.0<km>          // add new unit of measure

let convertToKms' (mts: float<m>) = mts / 1000.0<m> * 1.0<km>

let convertedToKms = convertToKms distanceInMts
let totalDistance' = convertedToKms + distanceInKms
let speed = totalDistance' / 2.4<h>




let printHuman person =
  match person with
  | (_, age) when age < 2 -> printfn "Infant"
  | (_, 0) -> printfn "error age cannot be zero"
  | (_, age) when age < 18 -> printfn "Adolescent"
  | (_, 20) ->  printfn "Adult"
  | _ -> printfn "Not implemented!"

// TODO: Implement 'printHuman' to do the following:
printHuman ("Alexander", 1)  // prints Infant
printHuman ("Joe", 15)       // prints Adolescent
printHuman ("Ricky", 42)     // prints Adult


module shape =

    // we create a Shape type with four different
    // kinds of shape and do different behavior based on the particular kind
    // of shape.
    // This is similar to polymorphism in an object oriented language,
    // but based on functions.

    type Shape =        // define a "union" of alternative structures
    | Circle of radius:float
    | Rectangle of float * float
    | Polygon of (float * float) list
    
    let draw (shape: Shape) =    // define a function "draw" with a shape param
      match shape with
      | Circle r -> printfn "The circle has a radius of %f" r
      | Rectangle (height,width) when height > 10. && width < 5. -> 
            printfn "The rectangle is %f high by %f wide is more then 10" height width

      | Rectangle (height,width) -> 
        printfn "The rectangle is %f high by %f wide" height width
      | Polygon(a) -> ()
      // | _ -> printfn "I don't recognize this shape"

    let circle = Circle(10.)
    let rect = Rectangle(40.,5.)
    let polygon = Polygon([(1.,1.); (2.,2.); (3.,3.)])
    
    [circle; rect; polygon] |> List.iter(fun s -> draw s)

    let rectangle = Rectangle (2.2, 3.3)
    let cir = Circle 3.4

    let getArea shape =
        match shape with
        | Rectangle (height,width)  -> height * width
        | Circle r -> System.Math.PI * r * r
        | _ -> failwith "No implemented"

    getArea cir
    getArea rectangle

    //getArea null

    let divide x y = 
        if y = 0 then None
        else Some (x/y)
    
    let result = divide 4 2         
    let add a b = a + b

    // Person option CalDB(int id) 
    (isNull)

    // let person = 
    //    if C#Module.Person(1) |> isNull then None
    //    else Some p

    // person?.name

    match result with 
    | None -> 0
    | Some v -> add 4 v

    divide 4 0

    let divide' x y = 
       (x/y)
    divide' 4 0

module MathExp = 

    // =====
    (*
        Now that we have the building blocks to represent ideas 
        in F#, we have all the power we need to represent a real world problem in the language of mathematics.

        In this simple example we were able to represent and evaluate a four-function mathematical expression using only a discriminated union and a pattern match. You would be hard 
        pressed to write the equivalent C# in as few lines of code because you would need to add additional scaffolding to represent these concepts.
    *)
    // This Discriminated Union is sufficient to express any four-function
    // mathematical expression.
    type Expr =
        | Num      of int
        | Add      of Expr * Expr
        | Subtract of Expr * Expr
        | Multiply of Expr * Expr
        | Divide   of Expr * Expr

    type Result1 = 
    | Success of int
    | Error of string
    with static member (+) (a, b) = 
            match a, b with 
            | Success a, Success b -> a + b |> Success
            | _, _ -> Error ""
         static member (-) (a, b) = 
            match a, b with 
            | Success a, Success b -> a - b |> Success
            | _, _ -> Error ""
         static member (*) (a, b) = 
            match a, b with 
            | Success a, Success b -> a * b |> Success
            | _, _ -> Error ""
         static member (/) (a, b) = 
            match a, b with 
            | Success a, Success b -> a / b |> Success
            | _, _ -> Error ""

    type rec2 = {name:string;age:int}
    type rec1 = {name:string;age:int}

    let myrec = {rec1.name = "hello" ;age = 20}

    // This simple pattern match is all we need to evaluate those
    // expressions. 
    let rec evaluate expr =
        match expr with
        | Num(x)             -> Success x
        | Add(lhs, rhs)      -> (evaluate lhs) + (evaluate rhs)
        | Subtract(lhs, rhs) -> (evaluate lhs) - (evaluate rhs)
        | Multiply(lhs, rhs) -> (evaluate lhs) * (evaluate rhs)
        | Divide(lhs, rhs) when (evaluate rhs) = Success 0 -> Error "error" 
        | Divide(lhs, rhs) -> (evaluate lhs) / (evaluate rhs)

    // 10 + 5
    let ``10 + 5`` = 0


    // 10 * 10 - 25 / 5
    let sampleExpr = 
        Subtract(
            Multiply(
                Num(10), 
                Num(10)),
            Divide(
                Num(25), 
                Num(5)))
        
    let result = evaluate sampleExpr


    // It appears that building an internal LOGO-like DSL is surprisingly easy, and requires almost no code! What you need is just to define the basic types to describe your actions:




    type Expression =
        | X
        | Constant of float
        | Add of Expression * Expression
        | Mul of Expression * Expression

    let rec interpret (ex:Expression) =
        match ex with
        | X -> fun (x:float) -> x
        | Constant(value) -> fun (x:float) -> value
        | Add(leftExpression,rightExpression) ->
            let left = interpret leftExpression
            let right = interpret rightExpression
            fun (x:float) -> left x + right x
        | Mul(leftExpression,rightExpression) ->
            let left = interpret leftExpression
            let right = interpret rightExpression
            fun (x:float) -> left x * right x

    let run (x:float,expression:Expression) =
            let f = interpret expression
            let result = f x
            printfn "Result: %.2f" result


    let expression = Add(Constant(1.0),Mul(Constant(2.0),X))
    run(10.0,expression)

    let expression2 = Mul(X,Constant(10.0))
    run(10.0,expression2)

    let add a b = Add(a, b)
    let mul a b = Mul(a, b)
    let c v =  Constant v
    let x = X

    let expression3 = add (c 1.0) (mul (c 2.0) x)
    run(10.0,expression3)



    (*
    Result: 21.00
    Result: 100.00
    *)


module DUs = 
    open System
    open Microsoft.FSharp.Core

    let value = Some 42
    let noValue = None 

    let check value = 
        match value with 
        | Some s -> Some s
        | None -> None 

    check None 
    
    //type Option<'a> = 
    //| Some of 'a
    //| None 

    //let nullable  = Nullable<string>("Hello")
    
    //let isOk = Result 42 

    
    let printOption (opt: Option<string>) =
        match opt with
        | Some x -> printfn "Some string =  %s" x
        | None -> printfn "No string" 
    
    
    printOption "hello"
    printOption null

    printOption (Some "hello")
    printOption (None)


module CardGame =
    type Suit = Club | Diamond | Spade | Heart

    type Rank = Two | Three | Four | Five | Six | Seven | Eight
                | Nine | Ten | Jack | Queen | King | Ace

    type Card = { Suit : Suit; Rank: Rank}
    
    let deck = 
        [ for suit in [ Club; Diamond; Spade; Heart ] do
            for rank in [ Two; Three; Four; Five; Six; Seven; Eight; Nine; Ten; Jack; Queen; King; Ace ] do
                yield { Suit = suit; Rank = rank } ]


    // we should probably use wrapper types for these three types to stop them getting mixed up
    type Hand = Hand of Card list
    type Deck = Deck of Card list

    type ShuffledDeck = ShuffledDeck of Card list



    type Player = { Name : string; Hand : Hand}
    
    type Game = 
        { Deck : Deck; Players : Player list}
            with member this.game() = ()

module ColorExample = 

    type Color = 
    | Red 
    | Black 
    | RGB of r:int * g:int * b:int
        with 
            static member create r g b = RGB(r,g,b)
            
            member self.print () =
                match self with 
                | Red -> printfn "this is RED"
                | Black -> printfn "this is Black"        
                | RGB(0,255,0) -> printfn "This is Green" 
                | RGB(r,g,b) when r > 100 -> printfn "Red is > 100 %d - Green is %d - blue is %d" r g b
                | x -> printfn "No matches for %A" x
            
            override this.ToString() = 
                match this with 
                | _ -> ""
            


    let rgb = Color.create 0 0 255
    
    rgb.ToString()

    rgb.print()

    let black = RGB(0,0,0)

    black.print()

        // member
        // static member
        // override



    let blue = RGB(0,0,255)

    let printColor color = 
        match color with 
        | Red -> printfn "this is RED"
        | Black -> printfn "this is Black"        
        | RGB(0,255,0) -> printfn "This is Green" 
        | RGB(r,g,b) when r > 100 -> printfn "Red is > 100 %d - Green is %d - blue is %d" r g b

        | x -> printfn "No matches for %A" x
        
    printColor blue 
    printColor (RGB(0,255,0))
    printColor (RGB(0,0,0))