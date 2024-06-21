using MediatR;
using Moq;
using Shop.Application.Commands.CreateProduct;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Xunit;

namespace Shop.Tests.Application.Commands
{
        public class CreateProductCommandTest
        {
            private readonly Mock<IMediator> _mediator;
            private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
            private readonly Mock<IEventStoreRepository> _eventStoreRepository;

            public CreateProductCommandTest()
            {
                _mediator = new Mock<IMediator>();
                _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
                _eventStoreRepository = new Mock<IEventStoreRepository>();
            }

            [Fact]
            public async Task Handle_ValidCommand_SavesProductAndDispatchesEvents()
            {
                // Arrange
                var command = new CreateProductCommand(Guid.NewGuid(), "Laptop", 999.99m);
                var handler = new CreateProductCommandHandler(_domainEventDispatcher.Object, _eventStoreRepository.Object);

                // Act
                await handler.Handle(command, new CancellationToken());

                // Assert
                _eventStoreRepository.Verify(repo => repo.SaveAsync(It.IsAny<Product>()), Times.Once);
                _domainEventDispatcher.Verify(dispatcher => dispatcher.DispatchEventsAsync(It.IsAny<Product>()), Times.Once);
            }

            [Fact]
            public void CreateProductCommandValidator_WhenNameIsValid_PassesValidation()
            {
                // Arrange
                var validator = new CreateProductCommandValidator();
                var command = new CreateProductCommand(Guid.NewGuid(), "Smartphone", 500);

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.True(result.IsValid);
            }

            [Fact]
            public void CreateProductCommandValidator_WhenNameIsEmpty_FailsValidation()
            {
                // Arrange
                var validator = new CreateProductCommandValidator();
                var command = new CreateProductCommand(Guid.NewGuid(), "", 500);

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("not be empty"));
            }

            [Fact]
            public void CreateProductCommandValidator_WhenNameExceedsMaxLength_FailsValidation()
            {
                // Arrange
                var validator = new CreateProductCommandValidator();
                var command = new CreateProductCommand(Guid.NewGuid(), new string('a', 201), 500); // Name length is 201, exceeding the maximum length of 200

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Name");
            }

            [Fact]
            public void CreateProductCommandValidator_WhenPriceIsOutOfRange_FailsValidation()
            {
                // Arrange
                var validator = new CreateProductCommandValidator();
                var command = new CreateProductCommand(Guid.NewGuid(), "Smartphone", 0); // Price is out of the valid range

                // Act
                var result = validator.Validate(command);

                // Assert
                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Price");
            }
        }
}
