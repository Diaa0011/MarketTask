using MarketTask.Application.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using MarketTask.Application.Dtos.Product;

namespace MarketTask.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ??
                throw new ArgumentNullException(nameof(productService));
        }
        [HttpGet]
        // [ProducesResponseType(200,Type=typeof(IEnumerable<ProductWithStoreReadDto>))] --> for documentation
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("id/{id}",Name = "GetProductById"), Authorize(Roles = "user,merchant")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }
        [HttpPost, Authorize(Roles = "merchant")]
        public async Task<IActionResult> createProduct(ProductCreateDto newProduct)
        {

            if (newProduct is null)
            {
                return BadRequest();
            }

            var createdProduct = await _productService.CreateProduct(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, newProduct);
        }
        
        [HttpPatch("id/{id}"), Authorize(Roles = "merchant")]
        public async Task<IActionResult> updateProduct(int id,[FromBody]JsonPatchDocument<ProductEditDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var updatedProduct = await _productService.UpdateProduct(id,patchDoc);
            if (updatedProduct is null)
            {
                return NotFound();
            }
            return NoContent();
        }
            


        [HttpDelete("id/{id}"), Authorize(Roles = "merchant")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _productService.GetProductById(id);

            if(productToDelete is null)
            {
                return NotFound();
            }
            await _productService.DeleteProduct(id);

            Console.WriteLine("---> Product Deleted Successfully [Controller Check]");

            return NoContent();
        }




    }
}
