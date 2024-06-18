using FastEndpoints;
using MediatR;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Commands.UpdateCustomer;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Customer.Update
{
    public class Handler(ISender mediator) : Endpoint<Request, ServiceResponse<EmptyResponse>>
    {

        public override void Configure() 
        {
            Put("/api/v1/customers/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<EmptyResponse> result;
            try
            {
                var cmd = new UpdateCustomerCommand(request.Id, request.Name);
                await mediator.Send(cmd);

                result = new ServiceResponse<EmptyResponse>
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<EmptyResponse>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            await SendAsync(result);
        }
    }
}
