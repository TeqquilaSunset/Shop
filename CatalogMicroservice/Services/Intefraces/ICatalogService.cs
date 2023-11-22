using CatalogMicroservice.Model.Dtos;

namespace CatalogMicroservice.Services.Intefraces
{
    public interface ICatalogService
    {
        public Task<CatalogDto> GetCatalogAsync();
    }
}
