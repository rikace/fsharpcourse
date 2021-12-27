module TestProject1

open NUnit.Framework
open System
open Types
open Functions


[<SetUp>]
let Setup () =
    ()



let customer = {
    Id = 1
    IsVip = false
    Credit = 0M
    PersonalDetails = Some {
        FirstName = "John"
        LastName = "Doe"
        DoB = DateTime(1970, 11, 23) }
    Notification = ReceiveNotification(receiveDeals = true,
                                         receiveAlerts = true)
}

[<Test>]
let ``3-1 Create customer``() =
      Assert.IsTrue ((customer.GetType ()).Name = typeof<Customer>.Name)
[<Test>]
let ``3-2 Increase credit using USD``() =
    let upgradedCustomer = increaseCreditUsingVip customer
    Assert.IsTrue( upgradedCustomer.Credit = 50M)


[<Test>]
let ``3-3 Adult customer``() =
    Assert.IsTrue (customer |> isAdult)

[<Test>]
let ``3-4 Non-adult customer``() =
    let nonadult = { customer with PersonalDetails = Some { customer.PersonalDetails.Value with DoB = DateTime.Now.AddYears(-1) } }
    Assert.IsFalse (nonadult |> isAdult)

[<Test>]
let ``3-5 Customer without personal details``() =
    let nonadult = { customer with PersonalDetails = None }
    Assert.IsFalse (nonadult |> isAdult)

[<Test>]
let ``3-6 Get alert when nofications are enabled``() =
    let alert = customer |> getAlert
    Assert.AreEqual (alert, "Alert for customer 1" )

[<Test>]
let ``3-7 Do not get alert when nofications are disabled``() =
    let alert = { customer with Notification = NoNotification } |> getAlert
    Assert.AreEqual (alert, "" )
