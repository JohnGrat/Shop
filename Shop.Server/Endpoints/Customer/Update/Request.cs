using FastEndpoints;
using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Endpoints.Customer.Update
{
    public class Request
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;
    }
}
