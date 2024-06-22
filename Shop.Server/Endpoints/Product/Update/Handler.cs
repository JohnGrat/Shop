using FastEndpoints;
using MediatR;
using Shop.Application.Commands.UpdateProduct;
using Shop.Server.DataTransferContracts;

namespace Shop.Server.Endpoints.Product.Update
{
    public class Handler(ISender mediator) : Endpoint<Request, ServiceResponse<EmptyResponse>>
    {

        public override void Configure()
        {
            Put("/api/v1/products/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<EmptyResponse> result;
            try
            {
                var cmd = new UpdateProductCommand(request.Id, request.Name, request.Price);
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
