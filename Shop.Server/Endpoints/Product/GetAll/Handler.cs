using FastEndpoints;
using MediatR;
using Shop.Application.Queries.GetProducts;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Product.GetAll
{
    public class Handler(ISender mediator) : Endpoint<EmptyRequest, ServiceResponse<IEnumerable<ProductDTO>>>
    {
        public override void Configure()
        {
            Get("/api/v1/products");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
        {
            ServiceResponse<IEnumerable<ProductDTO>> result;
            try
            {
                var productsDto = await mediator.Send(new GetProductsQuery(), cancellationToken);
                result = new ServiceResponse<IEnumerable<ProductDTO>>
                {
                    Data = productsDto.ToList(),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<IEnumerable<ProductDTO>>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            await SendAsync(result);
        }
    }
}
