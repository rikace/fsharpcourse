namespace Services

type ICustomerService =
 abstract UpgradeCustomer : int -> Types.Customer 
 abstract GetCustomerInfo : Types.Customer -> string     