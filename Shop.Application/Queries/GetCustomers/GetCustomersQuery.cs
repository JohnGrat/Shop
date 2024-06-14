using MediatR;
using Shop.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Queries.GetCustomers
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
    }
}
