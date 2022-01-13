namespace FSToDoList

open System.Collections.Generic
open Microsoft.AspNetCore.Mvc
open FSToDoList.DataContext
open FSToDoList.Models
open Microsoft.EntityFrameworkCore
open System.Linq

[<Route("api/ToDoItems")>]
[<ApiController>]
type ToDoItemsController private () = 
    inherit ControllerBase()

    new (context : ToDoContext) as this =
        ToDoItemsController () then
        this._Context <- context
    
    [<HttpGet>]
    member this.Get() =
        ActionResult<IEnumerable<ToDoItem>>(this._Context.ToDoItems)

    //GET: api/ToDoItems/search
    [<Route("search")>]
    [<HttpGet>]
    member this.Get([<FromBody>] _Values : ToDoItem[] ) =
        if base.ModelState.IsValid then //check the entry
            
            let ToDoItems : List<ToDoItem> = new List<ToDoItem>() // initiates the list (i like to keep the stuff typed)
            
            for value in _Values do //Search all the ToDoItems 
                if (value.Id = 0) then // if the id is not informed then search by the Name
                    ToDoItems.AddRange(this._Context.ToDoItems.Where(fun x -> x.Name = value.Name).ToList())
                else if(this._Context.ToDoItemExist(value.Id)) then // search by the the id
                    ToDoItems.Add(this._Context.ToDoItems.Find(value.Id))

            if (ToDoItems.Count = 0) then
               JsonResult(base.NotFound("NOT FOUND!, The search returned 0 values"))
            else
               JsonResult(base.Ok(ToDoItems))

        else
            JsonResult(base.BadRequest(base.ModelState))



    [<HttpGet("{id}")>]
    member this.Get(id:int) = 
        if base.ModelState.IsValid then  //check the entry
            if not ( this._Context.ToDoItemExist(id) ) then //check the existence of the ToDoItem
                JsonResult(base.NotFound("NOT FOUND!, There is no ToDoItem with this code: " + id.ToString())) // ToDoItem does not exist
            else
                let data = this._Context.GetToDoItem(id)
                let result = base.Ok(data)
                JsonResult(result)
        else
            JsonResult(base.BadRequest(base.ModelState))



    [<HttpPost>]
    member this.Post([<FromBody>] _ToDoItem : ToDoItem) =
        if (base.ModelState.IsValid) then 
            if not( isNull _ToDoItem.Name ) then
                if ( _ToDoItem.Id <> 0 ) then //check if the ID is set
                   JsonResult(base.BadRequest("BAD REQUEST, the ToDoItemID is autoincremented")) // the ToDoItem is autoincremented
                else 
                        this._Context.ToDoItems.Add(_ToDoItem) |> ignore
                        this._Context.SaveChanges() |> ignore
                        JsonResult(base.Ok(this._Context.ToDoItems.Last()))
            else
                JsonResult(base.BadRequest("BAD REQUEST!, the field Initials can not be null"))                    
        else
            JsonResult(base.BadRequest(base.ModelState))

    [<HttpPut("{id}")>]
     member this.Put( id:int, [<FromBody>] _ToDoItem : ToDoItem) =
        if (base.ModelState.IsValid) then 
            if not( isNull _ToDoItem.Name ) then
                if (_ToDoItem.Id <> id) then 
                    JsonResult(base.BadRequest())
                else
                        try//error handler
                            this._Context.Entry(_ToDoItem).State = EntityState.Modified |> ignore
                            this._Context.SaveChanges() |> ignore
                            JsonResult(base.Ok(_ToDoItem))
                        with ex ->
                            if not( this._Context.ToDoItemExist(id) ) then
                                JsonResult(base.NotFound())
                            else 
                                JsonResult(base.BadRequest())
            else
                JsonResult(base.BadRequest())                                
        else    
            JsonResult(base.BadRequest(base.ModelState))

    [<HttpDelete("{id}")>]
    member this.Delete(id:int) =
        if (base.ModelState.IsValid) then 
            if not( this._Context.ToDoItemExist(id) ) then 
               JsonResult(base.NotFound())
            else (
                    this._Context.ToDoItems.Remove(this._Context.GetToDoItem(id)) |> ignore
                    this._Context.SaveChanges() |> ignore
                    JsonResult(base.Ok(this._Context.ToDoItems.Last()))
            )
        else
           JsonResult(base.BadRequest(base.ModelState))

    [<DefaultValue>]
    val mutable _Context : ToDoContext