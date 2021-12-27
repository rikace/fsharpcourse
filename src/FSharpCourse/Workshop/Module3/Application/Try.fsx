#load "Types.fs"
#load "Functions.fs"

open System
open Types
open Functions


let customer = {
    Id = 1
    IsVip = false
    Credit = 0M
    PersonalDetails = Some {
        FirstName = "John"
        LastName = "Doe"
        DoB = DateTime(1980, 11, 23)
    }
    Notification = ReceiveNotification(receiveDeals = true, receiveAlerts = true)
}

let purchases = (customer, 10001M)
let vipCustomer = tryPromoteToVip purchases

(*let calculatedPurchases = getPurchases customer

let customerWithMoreCredit = customer |> increaseCredit (fun c -> c.IsVip)
let upgradedCustomer = upgradeCustomer customer*)
