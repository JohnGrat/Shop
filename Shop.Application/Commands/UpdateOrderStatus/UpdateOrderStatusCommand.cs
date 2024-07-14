using MediatR;
using Shop.Shared.Enums;

namespace Shop.Application.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public Guid Id { get; }
        public OrderStatus OrderStatus { get; }

        public UpdateOrderStatusCommand(Guid id, OrderStatus orderStatus)
        {
            Id = id;
            OrderStatus = orderStatus;
        }
    }
}
