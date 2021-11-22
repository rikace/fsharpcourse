module Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()


open Types
open Functions

//[<Test>]
//let ``2-1 Increase min credit using id``() =
//    let customer = { Id = 1; IsVip = false; Credit = 0M }
//    let upgradedCustomer = increaseCredit (fun c -> c.Id = 2) customer
//    Assert.Equals upgradedCustomer.Credit 50M
//
//[<Test>]
//let ``2-2 Increase max credit using id``() =
//    let customer = { Id = 2; IsVip = false; Credit = 0M }
//    let upgradedCustomer = increaseCredit (fun c -> c.Id = 2) customer
//    Assert.Equals upgradedCustomer.Credit 100M
//
//[<Test>]
//let ``2-3 Increase credit keeping existing one``() =
//    let customer = { Id = 2; IsVip = false; Credit = 10M }
//    let upgradedCustomer = increaseCredit (fun c -> c.Id = 2) customer
//    Assert.Equals upgradedCustomer.Credit 110M
//
//[<Test>]
//let ``2-4 Increase max credit using vip``() =
//    let customer = { Id = 2; IsVip = true; Credit = 0M }
//    let upgradedCustomer = increaseCreditUsingVip customer
//    Assert.Equals upgradedCustomer.Credit 100M
//
//[<Test>]
//let ``2-5 Upgrade customer with even id``() =
//    let customer = { Id = 2; IsVip = false; Credit = 0M }
//    let upgradedCustomer = upgradeCustomer customer
//    Assert.IsTrue (upgradedCustomer.IsVip && upgradedCustomer.Credit = 100M)
//
//[<Test>]
//let ``2-6 Upgrade customer with odd id``() =
//    let customer = { Id = 1; IsVip = false; Credit = 0M }
//    let upgradedCustomer = upgradeCustomer customer
//    Assert.IsFalse (upgradedCustomer.IsVip && upgradedCustomer.Credit = 50M)
