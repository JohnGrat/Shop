
using MediatR;
using Moq;
using Shop.Application.Commands.CreateOrder;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Xunit;

namespace Shop.Tests.Application.Commands
{
    namespace Shop.Tests.Application.Commands
    {
        public class CreateOrderCommandTest
        {
            private readonly Mock<IMediator> _mediator;
            private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
            private readonly Mock<IEventStoreRepository> _eventStoreRepository;

            public CreateOrderCommandTest()
            {
                _mediator = new Mock<IMediator>();
                _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
                _eventStoreRepository = new Mock<IEventStoreRepository>();
            }

            [Fact]
            public async Task Handle_ValidCommand_SavesOrderAndDispatchesEvents()
            {
                // Arrange
                var orderItems = new List<CreateOrderItemCommand>
            {
                new CreateOrderItemCommand(Guid.NewGuid(), 2, 299.99m)
            };
                var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "New York", "5th Avenue", orderItems);
                var handler = new CreateOrderCommandHandler(_domainEventDispatcher.Object, _eventStoreRepository.Object);

                // Act
                await handler.Handle(command, new CancellationToken());

                // Assert
                _eventStoreRepository.Verify(repo => repo.SaveAsync(It.IsAny<Order>()), Times.Once);
                _domainEventDispatcher.Verify(dispatcher => dispatcher.DispatchEventsAsync(It.IsAny<Order>()), Times.Once);
            }

            [Fact]
            public void CreateOrderCommandValidator_WhenDataIsValid_PassesValidation()
            {
                // Arrange
                var validator = new CreateOrderCommandValidator();
                var orderItems = new List<CreateOrderItemCommand>
            {
                new CreateOrderItemCommand(Guid.NewGuid(), 1, 100)
            };
                var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "City", "Street", orderItems);

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.True(result.IsValid);
            }

            [Fact]
            public void CreateOrderCommandValidator_WhenCityIsEmpty_FailsValidation()
            {
                // Arrange
                var validator = new CreateOrderCommandValidator();
                var orderItems = new List<CreateOrderItemCommand>
            {
                new CreateOrderItemCommand(Guid.NewGuid(), 1, 100)
            };
                var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "", "Street", orderItems);

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "City" && e.ErrorMessage.Contains("not be empty"));
            }

            [Fact]
            public void CreateOrderCommandValidator_WhenStreetExceedsMaxLength_FailsValidation()
            {
                // Arrange
                var validator = new CreateOrderCommandValidator();
                var orderItems = new List<CreateOrderItemCommand>
            {
                new CreateOrderItemCommand(Guid.NewGuid(), 1, 100)
            };
                var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "City", new string('a', 101), orderItems); // Street length is 101, exceeding the maximum length of 100

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Street");
            }

            [Fact]
            public void CreateOrderCommandValidator_WhenOrderItemsIsEmpty_FailsValidation()
            {
                // Arrange
                var validator = new CreateOrderCommandValidator();
                var command = new CreateOrderCommand(Guid.NewGuid(), Guid.NewGuid(), "City", "Street", new List<CreateOrderItemCommand>());

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "OrderItems");
            }
        }
    }
}
