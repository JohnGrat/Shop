

using MediatR;
using Moq;
using Shop.Application.Commands.UpdateOrderStatus;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Shop.Shared.Enums;
using Xunit;

namespace Shop.Tests.Application.Commands
{
    public class UpdateOrderStatusCommandTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
        private readonly Mock<IEventStoreRepository> _eventStoreRepository;

        public UpdateOrderStatusCommandTest()
        {
            _mediator = new Mock<IMediator>();
            _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
            _eventStoreRepository = new Mock<IEventStoreRepository>();
        }

        [Fact]
        public void UpdateOrderStatusCommandValidator_WhenIdIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new UpdateOrderStatusCommandValidator();
            var command = new UpdateOrderStatusCommand(Guid.Empty, OrderStatus.New);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void UpdateOrderStatusCommandValidator_WhenOrderStatusIsInvalid_FailsValidation()
        {
            // Arrange
            var validator = new UpdateOrderStatusCommandValidator();
            // Assuming -1 is an invalid OrderStatus value
            var command = new UpdateOrderStatusCommand(Guid.NewGuid(), (OrderStatus)(-1));

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "OrderStatus");
        }

        [Fact]
        public void UpdateOrderStatusCommandValidator_WhenValidCommand_PassesValidation()
        {
            // Arrange
            var validator = new UpdateOrderStatusCommandValidator();
            var command = new UpdateOrderStatusCommand(Guid.NewGuid(), OrderStatus.Paid);

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }
    }
}
