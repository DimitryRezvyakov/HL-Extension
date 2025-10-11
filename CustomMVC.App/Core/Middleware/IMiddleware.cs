using CustomMVC.App.Core.Http;
using CustomMVC.App.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.Core.Middleware
{
    public interface IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next);
    }
}
