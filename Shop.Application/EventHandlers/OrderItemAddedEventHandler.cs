

using MediatR;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class OrderItemAddedEventHandler : INotificationHandler<OrderItemAddedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public OrderItemAddedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            var orderRm = await _sqlDbRepository.GetByIdAsync<OrderReadModel>(notification.OrderId, "OrderItems");

            var orderItemRm = orderRm.OrderItems.SingleOrDefault(x => x.ProductId == notification.ProductId);
            if (orderItemRm != null)
            {
                orderItemRm.Quantity += notification.Quantity;
            }
            else
            {
                var productRm = await _sqlDbRepository.GetByIdAsync<ProductReadModel>(notification.ProductId);

                orderItemRm = new OrderItemReadModel();
                orderItemRm.ProductId = notification.ProductId;
                orderItemRm.OrderId = notification.OrderId;
                orderItemRm.ProductName = productRm.Name;
                orderItemRm.Quantity = notification.Quantity;
                orderItemRm.Price = notification.Price;
                orderRm.OrderItems.Add(orderItemRm);
            }

            await _sqlDbRepository.UpdateAsync(notification.OrderId, orderRm);
        }
    }
}
