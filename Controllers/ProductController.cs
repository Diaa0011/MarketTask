using AutoMapper;
using Market.Dtos.Product;
using Market.Dtos.Store;
using Market.Models;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly IProductService _productService;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper,IProductService productService)
        {
            _unitOfWork = unitOfWork ?? 
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _productService = productService ??
                throw new ArgumentNullException(nameof(productService));
        }
        /*[HttpGet,Authorize(Roles ="user,merchant")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();

            return Ok(_mapper.Map<List<ProductReadDto>>(products));
        */
        [HttpGet("{id}",Name = "GetProductById"), Authorize(Roles = "user,merchant")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(id);

            return Ok(_mapper.Map<ProductReadDto>(product));
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

            /*var store = _unitOfWork.Stores.GetById(newProduct.StoreId);

            if (store is null)
            {
                Console.WriteLine("--->Invalid Store");

                if(newProduct is null)
                {
                    return NoContent();
                }
                
            }

            var newProduct_add = _mapper.Map<Product>(newProduct);

            newProduct_add.store = store;

            await _unitOfWork.Products.AddAsync(newProduct_add);

            await _unitOfWork.SaveAsync();

            var final_p = _unitOfWork.Products.FindByNameAsync(newProduct.Name);

            return CreatedAtAction(nameof(GetProductById), new { id = final_p.Id }, newProduct_add);*/
        }
        /*
        [HttpPatch("{id}"), Authorize(Roles = "merchant")]
        public async Task<IActionResult> updateProduct(int id,[FromBody]JsonPatchDocument<ProductEditDto> patchDoc)
        {
            

            if (patchDoc == null)
            {
                return BadRequest();
            }
  
            var product = await _unitOfWork.Products.GetByIdAsync(id);

            if (product is null)
            {
                Console.WriteLine("No Products Found");
                return NotFound();
            }

            var productToPatch = _mapper.Map<ProductEditDto>(product);

            patchDoc.ApplyTo(productToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(productToPatch, product);

            _unitOfWork.Products.Update(product);

            await _unitOfWork.SaveAsync();

            Console.WriteLine("---> Updated Successfully");
        
            return NoContent();

        }

        [HttpDelete, Authorize(Roles = "merchant")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted_product = await _unitOfWork.Products.GetByIdAsync(id);

            if(deleted_product is null)
            {
                return NotFound();
            }

            _unitOfWork.Products.Delete(deleted_product);

            await _unitOfWork.SaveAsync();

            Console.WriteLine("---> Deleted Successfully");

            return NoContent();
        }*/




    }
}
