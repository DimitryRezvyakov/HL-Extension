using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Options
{
    public class MediatorOptions
    {
        public Assembly[] Assemblies { get; set; } = new[] { Assembly.GetExecutingAssembly() };

        public bool ThrowIfNotFound { get; set; } = false;
    }
}
