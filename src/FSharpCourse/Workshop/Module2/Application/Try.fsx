#load "Types.fs"
#load "Functions.fs"

open Types
open Functions

let Customer = { Id = 1; IsVip = false; Credit = 10M }

let purchases = (Customer, 101M)
let vipCustomer = tryPromoteToVip purchases

let calculatedPurchases = getPurchases Customer

let customerWithMoreCredit = increaseCreditUsingVip Customer
