using MediatR;

namespace Shop.Domain.Events
{
    public class ProductUpdatedEvent : INotification
    {
        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public ProductUpdatedEvent(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}
