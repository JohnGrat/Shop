using FastEndpoints;
using MediatR;
using Shop.Application.Queries.GetOrders;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Order.GetAll
{
    public class Handler(ISender mediator) : Endpoint<EmptyRequest, ServiceResponse<IEnumerable<OrderDTO>>>
    {
        public override void Configure()
        {
            Get("/api/v1/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
        {
            ServiceResponse<IEnumerable<OrderDTO>> result;
            try
            {
                var ordersDto = await mediator.Send(new GetOrdersQuery(), cancellationToken);
                result = new ServiceResponse<IEnumerable<OrderDTO>>
                {
                    Data = ordersDto.ToList(),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<IEnumerable<OrderDTO>>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            await SendAsync(result);
        }
    }
}
