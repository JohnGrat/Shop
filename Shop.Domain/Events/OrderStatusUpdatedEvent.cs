
using MediatR;
using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Domain.Events
{
    public class OrderStatusUpdatedEvent : INotification
    {
        public Guid Id { get; }
        public OrderStatus OrderStatus { get; }

        public OrderStatusUpdatedEvent(Guid id, OrderStatus orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
        }
    }
}
