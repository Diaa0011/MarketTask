using AutoMapper;
using Market.Dtos.Product;
using Market.Dtos.Store;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;
using Services.Service.IService;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        /*private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;*/

        private readonly IProductService _productService;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper,IProductService productService)
        {
            /*_unitOfWork = unitOfWork ?? 
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));*/
            _productService = productService ??
                throw new ArgumentNullException(nameof(productService));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            //var products = await _unitOfWork.Products.GetAllProductsAsync();
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        [HttpGet("id/{id}",Name = "GetProductById"), Authorize(Roles = "user,merchant")]
        public async Task<IActionResult> GetProductById(int id)
        {
            //var product = await _unitOfWork.Products.GetProductByIdAsync(id);
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
