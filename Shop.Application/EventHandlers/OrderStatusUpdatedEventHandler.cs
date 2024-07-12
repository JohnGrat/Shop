using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.Enums;

namespace Shop.Application.EventHandlers
{
    public class OrderStatusUpdatedEventHandler : INotificationHandler<OrderStatusUpdatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public OrderStatusUpdatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(OrderStatusUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _sqlDbRepository.GetByIdAsync<OrderReadModel>(notification.Id);
            orderRm.OrderStatus = (OrderStatus)notification.OrderStatus;

            await _sqlDbRepository.UpdateAsync(notification.Id, orderRm);
        }
    }
}
