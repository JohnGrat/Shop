using Shop.Application.Commands.UpdateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Shop.Tests.Application.Commands
{
    public class UpdateCustomerCommandTest
    {
        [Fact]
        public void UpdateCustomerCommandValidator_WhenIdIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new UpdateCustomerCommandValidator();
            var command = new UpdateCustomerCommand(Guid.Empty, "John Doe");

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Id" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void UpdateCustomerCommandValidator_WhenNameIsEmpty_FailsValidation()
        {
            // Arrange
            var validator = new UpdateCustomerCommandValidator();
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "");

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage.Contains("not be empty"));
        }

        [Fact]
        public void UpdateCustomerCommandValidator_WhenNameIsValid_PassesValidation()
        {
            // Arrange
            var validator = new UpdateCustomerCommandValidator();
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "John Doe");

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void UpdateCustomerCommandValidator_WhenNameExceedsMaxLength_FailsValidation()
        {
            // Arrange
            var validator = new UpdateCustomerCommandValidator();
            var command = new UpdateCustomerCommand(Guid.NewGuid(), new string('a', 201)); // Name length is 201, exceeding the maximum length of 200

            // Act
            var result = validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "Name");
        }
    }
}
