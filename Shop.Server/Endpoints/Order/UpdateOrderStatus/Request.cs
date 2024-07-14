using Shop.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Endpoints.Order.UpdateOrderStatus
{
    public class Request
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
