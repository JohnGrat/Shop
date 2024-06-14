using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ReadModels
{
    public class CustomerReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
