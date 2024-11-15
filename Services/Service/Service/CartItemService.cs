using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Cart;
using Market.Dtos.CartItemDto;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Services.Service.IService;

namespace Market.Services.Service.Service
{
    public class CartItemService:ICartItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<bool> AddToCart(string clientId,CartItemCreateDto cartItemCreateDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);

            
            
            //var cartId = _unitOfWork.Carts.Find(c => c.ClientId == clientId).CartId;
            var cartSearch = _unitOfWork.Carts.Find(c => c.ClientId == clientId);

            if (cartSearch == null)
            {
                var newCart = await cartUpsert(null, null, clientId);
                cartItem = await CartItemProductLinker(cartItem, newCart);
                await cartUpsert(newCart, cartItem, clientId);

                return true;
            }
            else
            {
                cartItem = await CartItemProductLinker(cartItem,cartSearch);
                await cartUpsert(cartSearch, cartItem, clientId);
                return true;

            }

        }

        public async Task<bool> RemoveFromCart()
        {
            throw new NotImplementedException();
        }

        private async Task<Cart> cartUpsert(Cart? cart, CartItem? cartItem, string clientId)
        {
            if (cart == null)
            {
                var newcart = new CartCreateDto
                {
                    ClientId = clientId
                };
                cart = _mapper.Map<Cart>(newcart);
                //cart.CartItems.Add(cartItem);
                _unitOfWork.Carts.Add(cart);

            }else{
                cart.TotalAmount += cartItem.TotalPrice;
                cart.TotalShippingCost += cartItem.ShippingCost;
                
                cart.CartItems.Add(cartItem);
                _unitOfWork.Carts.Update(cart);
            }
            await _unitOfWork.SaveAsync();

            return cart;
        }

        private async Task<CartItem> CartItemProductLinker(CartItem cartItem,Cart cart)
        {
            var product =  await _unitOfWork.Products.FindAsync(q =>q.Id == cartItem.ProductId);
            var store =  await _unitOfWork.Stores.FindAsync(q => q.Id == product.StoreId);

        if (product == null || store == null || cart == null)
        {
            return null;
        }
        var cartItems = await _unitOfWork.CartItems.FindAllAsync(q => q.CartId == cart.CartId);
        if(!cartItems.Any())
        {
            cartItem.Product = product;
            cartItem.cart = cart;
            cartItem.Price = product.Price * cartItem.Quantity;
            cartItem.TotalVat = product.VAT * cartItem.Quantity;
            cartItem.ShippingCost = store.ShippingCost;
            cartItem.TotalPrice = cartItem.Price + cartItem.TotalVat + cartItem.ShippingCost;
            return cartItem;
        }
        foreach(var cartSearch in cartItems)
        {
            if (cartSearch.ProductId == cartItem.ProductId)
            {
                cartSearch.Quantity += cartItem.Quantity;
                cartSearch.Price += (product.Price * cartItem.Quantity);
                cartSearch.TotalVat += product.VAT * cartItem.Quantity;
                cartSearch.TotalPrice += (cartSearch.Price + cartSearch.TotalVat);
                //productFound = true;
                return cartSearch;
            }
            
        }
        cartItem.Product = product;
        cartItem.cart = cart;
        cartItem.Price = product.Price * cartItem.Quantity;
        cartItem.TotalVat = product.VAT * cartItem.Quantity;
        cartItem.ShippingCost = store.ShippingCost;
        cartItem.TotalPrice = cartItem.Price + cartItem.TotalVat + cartItem.ShippingCost;
        return cartItem;

    }
    }
}