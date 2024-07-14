using FluentValidation;
using Shop.Shared.Enums;

namespace Shop.Application.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.OrderStatus)
                .Must(EnumIsDefined);
        }

        private bool EnumIsDefined(OrderStatus orderStatus)
        {
            bool enumIsDefined = Enum.IsDefined(typeof(Domain.Aggregates.OrderAggregate.OrderStatus), (int)orderStatus);
            return enumIsDefined;
        }
    }
}
