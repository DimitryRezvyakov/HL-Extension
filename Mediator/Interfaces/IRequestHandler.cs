using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Interfaces
{
    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>, new()
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cts);
    }

    public interface IRequestHandler<TRequest> where TRequest : IRequest, new()
    {
        public Task Handle(TRequest request, CancellationToken cts);
    }
}
