using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Middleware;
using CustomMVC.App.MVC.Controllers.Common.Entities;
using CustomMVC.App.MVC.Controllers.Common.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Routing
{
    /// <summary>
    /// Creates a action invoker which will handle the endpoint
    /// </summary>
    public class ActionInvokerFactory
    {
        /// <summary>
        /// Model binder to bind action parameters
        /// </summary>
        private readonly DefaultModelBinder _binder = new();

        /// <summary>
        /// Creates a RequestDelegate that will handle an endpoint
        /// </summary>
        /// <param name="context">HttpContext for this request</param>
        /// <param name="descriptor">Action descriptor for action</param>
        /// <returns></returns>
        public RequestDelegate Create(HttpContext context, ActionDescriptor descriptor)
        {
            //binding a action parameters from request data
            var model = _binder.Bind(context, descriptor);

            //creating a controller instance
            var controller = Activator.CreateInstance(descriptor.ControllerTypeInfo) as ControllerBase;

            ArgumentNullException.ThrowIfNull(controller);

            controller.Context = context;
            controller.ModelState = model;

            return async (context) => 
            {
                descriptor.MethodInfo.Invoke(controller, model.Parameters);
                await Task.CompletedTask;
            };
        }
    }
}
