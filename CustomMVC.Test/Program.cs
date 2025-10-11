using CustomMVC.App;
using CustomMVC.App.Core.Routing.Extensions;
using CustomMVC.App.Hosting.Application;



var builder = WebApplication.CreateBuilder();

builder.hostOptionsBuilder.Configure(cfg =>
    cfg.ConnectionString = "http://localhost:7777/");

var app = builder.Build();

app.UseRouting();

app.Use(async (context, next) =>
{
    Console.WriteLine("1");

    await next();

    Console.WriteLine("After 1");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("2");

    await next();

    Console.WriteLine("After 2");
});

app.Map(async context =>
{
    context.Response.SetStatusCode(200);
    await context.Response.WriteAsync("Hello");
    Console.WriteLine("Map method");
},
"/Index"
);

app.Run();