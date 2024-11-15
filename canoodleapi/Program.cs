using canoodleapi.Interfaces;
using canoodleapi.Repository;
using Microsoft.AspNetCore;
using System.Text.Json.Serialization;

namespace canoodleapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

        //    public static void Main(string[] args)
        //    {
        //        var builder = WebApplication.CreateSlimBuilder(args);

        //        // Configure services
        //        builder.Services.AddSingleton<DapperContext>();
        //        builder.Services.AddScoped<IRouteRepository, RouteRepository>();
        //        builder.Services.AddScoped<ILaborerRepository, LaborerRepository>();
        //        builder.Services.AddScoped<ILaborerVisitRepository, LaborerVisitRepository>();
        //        builder.Services.AddScoped<IMachineRepository, MachineRepository>();
        //        builder.Services.AddScoped<ISubActivityRepository, SubActivityRepository>();
        //        builder.Services.AddScoped<IMaintenanceActivityRepository, MaintenanceActivityRepository>();
        //        builder.Services.AddScoped<ICompletedActivityRepository, CompletedActivityRepository>();
        //        // Register other repositories similarly
        //        builder.Services.AddControllers();

        //        builder.Services.ConfigureHttpJsonOptions(options =>
        //        {
        //            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        //        });

        //        var app = builder.Build();

        //        var sampleTodos = new Todo[] {
        //            new(1, "Walk the dog"),
        //            new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
        //            new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
        //            new(4, "Clean the bathroom"),
        //            new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
        //        };

        //        var todosApi = app.MapGroup("/todos");
        //        todosApi.MapGet("/", () => sampleTodos);
        //        todosApi.MapGet("/{id}", (int id) =>
        //            sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        //                ? Results.Ok(todo)
        //                : Results.NotFound());

        //        app.Run();
        //    }
        //}

        //public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

        //[JsonSerializable(typeof(Todo[]))]
        //internal partial class AppJsonSerializerContext : JsonSerializerContext
        //{

        //}
    }
