using Mediator.Exceptions;
using Mediator.Interfaces;
using Mediator.Options;
using Mediator.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class Mediator : IMediator
    {
        private static MediatorOptions _options { get; set; } = new();
        private static readonly RequestHandlerProvider _handlerProvider = new(_options.Assemblies);
        private static readonly NotificationHandlerProvider _notificationHandlerProvider = new(_options.Assemblies);

        public Mediator() { }

        public Mediator(Action<MediatorOptions>? opt = null)
        {
            MediatorOptions options = new();

            if (opt != null) opt(options);

            _options = options;
        }

        public async Task Publish(INotification notification, CancellationToken ct)
        {
            var handlerType = _notificationHandlerProvider.Get(notification.GetType());

            if (handlerType != null)
            {
                var handlermethod = handlerType.GetType().GetMethod("Handle");

                await (Task)handlermethod?.Invoke(handlerType, new object[] { notification, ct })!;
            }

            else
            {
                if (_options.ThrowIfNotFound)
                    throw new NotificationHandlerNotFoundException(notification.GetType());
            }

        }

        public async Task Send(IRequest request, CancellationToken ct)
        {
            var handlerType = _handlerProvider.Get(request.GetType());

            if (handlerType != null)
            {
                var handlermethod = handlerType.GetType().GetMethod("Handle");

                await (Task)handlermethod?.Invoke(handlerType, new object[] { request, ct })!;
            }

            else
            {
                if (_options.ThrowIfNotFound)
                    throw new RequestHandlerNotFoundException(request.GetType());
            }
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default)
        {
            var handlerType = _handlerProvider.Get(request.GetType());

            if (handlerType != null)
            {
                var handlermethod = handlerType.GetType().GetMethod("Handle");

                return await (Task<TResponse>)handlermethod?.Invoke(handlerType, new object[] { request, ct })!;
            }

            else
            {
                if (_options.ThrowIfNotFound)
                    throw new RequestHandlerNotFoundException(request.GetType());

                return await Task.FromResult<TResponse>(default);
            }
        }
    }
}
