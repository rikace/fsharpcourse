open System

[<EntryPoint>]
let main argv =



    let safe (x1, y1) (x2, y2) =
        x1 <> x2 && y1 <> y2 && x2-x1 <> y2-y1 && x1-y2 <> x2-y1

    let rec search f n qs ps =
        match ps with
        | [] -> if List.length qs = n then f qs
        | q::ps ->
            search f n qs ps
            search f n (q::qs) (List.filter (safe q) ps)

    let ps n =
        [ for i in 1 .. n do
            for j in 1 .. n -> i, j ]

    let print n qs =
        for y in 1 .. n do
          for x in 1 .. n do
            printf "%s" (if List.contains (x, y) qs then "Q" else ".")
          printf "\n"
        printf "\n"

    let solve n = search (print n) n [] (ps n)
    solve 8
    0
