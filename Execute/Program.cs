using CustomMVC.App.Hosting.Application;
using CustomMVC.App.MVC.Extensions;

var builder = WebApplication.CreateBuilder();
builder.Host.Configure(opt =>
{
    opt.ConnectionString = "http://localhost:8888/";
});

var app = builder.Build();

app.UseControllersWithViews();

app.MapControllerRoute(
    "default",
    "{controller=Home}");

app.Run();