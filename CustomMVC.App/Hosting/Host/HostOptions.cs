using CustomMVC.App.Hosting.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Hosting.Host
{
    public enum Environment
    {
        Development, Production
    }

    public class HostOptions
    {
        public string Domain { get; set; } = "localhost";
        public string Port { get; set; } = "8888";
        public string ConnectionString { get; set; } = "http://localhost:8888/";
        public string HostName { get; set; } = Assembly.GetCallingAssembly().ToString();
        public string ApplicationName { get; set; } = "MyApp";
        public Environment ApplicationEnvironment { get; set; } = Environment.Development;
    }
}
