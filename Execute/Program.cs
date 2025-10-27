using CustomMVC.App.Common;
using CustomMVC.App.Common.Abstractions;
using CustomMVC.App.Core.Middleware.Extensions;
using CustomMVC.App.Hosting.Application;
using CustomMVC.App.Hosting.Host;
using CustomMVC.App.MVC.Extensions;
using Mediator.Extensions;
using Mediator.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder();

builder.Services.UseMediator(opt =>
{
    opt.Assemblies = new[] { Assembly.GetExecutingAssembly() };
});

builder.Services.GetService<IMediator>();

var app = builder.Build();

app.UseDefaultExceptionHandler();

app.UseStaticFiles();

app.UseControllersWithViews();

app.MapControllerRoute(
    "default",
    "{controller=Home}");

app.Run();