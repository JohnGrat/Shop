﻿using FastEndpoints;
using MediatR;
using Shop.Application.Queries.GetCustomers;
using Shop.Server.DataTransferContracts;
using Shop.Shared.DTOs;

namespace Shop.Server.Endpoints.Customer.GetAll
{
    public class Handler(ISender mediator) : Endpoint<EmptyRequest, ServiceResponse<IEnumerable<CustomerDTO>>>
    {
        public override void Configure()
        {
            Get("/api/v1/customers");
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
            await SendAsync(result);
        }
    }
}
