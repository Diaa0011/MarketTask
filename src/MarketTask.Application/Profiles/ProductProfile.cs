using AutoMapper;
using MarketTask.Application.Dtos.Product;
using MarketTask.Domain.Entites;

namespace MarketTask.Application.Profiles
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