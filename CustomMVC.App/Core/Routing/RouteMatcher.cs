using CustomMVC.App.Common;
using CustomMVC.App.Common.Exceptions;
using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Http.HttpMethods.Abstractions;
using CustomMVC.App.Core.Routing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing
{
    public class RouteMatcher
    {
        private readonly Logger<RouteMatcher> _logger = new();
        private readonly List<EndpointDataSource> _sources;

        public RouteMatcher(List<EndpointDataSource> sources) 
        { 
            _sources = sources;
        }

        public async Task<RouteEndpoint> MatchAsync(HttpContext context)
        {
            _logger.LogInfo($"Trying to match {context.Request.Uri}");

            var httpMethod = context.Request.Method;
            var path = context.Request?.Uri?.AbsolutePath ?? "/";

            foreach ( var source in _sources )
            {
                foreach (var route in source.Endpoints)
                {
                    var httpAttribute = route?.Metadata?.GetMetadata<IHttpMethodMetadata>();
                    if ((httpAttribute?.Methods.Contains(httpMethod) ?? false) &&
                        route?.RoutePattern == path)
                        return route;
                }
            }

            return await Task.FromException<RouteEndpoint>(new RouteNotFindException());
        }
    }
}
