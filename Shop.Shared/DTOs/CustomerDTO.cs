using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Shared.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
