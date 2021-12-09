(*
    Every morning on your way to the office, you pull your car up to your favorite coffee shop
    for a Grande Skinny Cinnamon Dolce Latte with whip.
    The barista always serves you exactly what you order.
    She can do this because you placed your order using precise language that she understands. Y
    ou don’t have to explain the meaning of every term that you utter,
    though to others what you say might be incomprehensible
*)


    // LAB
    // Define few F# types so that we can run the following "Price" function
    // NOTE: base on the types used in the function, for example, we might need
    //       - a "size" DU with some cases
    //       - a "drink" DU with some cases
    //       - a "extra" DU with some cases
    //       - a "Cup" record type with some static member


    let Price (cup:Cup) =
        let tall, grande, venti =
            match cup.Drink with
            | Latte      -> 2.69, 3.19, 3.49
            | Cappuccino -> 2.69, 3.19, 3.49
            | Mocha      -> 2.99, 3.49, 3.79
            | Americano  -> 1.89, 2.19, 2.59
        let basePrice =
            match cup.Size with
            | Tall -> tall
            | Grande -> grande
            | Venti -> venti
        let extras =
            cup.Extras |> List.sumBy (function
                | Shot -> 0.59
                | Syrup -> 0.39
            )
        basePrice + extras

    let myCoffee = Cup.Of Grande Cappuccino + Shot
    let price = myCoffee |> Price
    printfn "THe price of my coffee is %f" price
