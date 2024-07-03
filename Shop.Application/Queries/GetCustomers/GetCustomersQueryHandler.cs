using MediatR;
using Shop.Application.Mapperly;
using Shop.Application.ReadModels;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.DTOs;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public GetCustomersQueryHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var customersRm = await _sqlDbRepository.GetAllAsync<CustomerReadModel>();
            var customersDto = customersRm.Select(CustomerMapper.ToDTO);

            return customersDto;
        }
    }
}


