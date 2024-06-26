﻿using MediatR;
using Shop.Domain.Core;
using Shop.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Aggregates.CustomerAggregate
{
    public class Customer : AggregateRoot
    {
        public string Name { get; private set; }

        public Customer() { }

        public Customer(Guid id, string name)
        {
            ApplyChange(new CustomerCreatedEvent(id, name));
        }


        public void Change(string name)
        {
            ApplyChange(new CustomerUpdatedEvent(Id, name));
        }


        protected override void When(INotification @event)
        {
            switch (@event)
            {
                case CustomerCreatedEvent e:
                    Id = e.Id;
                    Name = e.Name;
                    break;
                case CustomerUpdatedEvent e:
                    Name = e.Name;
                    break;
            }
        }
    }
}
