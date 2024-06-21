using MediatR;
using Shop.Application.Mapperly;
using Shop.Domain.Events;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.EventHandlers
{
    public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        private readonly ISqlDbRepository _sqlDbRepository;

        public ProductUpdatedEventHandler(
            ISqlDbRepository sqlDbRepository)
        {
            _sqlDbRepository = sqlDbRepository;
        }

        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var productRm = ProductMapper.ToDTO(notification);
            await _sqlDbRepository.UpdateAsync(notification.Id, productRm);
        }
    }
}
