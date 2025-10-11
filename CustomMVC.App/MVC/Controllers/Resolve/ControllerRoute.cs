using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Resolve
{
    public class ControllerRoute
    {
        public string Name { get; set; } = "default";
        public string Pattern { get; set; } = "/";

        public object[]? Defaults { get; set; }  = new[] { controller = "Home", action = "Index"};
    }
}
