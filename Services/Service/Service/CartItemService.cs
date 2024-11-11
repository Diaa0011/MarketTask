using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Cart;
using Market.Dtos.CartItemDto;
using Market.Model;
using Market.Services.Repository.IRepository;
using Services.Service.IService;

namespace Market.Services.Service.Service
{
    public class CartItemService:ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private static string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;  
        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<bool> AddToCart(string clientId,CartItemCreateDto cartItemCreateDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);

            cartItem = cartItemProductLinker(cartItem);
            
            //var cartId = _unitOfWork.Carts.Find(c => c.ClientId == clientId).CartId;
            var cartSearch = _unitOfWork.Carts.Find(c => c.ClientId == clientId);

            if (cartSearch == null)
            {
                cartUpsert(null, cartItem, clientId);
                return true;
            }
            else
            {
                cartUpsert(cartSearch, cartItem, clientId);
                return true;

            }

        }

        public async Task<bool> RemoveFromCart()
        {
            throw new NotImplementedException();
        }

        private  Cart cartUpsert(Cart? cart, CartItem cartItem, string clientId)
        {
            if (cart == null)
            {
                var newcart = new CartCreateDto
                {
                    TotalAmount = cartItem.TotalPrice,
                    TotalShippingCost = cartItem.ShippingCost,
                    ClientId = clientId
                };
                cart = _mapper.Map<Cart>(newcart);
                cart.CartItems.Add(cartItem);
                _unitOfWork.Carts.Add(cart);

            }else{
                cart.TotalAmount += cartItem.TotalPrice;
                cart.TotalShippingCost += cartItem.ShippingCost;
                cart.CartItems.Add(cartItem);
                _unitOfWork.Carts.Update(cart);
            }
            _unitOfWork.SaveAsync();

            return cart;
        }

        private CartItem cartItemProductLinker(CartItem cartItem)
        {
            var product = _unitOfWork.Products.Find(q =>q.Id == cartItem.ProductId);
            var store = _unitOfWork.Stores.Find(q => q.Id == product.StoreId);
            cartItem.Product = product;
            cartItem.Price = product.Price * cartItem.Quantity;
            cartItem.TotalVat = product.VAT;
            cartItem.ShippingCost = store.ShippingCost;
            cartItem.TotalPrice = product.Price + product.VAT + cartItem.ShippingCost;
            
            return cartItem;
        }
    }
}