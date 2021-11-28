
module Intro =

    let data = ("Cleveland", 390000)
    let city, population = data

    let x = 9
    match x with
      | num when num < 10 -> printfn "Less than ten"
      | _ -> printfn "Greater than or equal to ten"



    let n = Some 7

    let _, v = (7, "Hello")

    let myt = (40, 50)

    let a = snd myt

    let printNum num =
      match num with
      | 1 -> printfn "number 1"
      | n when n > 10 -> printfn "number %d" n
      | _ -> printfn "this is not what I was looking"
  

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
