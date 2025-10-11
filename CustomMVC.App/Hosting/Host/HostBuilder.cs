using CustomMVC.App.Hosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Host
{
    public class HostBuilder : IHostBuilder
    {
        private readonly HostOptions _options = new();
        public HttpListenerHost Build()
        {
            return new HttpListenerHost(_options);
        }

        public IHostBuilder Configure(Action<HostOptions>? opt)
        {
            opt?.Invoke(_options);
            
            return this;
        }
    }
}
