using CustomMVC.App.Core.Routing;
using CustomMVC.App.Core.Routing.Common;
using CustomMVC.App.DependencyInjection;
using CustomMVC.App.Hosting.Abstractions;
using CustomMVC.App.Hosting.Host;
using CustomMVC.App.MVC.Controllers.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Application
{
    public class WebApplicationBuilder
    {
        /// <summary>
        /// A web application instance
        /// </summary>
        private WebApplication? _app {  get; set; }

        /// <summary>
        /// A host builder
        /// </summary>
        private HostBuilder hostOptionsBuilder = new HostBuilder();

        /// <summary>
        /// A web application pipeline builder
        /// </summary>
        private readonly WebApplicationPipelineBuilder pipelineBuilder = new();

        /// <summary>
        /// A web application endpoint data sources
        /// </summary>
        private List<EndpointDataSource> _endpointDataSources = new List<EndpointDataSource>() { DefaultEndpointDataSource.Instance };

        public HostBuilder Host => hostOptionsBuilder;
        public List<EndpointDataSource> Sources => _endpointDataSources;
        public WebApplicationPipelineBuilder PipeLine => pipelineBuilder;

        /// <summary>
        /// Builds a WebApplication
        /// </summary>
        /// <returns>WebApplication</returns>
        public WebApplication Build()
        {
            var app = new WebApplication(hostOptionsBuilder.Build(), this);

            _app = app;

            foreach (var endpointDataSource in _endpointDataSources)
                app.endpointDataSources.Add(endpointDataSource);

            return app;
        }

        /// <summary>
        /// Adds a new endpoint data source
        /// </summary>
        /// <param name="source"></param>
        public void AddEndpointDataSource(EndpointDataSource source)
        {
            //If web application already created we add source to web application sources
            if (_app != null)
                UpdateWebApplicationDataSources(source);

            //Instead adding it to builder
            _endpointDataSources.Add(source);
        }

        /// <summary>
        /// Add endpoint data source directly to web application sources
        /// </summary>
        /// <param name="source"></param>
        private void UpdateWebApplicationDataSources(EndpointDataSource source)
        {
            _app?.endpointDataSources.Add(source);
        }
    }
}
