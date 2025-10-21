using CustomMVC.App.Common.Exceptions;
using CustomMVC.App.Core.Http;
using CustomMVC.App.MVC.Controllers.Abstractions;
using CustomMVC.App.MVC.Controllers.Common;
using CustomMVC.App.MVC.Controllers.Common.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomMVC.App.MVC.Controllers.Routing
{
    public class ActionSelector
    {
        /// <summary>
        /// Model binder for selecting methods which binder can bind
        /// </summary>
        private readonly ModelBinder _binder = new DefaultModelBinder();

        /// <summary>
        /// Selecting best candidate for endpoint, if no one throws RouteNotFoundException
        /// </summary>
        /// <param name="context"></param>
        /// <returns>ActionDescriptor</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="RouteNotFoundException"></exception>
        public async Task<ActionDescriptor> SelectBestCandidate(HttpContext context)
        {
            //getting all action descriptors which could be invoked for this endpoint
            var candidates = context.Endpoint.Metadata.GetRequireMetadata<IReadOnlyList<ActionDescriptor>>();

            if (candidates.Count == 0 || candidates == null)
                throw new Exception();

            if (candidates.Count == 1)
                return await Task.FromResult(candidates[0]);

            var endpoint = context.Endpoint;

            //filtering by http method
            var validCandidates = candidates.Where(c => c.HttpMethods.Select(m => m.Methods.First()).Contains(context.Request.Method));

            //filtering by binder
            validCandidates = validCandidates.Where(a => _binder.CanBind(context, a)).ToArray();

            if (validCandidates.Count() == 0)
                throw new RouteNotFoundException();

            return await Task.FromResult(validCandidates.First());
        }
    }
}
