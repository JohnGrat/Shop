using Shop.Domain.Aggregates.OrderAggregate;

namespace Shop.Application.ReadModels
{
    public class OrderReadModel
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; } = default!;

        public string City { get; set; } = default!;

        public string Street { get; set; } = default!;

        public OrderStatus OrderStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public List<OrderItemReadModel> OrderItems { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public int TotalQuantity { get; set; }
    }

    public class OrderItemReadModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = default!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
