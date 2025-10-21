using CustomMVC.App.Core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Http
{
    public class HttpContext
    {
        public virtual HttpRequest Request { get; set; }
        public HttpResponse Response { get; }
        public RouteEndpoint Endpoint { get; set; }
        public IServiceProvider RequestServices { get; set; }
        public Dictionary<string, string> RouteParametrs { get; set; }
        public Dictionary<object, object> Items { get; } = new Dictionary<object, object>();

        public HttpContext(HttpRequest request, HttpResponse response) 
        {
            Request = request;
            Response = response;
        }

        public HttpContext()
        {

        }
    }
}
