using MediatR;
using Moq;
using Shop.Application.Commands.DeleteProduct;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Xunit;

namespace Shop.Tests.Application.Commands
{
    public class DeleteProductCommandTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
        private readonly Mock<IEventStoreRepository> _eventStoreRepository;

        public DeleteProductCommandTest()
        {
            _mediator = new Mock<IMediator>();
            _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
            _eventStoreRepository = new Mock<IEventStoreRepository>();
        }

        [Fact]
        public void DeleteProductCommandValidator_WhenIdIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new DeleteProductCommandValidator();
            var command = new DeleteProductCommand(Guid.Empty);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void DeleteProductCommandValidator_WhenIdIsValid_PassesValidation()
        {
            // Arrange
            var validator = new DeleteProductCommandValidator();
            var command = new DeleteProductCommand(Guid.NewGuid());

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}

