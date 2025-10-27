using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Interfaces
{
    public interface IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct);
        public Task Send(IRequest request, CancellationToken ct);
        public Task Publish(INotification notification, CancellationToken ct);
    }
}
