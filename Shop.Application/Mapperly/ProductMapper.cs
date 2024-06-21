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
        public static partial ProductDTO ToDTO(ProductReadModel customer);
        public static partial ProductDTO ToDTO(CreateProductCommand customer);
        public static partial ProductReadModel ToDTO(ProductDTO customer);
        public static partial ProductReadModel ToDTO(ProductCreatedEvent customer);
        public static partial ProductReadModel ToDTO(ProductUpdatedEvent customer);
    }
}
