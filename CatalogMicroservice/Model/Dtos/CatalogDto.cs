using Shop.Model.Dtos;

namespace CatalogMicroservice.Model.Dtos
{
    public class CatalogDto
    {
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }
}
