﻿using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Endpoints.Product.Create
{
    public class Request
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = default!;

        [Range(1, 1000)]
        public decimal Price { get; set; }
    }
}
