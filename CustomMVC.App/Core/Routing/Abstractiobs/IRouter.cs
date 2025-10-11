using CustomMVC.App.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Routing.Abstractiobs
{
    public interface IRouter
    {
        Task RouteAsync(HttpContext context);
    }
}
