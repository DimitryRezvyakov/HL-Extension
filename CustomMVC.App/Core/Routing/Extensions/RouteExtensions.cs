using CustomMVC.App.Hosting.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing.Extensions
{
    public static class RouteExtensions
    {
        public static void UseRouting(this WebApplication app)
        {
            app.PipeLineBuilder.UseRouting = true;
        }
    }
}
