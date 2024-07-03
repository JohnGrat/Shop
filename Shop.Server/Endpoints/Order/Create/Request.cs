using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Endpoints.Order.Create
{
    public class Request
    {
        [Display(Name = "Customer")]
        [Required]
        public Guid? CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string Street { get; set; } = default!;

        public List<CreateOrderItem> OrderItems { get; set; } = default!;

    }

    public class CreateOrderItem
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
