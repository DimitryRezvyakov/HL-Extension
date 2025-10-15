using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing.Common
{
    public abstract class EndpointBuilder
    {
        public List<object> Metadata { get; set; } = new();

        public abstract Endpoint Build();
    }
}
