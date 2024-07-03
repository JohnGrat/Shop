using Riok.Mapperly.Abstractions;
using Shop.Application.Commands.CreateProduct;
using Shop.Application.ReadModels;
using Shop.Domain.Events;
using Shop.Shared.DTOs;


namespace Shop.Application.Mapperly
{
    [Mapper]
    public static partial class ProductMapper
    {
        public static partial ProductDTO ToDTO(ProductReadModel product);
        public static partial ProductDTO ToDTO(CreateProductCommand product);
        public static partial ProductReadModel ToDTO(ProductDTO product);
        public static partial ProductReadModel ToDTO(ProductCreatedEvent product);
        public static partial ProductReadModel ToDTO(ProductUpdatedEvent product);
    }
}