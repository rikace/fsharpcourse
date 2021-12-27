module Functions

open Types
open System

let tryPromoteToVip purchases =
    let customer, amount = purchases
    if amount > 1000M then { customer with IsVip = true }
    else customer

let isAdult customer = 
    match customer.PersonalDetails with
        | None -> false
        | Some d -> d.DoB.AddYears 18 <= DateTime.Now.Date

 
let purchases = (customer, 10001M)
let vipCustomer = tryPromoteToVip purchases

let getPurchases customer =
    if customer.Id % 2 = 0 then (customer, 120M)
    else (customer, 80M)

let increaseCredit condition customer =
    if condition customer then { customer with Credit = customer.Credit + 100M }
    else { customer with Credit = customer.Credit + 50M }

let increaseCreditUsingVip = increaseCredit (fun c -> c.IsVip)

let upgradedCustomer = getPurchases >> tryPromoteToVip >> increaseCreditUsingVip

let getAlert customer =
 match customer.Notification with
 | ReceiveNotification(receiveAlerts = true) ->
 sprintf "Alert for customer %i" customer.Id
 | _ -> ""