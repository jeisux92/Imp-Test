using Impexium.Domain.Entities;
using Impexium.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Impexium.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("{id}")]
        [ActionName("GetAsync")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _productsService.GetProductAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll() =>
           Ok(_productsService.GetAllProductsAsync());


        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            int result = await _productsService.CreateProductAsync(product);
            if (result != 0)
            {
                return CreatedAtAction(nameof(GetAsync), new { id = result }, product);
            }
            return new StatusCodeResult(500);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Product product)
        {
            await _productsService.UpdateProductAsync(id, product);
            return Ok();
        }

    }
}
