using AutoMapper;
using Market.Dtos.Product;
using Market.Model;

namespace Market
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Source -> Target
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductEditDto>();
            CreateMap<ProductEditDto, Product>();
            CreateMap<Product,ProductWithStoreReadDto>();
            CreateMap<ProductToCartDto, Product>();
        }
    }
}