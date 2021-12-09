module Recursion =


    //

    let rec addOne' (input : int list) : int list =
        match input with
        | [] -> []
        | x :: xs -> (x + 1) :: addOne' xs

    // This will cause a StackOverflowException!
    let ohNo' = [ 0 .. 400000 ] |> addOne'

    // LAB
    // Re-write the function 'addOne' in a "Tail optimized fashion"
    // to fix the StackOverflowException error

    // Tail recursion resolves the StackOverflowException by allowing the compiler to perform a mechanical transformation
    // turning the recursive call into a goto statement rather than a function call.
    // A goto statement is not a function call, so does not grow the stack.
    //
    // The addOne example above is not tail recursive because in the non-empty list case x :: xs -> ...
    // there is a recursive call let tail = addOne xs followed by an append to the resulting list from the recursive call head :: tail.

    // let rec addOne (input : int list) ...
