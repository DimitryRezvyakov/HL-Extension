using CustomMVC.App.Core.Http;
using CustomMVC.App.Core.Middleware;
using CustomMVC.App.MVC.Controllers.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Common
{
    /// <summary>
    /// Create method is request delegate that will be invoked bu UseEndpoints
    /// </summary>
    public class MVCRequestDelegateFactory
    {
        private static readonly ActionSelector _actionSelector = new();
        private static readonly ActionInvokerFactory _actionInvokerFactory = new();

        /// <summary>
        /// Creating a action invoker for this endpoint
        /// </summary>
        /// <param name="context">Http context</param>
        public static async Task Create(HttpContext context)
        {
            //selecting the best candidate
            var actionDescriptor = await _actionSelector.SelectBestCandidate(context);

            //creating action invoker
            var actionInvoker = _actionInvokerFactory.Create(context, actionDescriptor);

            await actionInvoker.Invoke(context);
        }
    }
}
