module Tests

open NUnit.Framework

[<SetUp>]
let Setup () =
    ()


open Types
open Functions

[<Test>]
let ``1-1 Create customer``() =
   let customer = { Id = 1; IsVip = false; Credit = 0M }
   Assert.AreEqual(customer.GetType () ,typeof<Customer>)

[<Test>]
let ``1-2 Promote to vip``() =
    let customer = { Id = 1; IsVip = false; Credit = 0M }
    let promotedCustomer = tryPromoteToVip (customer, 100.1M)
    Assert.IsTrue promotedCustomer.IsVip
//
//[<Test>]
//let ``1-3 Do not promote to vip``() =
//    let customer = { Id = 1; IsVip = false; Credit = 0M }
//    let promotedCustomer = tryPromoteToVip (customer, 99.9M)
//    Assert.IsFalse promotedCustomer.IsVip
//
//[<Test>]
//let ``1-4 Get purchases for odd customers``() =
//    let customer = { Id = 1; IsVip = false; Credit = 0M }
//    let _, purchases = getPurchases customer
//    Assert.Equals purchases 80M
//
//[<Test>]
//let ``1-5 Get purchases for even customers``() =
//    let customer = { Id = 2; IsVip = false; Credit = 0M }
//    let _, purchases = getPurchases customer
//    Assert.Equals purchases 120M
