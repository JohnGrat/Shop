using Riok.Mapperly.Abstractions;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Shared.DTOs;

namespace Shop.Application.Mapperly
{
    [Mapper]
    public static partial class CustomerMapper
    {
        public static partial CustomerDTO ToDTO(CustomerReadModel customer);
        public static partial CustomerDTO ToDTO(CreateCustomerCommand customer);
        public static partial CustomerReadModel ToDTO(CustomerDTO customer);
        public static partial CustomerReadModel ToDTO(CustomerCreatedEvent customer);
        public static partial CustomerReadModel ToDTO(CustomerUpdatedEvent customer);
    }
}