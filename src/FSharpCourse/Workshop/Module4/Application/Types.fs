module Types

open System

[<Measure>]
type USD
[<Measure>] type EUR

type PersonalDetails = {
    FirstName: string
    LastName: string
    DateOfBirth: DateTime
}


type Notifications =
    | NoNotifications
    | ReceiveNotifications of receiveDeals: bool * receiveAlerts: bool

type Customer = {
    Id: int
    IsVip: bool
    Credit: decimal<USD>
    PersonalDetails : PersonalDetails option
    Notifications: Notifications
}
 
type PurchaseHistory = {
    CustomerId : int
    PurchaseByMonth : decimal list
}
