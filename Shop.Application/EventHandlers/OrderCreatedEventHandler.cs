using MediatR;
using Shop.Application.Mapperly;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public OrderCreatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = OrderMapper.ToDTO(notification);
            await _sqlDbRepository.InsertAsync(orderRm);
        }
    }
}
