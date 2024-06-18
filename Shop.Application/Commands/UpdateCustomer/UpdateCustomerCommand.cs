using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateCustomerCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
