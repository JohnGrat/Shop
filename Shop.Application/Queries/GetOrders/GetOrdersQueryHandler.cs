

using MediatR;
using Shop.Application.Mapperly;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetOrders
{
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public GetOrdersQueryHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var ordersRm = await _sqlDbRepository.GetAllAsync<OrderReadModel>("OrderItems");

            var ordersDto = ordersRm.Select(OrderMapper.ToDTO);

            return ordersDto;
        }
    }
}
