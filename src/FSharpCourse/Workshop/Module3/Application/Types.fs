module Types

open System

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
    Credit: decimal
    PersonalDetails: PersonalDetails option
    Notifications: Notifications
} 