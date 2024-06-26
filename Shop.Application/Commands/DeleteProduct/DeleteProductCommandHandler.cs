﻿using MediatR;
using Shop.Application.Exceptions;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;

namespace Shop.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        private readonly IEventStoreRepository _eventStoreRepository;

        public DeleteProductCommandHandler(
            IDomainEventDispatcher domainEventDispatcher,
            IEventStoreRepository eventStoreRepository)
        {
            _domainEventDispatcher = domainEventDispatcher;
            _eventStoreRepository = eventStoreRepository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _eventStoreRepository.LoadAsync<Product>(request.Id);
            if (product == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }
            product.Delete();
            await _eventStoreRepository.SaveAsync(product);
            await _domainEventDispatcher.DispatchEventsAsync(product);
        }
    }
}
