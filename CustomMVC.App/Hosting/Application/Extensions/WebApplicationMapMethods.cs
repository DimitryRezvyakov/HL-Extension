using CustomMVC.App.Core.Http.HttpMethods.Attributes;
using CustomMVC.App.Core.Middleware;
using CustomMVC.App.Core.Routing;
using CustomMVC.App.Core.Routing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Application.Extensions
{
    public static class WebApplicationMapMethods
    {
        public static void MapGet(this WebApplication app, string pattern, RequestDelegate handler)
        {
            var enpointBuilder = new RouteEndpointBuilder(pattern, handler, 1);

            enpointBuilder.Metadata.Add(new HttpGet());

            app.WebAppBuilder.defaultEndpointDataSource.Add(enpointBuilder);
        }

        public static void MapPost(this WebApplication app, string pattern, RequestDelegate handler)
        {
            var enpointBuilder = new RouteEndpointBuilder(pattern, handler, 1);

            enpointBuilder.Metadata.Add(new HttpPost());

            app.WebAppBuilder.defaultEndpointDataSource.Add(enpointBuilder);
        }
    }
}
