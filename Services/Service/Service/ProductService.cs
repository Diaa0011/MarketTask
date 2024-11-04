using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Product;
using Market.Model;
using Market.Models;
using Market.Services.Repository.IRepository;
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
            //product.storeId = store.Id;
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
            var productRead = _mapper.Map<ProductReadDto>(product);

            return productRead;
        }
/*
        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productId);

            if (product is null)
            {
                return false;
            }

            _unitOfWork.Products.DeleteProduct(product);
            await _unitOfWork.SaveAsync();

            return true;
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var products = await _unitOfWork.Products.GetAllProductsAsync();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto> GetProductById(int productId)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productId);

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> UpdateProduct(ProductEditDto productDto)
        {
            var product = await _unitOfWork.Products.GetProductByIdAsync(productDto.Id);

            if (product is null)
            {
                return null;
            }

            _mapper.Map(productDto, product);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ProductReadDto>(product);
        }*/
    }
}