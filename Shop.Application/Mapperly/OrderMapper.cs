using Riok.Mapperly.Abstractions;
using Shop.Application.Commands.CreateOrder;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Shared.DTOs;

namespace Shop.Application.Mapperly
{

    [Mapper]
    public static partial class OrderMapper
    {
        public static partial OrderDTO ToDTO(CreateOrderCommand order);
        public static partial OrderReadModel ToDTO(OrderCreatedEvent order);
    }
}
