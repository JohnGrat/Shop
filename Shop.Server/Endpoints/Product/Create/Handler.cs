using FastEndpoints;
using MediatR;
using Shop.Application.Commands.CreateProduct;
using Shop.Application.Mapperly;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Product.Create
{
    public class Handler(ISender mediator) : Endpoint<Request, ServiceResponse<ProductDTO>>
    {

        public override void Configure()
        {
            Post("/api/v1/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<ProductDTO> result;
            try
            {
                var cmd = new CreateProductCommand(Guid.NewGuid(), request.Name, request.Price);
                await mediator.Send(cmd);

                result = new ServiceResponse<ProductDTO>
                {
                    Data = ProductMapper.ToDTO(cmd),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<ProductDTO>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            await SendAsync(result);
        }
    }
}
