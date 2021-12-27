#load "Types.fs"
#load "Functions.fs"

open Types
open Functions

let cust = { Id = 2; IsVip = true; Credit = 10000M }

let purchases = (cust, 1001M)
let vipCustomer = tryPromoteToVip purchases


let purchases1 = (cust, 120M)
let purchases2 = (cust, 80M)

let getPurchases customer =
     if customer.Id % 2 = 0 then purchases1
     else purchases2


let calculatedPurchases = getPurchases cust