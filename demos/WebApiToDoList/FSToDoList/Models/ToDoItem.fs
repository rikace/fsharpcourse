namespace FSToDoList

open System.ComponentModel.DataAnnotations

module Models =
  
    [<CLIMutable>]
    type ToDoItem =
        {
            Id : int
            [<Required>]
            Name : string
            IsComplete : bool
        }
