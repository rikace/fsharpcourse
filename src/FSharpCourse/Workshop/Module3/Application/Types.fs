module Types

open System


type PersonalDetails = {
    FirstName :string
    LastName : string
    DoB : DateTime
}

type Notification = 
    | NoNotification
    |ReceiveNotification of receiveDeals : bool * receiveAlerts: bool



type Customer = {
    Id: int
    IsVip: bool
    Credit : decimal
    PersonalDetails : PersonalDetails option
    Notification : Notification
}
 
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


