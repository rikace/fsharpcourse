module Functions

open Types


let tryPromoteToVip purchases =
    let cust, amount = purchases
    if amount > 1000M then { cust with IsVip = true }
    else cust




let cust = { Id = 2; IsVip = true; Credit = 10000M }


let purchases1 = (cust, 120M)
let purchases2 = (cust, 80M)

let getPurchases customer =
     if (customer.Id % 2) = 0 then purchases1
     else purchases2


let calculatedPurchases = getPurchases cust

