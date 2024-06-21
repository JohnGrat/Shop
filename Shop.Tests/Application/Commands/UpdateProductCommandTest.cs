using MediatR;
using Moq;
using Shop.Application.Commands.UpdateProduct;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Xunit;

namespace Shop.Tests.Application.Commands
{
    public class UpdateProductCommandTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
        private readonly Mock<IEventStoreRepository> _eventStoreRepository;

        public UpdateProductCommandTest()
        {
            _mediator = new Mock<IMediator>();
            _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
            _eventStoreRepository = new Mock<IEventStoreRepository>();
        }

        [Fact]
        public void UpdateProductCommandValidator_WhenIdIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new UpdateProductCommandValidator();
            var command = new UpdateProductCommand(Guid.Empty, "Laptop", 999.99m);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void UpdateProductCommandValidator_WhenNameIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new UpdateProductCommandValidator();
            var command = new UpdateProductCommand(Guid.NewGuid(), "", 999.99m);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void UpdateProductCommandValidator_WhenNameIsValid_PassesValidation()
        {
            // Arrange
            var validator = new UpdateProductCommandValidator();
            var command = new UpdateProductCommand(Guid.NewGuid(), "Smartphone", 500);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateProductCommandValidator_WhenNameExceedsMaxLength_FailsValidation()
        {
            // Arrange
            var validator = new UpdateProductCommandValidator();
            var command = new UpdateProductCommand(Guid.NewGuid(), new string('a', 201), 500); // Name length is 201, exceeding the maximum length of 200

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }

        [Fact]
        public void UpdateProductCommandValidator_WhenPriceIsOutOfRange_FailsValidation()
        {
            // Arrange
            var validator = new UpdateProductCommandValidator();
            var command = new UpdateProductCommand(Guid.NewGuid(), "Smartphone", 0); // Price is out of the valid range

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Price");
        }
    }
}
