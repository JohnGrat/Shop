using MediatR;
using Shop.Application.Mapperly;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public GetProductsQueryHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var productsRm = await _sqlDbRepository.GetAllAsync<ProductReadModel>();
            var productsDto = productsRm.Select(ProductMapper.ToDTO);

            return productsDto;
        }
    }
}
