using Mediator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Mediator.Providers
{
    public class NotificationHandlerProvider
    {
        private static readonly Dictionary<Type, Type> _handlers = new();

        public object? Get(Type type)
        {
            var hasHandler = _handlers.TryGetValue(type, out var handler);

            if (hasHandler)
            {
                try
                {
                    var handlerInstance = Activator.CreateInstance(handler!);
                    return handlerInstance;
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public NotificationHandlerProvider(Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                var notifications = assembly.GetTypes()
                    .Where(t => t.GetInterfaces().Contains(typeof(INotification)))
                    .ToList();

                var notificationHandlers = assembly.GetTypes()
                    .Where(
                    t => !t.IsAbstract &&
                    t.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)
                        )
                    )
                    .ToList();

                foreach (var notification in notifications)
                {
                    var handler = notificationHandlers
                        .FirstOrDefault(h => h.GetInterfaces()
                        .Any(i => i.GetGenericArguments()[0] == notification));

                    if (handler == null) return;

                    _handlers.Add(notification, handler);
                }
            }
        }
    }
}
