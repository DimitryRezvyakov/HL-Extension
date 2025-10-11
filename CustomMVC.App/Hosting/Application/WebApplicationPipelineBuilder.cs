using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Middleware;
using CustomMVC.App.Core.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace CustomMVC.App.Hosting.Application
{
    public class WebApplicationPipelineBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new();
        private readonly EndpointRouter _router = new();

        public bool UseRouting { get; set; } = false;
        public bool UseControllers = false;


        public WebApplicationPipelineBuilder Use(Func<HttpContext, Func<Task>, Task> middleware)
        {
            _middlewares.Add(next => async ctx =>
            {
                await middleware(ctx, () => next(ctx));
            });

            return this;
        }

        public WebApplicationPipelineBuilder Run(Func<HttpContext, Task> terminalMidleware)
        {
            _middlewares.Add(next => async ctx => await terminalMidleware(ctx));

            return this;
        }

        public WebApplicationPipelineBuilder Map(string pattern, string method, Func<HttpContext, Task> handler)
        {
            _router.Map(pattern, method, handler);

            return this;
        }

        public RequestDelegate Build()
        {
            if (_middlewares.Count == 0)
                return ctx =>
                {
                    ctx.Response.SetStatusCode(404);
                    return Task.CompletedTask;
                };

            RequestDelegate component;
            if (UseRouting)
            {
                component = async ctx =>
                {
                    await _router.RouteAsync(ctx);
                };
            }
            else
            {
                component = ctx =>
                {
                    ctx.Response.SetStatusCode(404);
                    return Task.CompletedTask;
                };
            }


            for (int i = _middlewares.Count - 1; i >= 0; i--)
            {
                component = _middlewares[i](component);
            }

            return component;
        }
    }
}
