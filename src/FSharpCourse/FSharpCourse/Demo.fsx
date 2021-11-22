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


// Demo 4

let numbers = [1..100]
let numbersWithZero = 0 :: numbers
let evenNumbers = numbersWithZero |> List.filter (fun x -> x % 2 = 0)

type MyClass(myField: int) =
    member this.MyProperty = myField
    member this.MyMethod methodParam = myField + methodParam

let myInstance = MyClass(1)
myInstance.MyProperty
myInstance.MyMethod 2

type IMyInterface =
    abstract member MyMethod: int -> int

let myInstance' =
    { new IMyInterface with
       member this.MyMethod methodParam =
              methodParam + 1 }

myInstance'.MyMethod 2


module shape =

    // we create a Shape type with four different
    // kinds of shape and do different behavior based on the particular kind
    // of shape.
    // This is similar to polymorphism in an object oriented language,
    // but based on functions.

    type Shape =        // define a "union" of alternative structures
    | Circle of int
    | Rectangle of int * int
    | Polygon of (int * int) list
    | Point of (int * int)

    let draw shape =    // define a function "draw" with a shape param
      match shape with
      | Circle r -> printfn "The circle has a radius of %d" r
      | Rectangle (height,width) when height > 10-> printfn "The rectangle is %d high by %d wide" height width
     // | Polygon points -> printfn "The polygon is made of these points %A" points
      | _ -> printfn "I don't recognize this shape"

    let circle = Circle(10)
    let rect = Rectangle(4,5)
    let polygon = Polygon( [(1,1); (2,2); (3,3)])
    let point = Point(2,3)

    [circle; rect; polygon; point] |> List.iter draw


    module DiscUnion =

      type Shape  =
        | Square  of int
        | Rectangle of float * float
        | Circle of float

      let sq = Square 7
      let rect = Rectangle (2.2, 3.3)
      let cir = Circle 3.4

      let getArea shape =
        match shape with
        | Square side -> float(side * side)
        | Rectangle(w,h) -> w * h
        | Circle r -> System.Math.PI * r * r

