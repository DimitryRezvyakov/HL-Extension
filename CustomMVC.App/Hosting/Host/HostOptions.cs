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
        /// <summary>
        /// Host domain
        /// </summary>
        public string Domain { get; set; } = "localhost";

        /// <summary>
        /// Host port
        /// </summary>
        public string Port { get; set; } = "8888";

        /// <summary>
        /// Host connection string
        /// </summary>
        public string ConnectionString { get; set; } = "http://localhost:8888/";

        /// <summary>
        /// Host name
        /// </summary>
        public string HostName { get; set; } = Assembly.GetCallingAssembly().ToString();

        /// <summary>
        /// Application name
        /// </summary>
        public string ApplicationName { get; set; } = "MyApp";

        /// <summary>
        /// Application environment
        /// </summary>
        public Environment ApplicationEnvironment { get; set; } = Environment.Development;
    }
}
