using FastEndpoints;
using MediatR;
using Shop.Application.Commands.CreateOrder;
using Shop.Application.Mapperly;
using Shop.Application.Queries.GetProductById;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Order.Create
{
    public class Handler(ISender mediator) : Endpoint<Request, ServiceResponse<OrderDTO>>
    {

        public override void Configure()
        {
            Post("/api/v1/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request request,
            CancellationToken cancellationToken)
        {
            ServiceResponse<OrderDTO> result;
            try
            {
                var orderItemsCmd = new List<CreateOrderItemCommand>();
                foreach (var orderItemDto in request.OrderItems)
                {
                    var productDto = await mediator.Send(new GetProductByIdQuery(orderItemDto.ProductId));
                    orderItemsCmd.Add(new CreateOrderItemCommand(orderItemDto.ProductId, orderItemDto.Quantity, productDto.Price));
                }

                var cmd = new CreateOrderCommand(Guid.NewGuid(), request.CustomerId.Value, request.City, request.Street, orderItemsCmd);
                await mediator.Send(cmd);

                result = new ServiceResponse<OrderDTO>
                {
                    Data = OrderMapper.ToDTO(cmd),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<OrderDTO>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            await SendAsync(result);
        }
    }
}
