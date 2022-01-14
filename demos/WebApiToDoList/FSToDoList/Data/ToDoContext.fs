namespace FSToDoList

open FSToDoList.Models
open Microsoft.EntityFrameworkCore
open System.Linq

module DataContext =

    type ToDoContext(options : DbContextOptions<ToDoContext>) = 
        inherit DbContext(options)
    
        [<DefaultValue>]
        val mutable ToDoItems : DbSet<ToDoItem>

        member public this._ToDoItems      with    get()      = this.ToDoItems 
                                           and     set value  = this.ToDoItems <- value 

        //returns if the Item exists 
        member this.ToDoItemExist (id:int) = this.ToDoItems.Any(fun x -> x.Id = id)

        //Returns the Item with the given id
        member this.GetToDoItem (id:int) = this.ToDoItems.Find(id)


        override this.OnModelCreating(builder: ModelBuilder) =
            builder.Entity<ToDoItem>().ToTable("ToDos") |> ignore


    let Initialize (context : ToDoContext) =
        
        //context.Database.EnsureDeleted() |> ignore //Deletes the database
        context.Database.EnsureCreated() |> ignore //check if the database is created, if not then creates it

        //default pairs for testing
        let tdItems : ToDoItem[] = 
            [|
                { Id = 0; Name = "Do the software"; IsComplete = true }
                { Id = 0; Name = "Create the Article"; IsComplete = true  }
                { Id = 0; Name = "Upload to the internet"; IsComplete = false }
            |]

        if not(context.ToDoItems.Any()) then
                context.ToDoItems.AddRange(tdItems) |> ignore
                context.SaveChanges() |> ignore  
