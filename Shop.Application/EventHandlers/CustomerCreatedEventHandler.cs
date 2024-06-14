using MediatR;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public CustomerCreatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerRm = CustomerMapper.ToDTO(notification);
            await _sqlDbRepository.InsertAsync(customerRm);
        }
    }
}
