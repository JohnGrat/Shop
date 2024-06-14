using FastEndpoints;
using FluentValidation;
using MediatR;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Queries.GetCustomers;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;
using System;

namespace Shop.Server.Endpoints.Customer.GetAll
{
    public class Handler(ISender mediator) : Endpoint<EmptyRequest, Response>
    {
        public override void Configure()
        {
            Get("/api/v1/customers/all");
            AllowAnonymous();
        }

        public override async Task HandleAsync(EmptyRequest request, CancellationToken cancellationToken)
        {
            ServiceResponse<IEnumerable<CustomerDTO>> result;
            try
            {
                var customersDto = await mediator.Send(new GetCustomersQuery(), cancellationToken);
                result = new ServiceResponse<IEnumerable<CustomerDTO>>
                {
                    Data = customersDto.ToList(),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                result = new ServiceResponse<IEnumerable<CustomerDTO>>
                {
                    ErrorMessage = ex.Message,
                    IsSuccess = false
                };
            }
            var response = new Response(result);
            await SendAsync(response);
        }
    }
}
