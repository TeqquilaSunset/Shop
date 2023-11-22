using CatalogMicroservice.Services.Intefraces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalog()
        {
            var result = await _catalogService.GetCatalogAsync();
            return Ok(result);
        }
    }
}
