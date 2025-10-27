using Mediator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediator.Interfaces;
using System.Reflection;

namespace Mediator.Providers
{
    public class NotificationHandlerProvider
    {
        private static readonly Dictionary<Type, Type> _handlers = new();

        public object? Get(Type type)
        {
            var hasHandler = _handlers.TryGetValue(type, out var handler);

            return hasHandler ? handler : null;
        }

        public NotificationHandlerProvider(Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                var notifications = assembly.GetTypes()
                    .Where(t => t is INotification)
                    .ToList();

                var notificationHandlers = assembly.GetTypes()
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(INotificationHandler<>))
                    .ToList();

                foreach (var notification in notifications)
                {
                    var handler = notificationHandlers
                        .Where(t => t.GetGenericArguments()[0] == notification)
                        .FirstOrDefault();

                    if (handler == null) return;

                    _handlers.Add(notification, handler);
                }
            }
        }
    }
}
