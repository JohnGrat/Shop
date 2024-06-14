using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Endpoints.Customer.Create
{
    public class Request
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
