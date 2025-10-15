using CustomMVC.App.Common;
using CustomMVC.App.Common.Exceptions;
using CustomMVC.App.Hosting.Application;
using CustomMVC.App.Hosting.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing.Extensions
{
    public static class RouteExtensions
    {
        private readonly static Logger<WebApplication> _logger = new(); 
        public static void UseRouting(this WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                _logger.LogInfo($"Mathing {context.Request.Uri?.AbsolutePath ?? "/"}, {_logger.type}");

                var matcher = new RouteMatcher(app.endpointDataSources);

                try
                {
                    RouteEndpoint route = await matcher.MatchAsync(context);

                    _logger.LogInfo($"Sucsessfully matched {context.Request.Uri?.AbsolutePath ?? "/"}, {_logger.type}");

                    context.Endpoint = route;

                    await next();
                }
                catch (RouteNotFindException)
                {
                    context.Response.SetStatusCode(404);
                }
                catch (Exception ex)
                {
                    _logger.LogFatal($"Matching exception", ex);

                    context.Response.SetStatusCode(500);
                }
            });
        }

        public static void UseEndpoints(this WebApplication app)
        {
            app.WebAppBuilder.pipelineBuilder.EndpointHandler = async (context) =>
            {
                RouteEndpoint routeEnpoint = context.Endpoint;

                _logger.LogInfo($"Executing endpoint, {routeEnpoint}");

                var routeHandler = routeEnpoint.RequestDelegate;

                await routeHandler(context);
            };
        }
    }
}
