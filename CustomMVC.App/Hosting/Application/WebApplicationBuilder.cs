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
        private static readonly ServiceProvider? Services = ServiceProvider.GetInstance();
        /// <summary>
        /// A web application instance
        /// </summary>
        private WebApplication? _app {  get; set; }

        /// <summary>
        /// A host builder
        /// </summary>
        private IHostBuilder hostOptionsBuilder = Services.GetService<IHostBuilder>();

        /// <summary>
        /// A web application pipeline builder
        /// </summary>
        private readonly IWebApplicationPipelineBuilder pipelineBuilder = Services.GetService<IWebApplicationPipelineBuilder>();

        /// <summary>
        /// A web application endpoint data sources
        /// </summary>
        private List<EndpointDataSource> _endpointDataSources = new List<EndpointDataSource>() { DefaultEndpointDataSource.Instance };

        public IHostBuilder Host => hostOptionsBuilder;
        public List<EndpointDataSource> Sources => _endpointDataSources;
        public IWebApplicationPipelineBuilder PipeLine => pipelineBuilder;

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
