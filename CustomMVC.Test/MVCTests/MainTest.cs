using CustomMVC.App.Core.Routing.Extensions;
using CustomMVC.App.Hosting.Application;
using CustomMVC.App.MVC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomMVC.Test.MVCTests
{
    public class MainTest
    {

        [Fact]
        public void OnAppExecutingAndWorking()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Host.Configure(opt =>
            {
                opt.ConnectionString = "http://localhost:8888/";
            });

            var app = builder.Build();
            app.UseRouting();
            app.UseEndpoints();

            app.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}");

            app.Run();

        }
    }
}
