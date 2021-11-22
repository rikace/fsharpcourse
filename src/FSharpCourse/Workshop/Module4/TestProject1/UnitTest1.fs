module TestProject1

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

open System
open Types
open Functions

let customer = {
    Id = 1
    IsVip = false
    Credit = 0M<USD>
    PersonalDetails = Some {
        FirstName = "John"
        LastName = "Doe"
        DateOfBirth = DateTime(1970, 11, 23) }
    Notifications = ReceiveNotifications(receiveDeals = true,
                                         receiveAlerts = true) }

//[<Test>]
//let ``4-1 Get purchases average``() =
//    let purchases = getPurchases customer
//    Assert.Equals(purchases, (customer, 60M))
//
//[<Test>]
//let ``4-2 Upgrade customer``() =
//    let service = CustomerService() :> ICustomerService
//    let upgradedCustomer = service.UpgradeCustomer 2
//    Assert.IsTrue upgradedCustomer.IsVip
//    Assert.Equals (upgradedCustomer.Credit, 110M<USD>)
//
//[<Test>]
//let ``4-3 Get customer info``() =
//    let service = CustomerService() :> ICustomerService
//    let info = service.GetCustomerInfo customer
//    let expectedInfo = "Id: 1, IsVip: false, Credit: 0.00, IsAdult: true, Alert: Alert for customer 1"
//    Assert.Equals (info, expectedInfo)
