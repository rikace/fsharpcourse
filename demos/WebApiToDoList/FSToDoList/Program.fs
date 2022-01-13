namespace FSToDoList

open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore
open Microsoft.AspNetCore.Hosting
open FSToDoList.DataContext

module Program =
    let exitCode = 0

    let CreateWebHostBuilder args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()

    [<EntryPoint>]
    let main args =
        let host = CreateWebHostBuilder(args).Build()
        use scope = host.Services.CreateScope()
        let services = scope.ServiceProvider
        let context = services.GetRequiredService<ToDoContext>()
        
        Initialize(context) |> ignore
        host.Run()
        exitCode
