using CustomMVC.App.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing
{
    public class Route
    {
        public string Pattern { get;}
        public string Method { get; }
        public Func<HttpContext, Task> Handler { get; }

        public Route (string pattern, string method, Func<HttpContext, Task> handler)
        {
            Pattern = pattern;
            Method = method;
            Handler = handler;
        }
    }
}
