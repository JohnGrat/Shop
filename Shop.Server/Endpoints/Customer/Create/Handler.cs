using FastEndpoints;
using MediatR;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Queries.GetCustomers;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;


namespace Shop.Server.Endpoints.Customer.Create
{
    public class Handler(ISender mediator) : Endpoint<Request, Response>
    {

        public override void Configure()
        {
            Post("/api/v1/customers/create");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<CustomerDTO> result;
            try
            {
                var customerCmd = new CreateCustomerCommand(Guid.NewGuid(), request.Name);
                await mediator.Send(customerCmd);

                result = new ServiceResponse<CustomerDTO>
                {
                    Data = CustomerMapper.ToDTO(customerCmd),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<CustomerDTO>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            var response = new Response(result);
            await SendAsync(response);
        }
    }
}
