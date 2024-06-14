using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Customer.GetAll
{
    public record Response(ServiceResponse<IEnumerable<CustomerDTO>> customers);
}
