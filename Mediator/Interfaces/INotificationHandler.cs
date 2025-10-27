using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator.Interfaces
{
    public interface INotificationHandler<TNotification> where TNotification : INotification
    {
        public Task Handle(TNotification notification, CancellationToken cts);
    }
}
