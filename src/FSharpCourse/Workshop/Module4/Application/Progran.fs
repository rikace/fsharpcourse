module Program

open System
open Services
open Functions

[<EntryPoint>]
let rec main args =
    // let service = CustomerService() :> ICustomerService

//    let service =
//        { new ICustomerService with
//            member this.UpgradeCustomer id =
//                id
//                |> Functions.getCustomer
//                |> Functions.upgradeCustomer
//
//            member this.GetCustomerInfo customer =
//                let isAdult = Functions.isAdult customer
//                let alert = Functions.getAlert customer
//                sprintf "Id: %i, IsVip: %b, Credit: %.2f, IsAdult: %b, Alert: %s"
//                    customer.Id customer.IsVip customer.Credit isAdult alert  }
//
//    printf "Id to upgrade [1-4]: "
//    let valid, id = Console.ReadLine () |> Int32.TryParse
//    printfn ""
//    if not valid then
//        printfn "Invalid customer Id"
//    else
//        printfn "Customer to upgrade:"
//        let customerBefore = getCustomer id
//        customerBefore |> service.GetCustomerInfo |> printfn "%s"
//        printfn ""
//        printfn "Upgrading customer..."
//        let customerAfter = service.UpgradeCustomer id
//        printfn ""
//        printfn "Customer upgraded:"
//        customerAfter |> service.GetCustomerInfo |> printfn "%s"
//    printfn ""
//    printfn "Press any key to try again or 'q' to quit"
//    let input = Console.ReadKey ()
//    printfn ""
//    if input.Key = ConsoleKey.Q then 0 else main args
    0
