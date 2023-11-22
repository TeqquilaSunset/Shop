namespace Shop.Model.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public BrandDto Brand { get; set; }
        public CategoryDto Category { get; set; }
    }
}
