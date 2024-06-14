using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Events
{
    public class CustomerChangedEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }

        public CustomerChangedEvent(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
