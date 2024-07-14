using MediatR;
using Shop.Application.Mapperly;
using Shop.Application.ReadModels;
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
            var customerRm = await _sqlDbRepository.GetByIdAsync<CustomerReadModel>(notification.CustomerId);

            var orderRm = OrderMapper.ToDTO(notification);

            orderRm.CustomerName = customerRm.Name;
            orderRm.City = notification.Address.City;
            orderRm.Street = notification.Address.Street;
            orderRm.OrderItems = new List<OrderItemReadModel>();

            await _sqlDbRepository.InsertAsync(orderRm);
        }
    }
}
