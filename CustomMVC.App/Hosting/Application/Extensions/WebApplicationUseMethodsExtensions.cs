using CustomMVC.App.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Application.Extensions
{
    public static class WebApplicationUseMethodsExtensions
    {
        public static void Use(this WebApplication app, Func<HttpContext, Func<Task>, Task> middleware)
        {
            ArgumentNullException.ThrowIfNull(app.WebAppBuilder);

            app.WebAppBuilder.pipelineBuilder.Use(middleware);
        }

        public static void Run(this WebApplication app, Func<HttpContext, Task> terminalMiddleware)
        {
            app.WebAppBuilder.pipelineBuilder.Run(terminalMiddleware);
        }
    }
}
