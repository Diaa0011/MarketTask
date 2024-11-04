using AutoMapper;
using Market.Dtos.Product;
using Market.Models;

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
            CreateMap<ProductEditDto, Product>();
            CreateMap<ProductToCartDto, Product>();
        }
    }
}