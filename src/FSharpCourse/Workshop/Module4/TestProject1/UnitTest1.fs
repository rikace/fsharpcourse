module TestProject1

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()

open System
open Types
open Functions
open Services

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


[<Test>]
let ``4-1 Get purchases average``() =
    let purchases = getPurchases customer
    let test = (purchases = (customer, 60M))
    Assert.IsTrue test


[<Test>]
let ``4-2 Increase credit using USD``() =
    let upgradedCustomer = increaseCreditUsingVip customer
    let test = upgradedCustomer.Credit = 50M<USD>
    Assert.IsTrue test

[<Test>]
let ``4-3 Upgrade customer``() =
    let service = CustomerService()
    let upgradedCustomer = service.UpgradeCustomer 2
    let test = upgradedCustomer.Credit = 110M<USD>
    Assert.IsFalse upgradedCustomer.IsVip
    Assert.IsFalse test

[<Test>]
let ``4-4 Get customer info``() =
    let service = CustomerService()
    let info = service.GetCustomerInfo customer
    let expectedInfo = "Id: 1, IsVip: false, Credit: 0.00, IsAdult: true, Alert: Alert for customer 1"
    let test = info = expectedInfo
    Assert.IsTrue test
