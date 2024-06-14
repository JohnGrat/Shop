using Shop.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dispatchers.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync<T>(T aggregate) where T : AggregateRoot;
    }
}
