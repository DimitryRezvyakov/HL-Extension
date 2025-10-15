using CustomMVC.App.Core.Routing;
using CustomMVC.App.DependencyInjection;
using CustomMVC.App.Hosting.Abstractions;
using CustomMVC.App.Hosting.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Application
{
    public class WebApplicationBuilder
    {
        public readonly HostBuilder hostOptionsBuilder = new();
        public readonly WebApplicationPipelineBuilder pipelineBuilder = new();
        public readonly DefaultEndpointDataSource defaultEndpointDataSource = DefaultEndpointDataSource.Instance;

        public WebApplication Build()
        {
            var app = new WebApplication(hostOptionsBuilder.Build(), this);

            app.endpointDataSources.Add(defaultEndpointDataSource);

            return app;
        }
    }
}
