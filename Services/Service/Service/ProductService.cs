using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Product;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Service.IService;

namespace Market.Services.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly string userId;


        public ProductService(IUnitOfWork unitOfWork, IMapper mapper,IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
                
            //userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
        }

        public async Task<IEnumerable<ProductWithStoreReadDto>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();
            
            var Readedproducts =  _mapper.Map<IEnumerable<ProductWithStoreReadDto>>(products);
            
            return Readedproducts;
        }

        public async Task<ProductWithStoreReadDto> GetProductById(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productId);
            
            var Readedproduct =  _mapper.Map<ProductWithStoreReadDto>(product);
            
            return Readedproduct;
        }
        
        public async Task<ProductReadDto> CreateProduct(ProductCreateDto productDto)
        {
            var store = _unitOfWork.Stores.GetById(productDto.StoreId);

            if (store is null)
            {
                Console.WriteLine("--->Invalid Store");

                if (productDto is null)
                {
                    return null;
                }
            }

            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            var productRead = _mapper.Map<ProductReadDto>(product);

            return productRead;
        }
        

        public async Task<ProductReadDto> UpdateProduct(int id,[FromBody]JsonPatchDocument<ProductEditDto> patchDoc)
        {   
            // var store = _unitOfWork.Stores.GetById(productDto.StoreId);
            var product = await _unitOfWork.Products.GetProductByIdAsync(id);

            if (product is null)
            {
                return null;
            }

            var productToPatch = _mapper.Map<ProductEditDto>(product);

            patchDoc.ApplyTo(productToPatch);

            _mapper.Map(productToPatch, product);

            await _unitOfWork.SaveAsync();

            var productRead = _mapper.Map<ProductReadDto>(product);

            return productRead;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productId);

            if (product is null)
            {
                return false;
            }

            _unitOfWork.Products.Delete(product);
            
            await _unitOfWork.SaveAsync();

            return true;
        }

        
    }
}