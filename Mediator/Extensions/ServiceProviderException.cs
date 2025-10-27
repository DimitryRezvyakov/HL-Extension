using Shared.Interfaces;
using Mediator.Interfaces;
using Mediator.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Extensions
{
    public static class ServiceProviderException
    {
        public static void UseMediator(this IServiceProviderCustom serviceProvider, Action<MediatorOptions>? opt = null)
        {
            serviceProvider.AddSingleton<IMediator, Mediator>(new object[] {opt});
        }
    }
}
