namespace Shop.Application.ReadModels
{
    public class ProductReadModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public decimal Price { get; set; }
    }
}
