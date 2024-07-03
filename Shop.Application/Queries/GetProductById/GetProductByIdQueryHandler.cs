using MediatR;
using Shop.Application.Exceptions;
using Shop.Application.Mapperly;
using Shop.Application.ReadModels;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public GetProductByIdQueryHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }
        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productRm = await _sqlDbRepository.GetByIdAsync<ProductReadModel>(request.Id);
            if (productRm == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            var productsDto = ProductMapper.ToDTO(productRm);
            return productsDto;
        }
    }
}
