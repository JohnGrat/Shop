using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.EventHandlers
{
    public class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public CustomerUpdatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = CustomerMapper.ToDTO(notification);
            await _sqlDbRepository.UpdateAsync(notification.Id, customerRm);
        }
    }
}
