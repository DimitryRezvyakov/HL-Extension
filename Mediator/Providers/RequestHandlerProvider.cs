using Mediator.Interfaces;
using Mediator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Providers
{
    public class RequestHandlerProvider
    {
        private readonly Dictionary<Type, Type> _handlers = new Dictionary<Type, Type>();

        public object? Get(Type request) 
        {
            var hasHandler = _handlers.TryGetValue(request, out var handler);

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

        public RequestHandlerProvider(Assembly[] assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                var requests = assembly.GetTypes()
                    .Where(t => t.IsGenericType && 
                    (t.GetGenericTypeDefinition() == typeof(IRequest<>) ||
                    t.GetGenericTypeDefinition() ==  typeof(IRequest)))
                    .ToList();

                var handlers = assembly.GetTypes()
                    .Where(t => t.IsGenericType &&
                    (t.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                    t.GetGenericTypeDefinition() == typeof(IRequestHandler<>)))
                    .ToList();

                foreach (var requset in requests)
                {
                    var handler = handlers
                        .FirstOrDefault(h => h.GetInterfaces()
                        .Any(i => i.GetGenericArguments()[0] == requset));

                    if (handler != null)
                    {
                        _handlers.Add(requset, handler);
                    }
                }
            }
        }
    }
}
