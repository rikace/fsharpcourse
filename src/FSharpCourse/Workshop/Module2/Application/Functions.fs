module Functions

open Types

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 100M then { customer with IsVip = true }
    else customer

let getPurchases customer =
    if customer.Id % 2 = 0 then (customer, 120M)
    else (customer, 80M)

let increaseCredit condition customer =
    if condition customer then { customer with Credit = customer.Credit + 100M }
    else { customer with Credit = customer.Credit + 50M }

let increaseCreditUsingVip = increaseCredit (fun c -> c.IsVip)


let upgradeCustomer customer = 
    let customerWithPurchases = getPurchases customer 
    let promotedCustomer = tryPromoteToVip customerWithPurchases
    let upgradedCustomer = increaseCreditUsingVip promotedCustomer
    upgradedCustomer

let upgrade1Customer customer = 
    customer
    |> getPurchases
    |> tryPromoteToVip 
    |> increaseCreditUsingVip


let upgrade2Customer = getPurchases >> tryPromoteToVip >> increaseCreditUsingVip


