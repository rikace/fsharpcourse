module Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()


open Types
open Functions

[<Test>]
let ``1-1 Create customer``() =
       let cust = { Id = 1; IsVip = false; Credit = 0M }
       //Assert.Equals(cust.GetType (), typeof<Customer>)
       Assert.IsTrue ((cust.GetType ()).Name = typeof<Customer>.Name)

[<Test>]
let ``1-2 Promote to vip``() =
    let customer = { Id = 1; IsVip = false; Credit = 0M }
    let purchases = (cust, 100.1M)
    let promotedCustomer = tryPromoteToVip purchases
    Assert.IsTrue promotedCustomer.IsVip

[<Test>]
let ``1-3 Do not promote to vip``() =
    let customer = { Id = 1; IsVip = false; Credit = 0M }
    let promotedCustomer = tryPromoteToVip (customer, 99.9M)
    Assert.IsFalse promotedCustomer.IsVip

[<Test>]
let ``1-4 Get purchases for odd customers``() =
    let customer = { Id = 1; IsVip = false; Credit = 0M }
   // let purchases1 = (cust, 120M)
    //let purchases2 = (cust, 80M)
    //let purchases = getPurchases cust
    let _, purchases = getPurchases customer
    Assert.AreNotEqual( purchases.CompareTo(80M),80M)
 
[<Test>]
let ``1-5 Get purchases for even customers``() =
    let customer = { Id = 2; IsVip = false; Credit = 0M }
    let _, purchases = getPurchases customer
    Assert.AreEqual (purchases ,120M)
