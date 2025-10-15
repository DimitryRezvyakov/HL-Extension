using CustomMVC.App.Common.Abstractions;
using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Middleware;
using CustomMVC.App.Core.Routing;
using CustomMVC.App.Core.Routing.Common;
using CustomMVC.App.DependencyInjection;
using CustomMVC.App.Hosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Application
{
    public class WebApplication
    {
        //private static readonly IConfiguration _config = GetConfiguration();

        public readonly WebApplicationBuilder WebAppBuilder;
        public readonly List<EndpointDataSource> endpointDataSources = new List<EndpointDataSource>();

        private readonly IHost _host;
        private RequestDelegate? _requestDelegate;

        public static WebApplicationBuilder CreateBuilder()
        {
            return new WebApplicationBuilder();
        }

        public WebApplication(IHost host, WebApplicationBuilder builder)
        {
            WebAppBuilder = builder;
            _host = host;
        }

        public void Run()
        {
            ArgumentNullException.ThrowIfNull(WebAppBuilder);

            _requestDelegate = WebAppBuilder.pipelineBuilder.Build();

            if (_requestDelegate == null)
                throw new ArgumentNullException("Request Delegate is null");

            if (_host is null)
                throw new ArgumentNullException("You must build the application using WebApplicationBuilder before running");

            _host.RequestDelegate = _requestDelegate;

            _host.Start();

            while(_host.isListening)
            {
                Console.ReadLine();
            }
        }
    }
}
