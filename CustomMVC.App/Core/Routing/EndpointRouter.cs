using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Routing.Abstractiobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing
{
    public class EndpointRouter : IRouter
    {
        private List<Route> _routes = new List<Route>();

        public void Map(string pattern, string method, Func<HttpContext, Task> handler)
        {
            _routes.Add(new Route(pattern, method, handler));
        }

        public async Task RouteAsync(HttpContext context)
        {

            var request = context.Request;
            string httpMethod = context.Request.Method;

            Route? route = _routes
                .Where(r => r.Method.Equals(httpMethod, comparisonType: StringComparison.OrdinalIgnoreCase))
                .Where(r => r.Pattern == (request?.Uri?.AbsolutePath ?? ""))
                .FirstOrDefault();

            if (route == null)
            {
                context.Response.SetStatusCode(404);
                return;
            }

            await route.Handler(context);
        }
    }
}
