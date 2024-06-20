using MediatR;
using Moq;
using Shop.Application.Commands.CreateCustomer;
using Shop.Domain.Aggregates.CustomerAggregate;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Xunit;

namespace Shop.Tests.Application.Commands.CreateCustomer
{
    public class CreateCustomerCommandTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IDomainEventDispatcher> _domainEventDispatcher;
        private readonly Mock<IEventStoreRepository> _eventStoreRepository;

        public CreateCustomerCommandTest()
        {
            _mediator = new Mock<IMediator>();
            _domainEventDispatcher = new Mock<IDomainEventDispatcher>();
            _eventStoreRepository = new Mock<IEventStoreRepository>();
        }

        [Fact]
        public async Task Handle_ValidCommand_SavesCustomerAndDispatchesEvents()
        {
            // Arrange
            var command = new CreateCustomerCommand(Guid.NewGuid(), "John");
            var handler = new CreateCustomerCommandHandler(_domainEventDispatcher.Object, _eventStoreRepository.Object);

            // Act
            await handler.Handle(command, new CancellationToken());

            // Assert
            _eventStoreRepository.Verify(repo => repo.SaveAsync(It.IsAny<Customer>()), Times.Once);
            _domainEventDispatcher.Verify(dispatcher => dispatcher.DispatchEventsAsync(It.IsAny<Customer>()), Times.Once);
        }


        [Fact]
        public void CreateCustomerCommandValidator_WhenNameIsValid_PassesValidation()
        {
            // Arrange
            var validator = new CreateCustomerCommandValidator();
            var command = new CreateCustomerCommand(Guid.NewGuid(), "John Doe");

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void CreateCustomerCommandValidator_WhenNameIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new CreateCustomerCommandValidator();
            var command = new CreateCustomerCommand(Guid.NewGuid(), "");

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void CreateCustomerCommandValidator_WhenNameExceedsMaxLength_FailsValidation()
        {
            // Arrange
            var validator = new CreateCustomerCommandValidator();
            var command = new CreateCustomerCommand(Guid.NewGuid(), new string('a', 201)); // Name length is 201, exceeding the maximum length of 200

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }
    }
}
