using MediatR;
using Shop.Application.Mapperly;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public ProductCreatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            var productRm = ProductMapper.ToDTO(notification);
            await _sqlDbRepository.InsertAsync(productRm);
        }
    }
}
