using FastEndpoints;
using MediatR;
using Shop.Application.Commands.UpdateOrderStatus;
using Shop.Server.DataTransferContracts;

namespace Shop.Server.Endpoints.Order.UpdateOrderStatus
{
    public class Handler(ISender mediator) : Endpoint<Request, ServiceResponse<EmptyResponse>>
    {
        public override void Configure()
        {
            Patch("/api/v1/orders/{id}/status");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request, CancellationToken cancellationToken)
        {
            ServiceResponse<EmptyResponse> result;
            try
            {
                var cmd = new UpdateOrderStatusCommand(request.Id, request.OrderStatus);
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
