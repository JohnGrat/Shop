using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Customer.Create
{
    public record Response(ServiceResponse<CustomerDTO> customers);
}
