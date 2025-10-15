using CustomMVC.App.Core.Middleware;
using CustomMVC.App.Core.Routing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing
{
    public class RouteEndpoint : Endpoint
    {
        public string RoutePattern { get; set; }
        public RequestDelegate RequestDelegate { get; set; }
        public int Order { get; set; }
        public RouteEndpointMetadata? Metadata { get; set; }

        public RouteEndpoint(string routePattern, RequestDelegate requestDelegate, int order, RouteEndpointMetadata? metadata = null)
        {
            ArgumentNullException.ThrowIfNull(routePattern);
            ArgumentNullException.ThrowIfNull(requestDelegate);

            RoutePattern = routePattern;
            RequestDelegate = requestDelegate;
            Order = order;
            Metadata = metadata;
        }
    }
}
