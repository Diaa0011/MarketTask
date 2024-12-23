using System.Security.Claims;
using AutoMapper;

using MarketTask.Domain.Entites;
using MarketTask.Infrastructure.Repository.IRepository;
using MarketTask.Application.Dtos.CartItemDto;
using MarketTask.Application.Dtos.Cart;
using MarketTask.Application.Services.IService;

namespace MarketTask.Application.Services.Service
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

        public async Task<bool> RemoveFromCart(string clientId,CartItemRemoveDto cartItemRemoveDto)
        {
            var cartItemToRemove = cartItemRemoveDto;

            var cart = _unitOfWork.Carts.Find(c => c.ClientId == clientId);
            

            
            if(cartItemToRemove == null || cart == null)
            {
                return false;
            }  

            bool result = await CartItemRemover(cartItemToRemove, cart);

            if(result)
            {
                return true;
            }else
            {
                return false;
            }

            
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
                _unitOfWork.Carts.Add(cart);

            }else{
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
            //decimal vat  = 0;
            var cartItemData = new []
            {   new{
                Quantity = cartItem.Quantity,
                Price = product.Price*cartItem.Quantity,
                vat = product.VAT,
                TotalVat = product.Price * product.VAT * cartItem.Quantity,
                ShippingCost = store.ShippingCost * cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity + product.Price * product.VAT * cartItem.Quantity + store.ShippingCost * cartItem.Quantity
                }
            };
        if (product == null || store == null || cart == null)
        {
            return null;
        }
        var cartItems = await _unitOfWork.CartItems.FindAllAsync(q => q.CartId == cart.CartId);
        if(!cartItems.Any())
        {
            cartItem.Product = product;
            cartItem.cart = cart;
            cartItem.Price = cartItemData[0].Price;
            cartItem.Quantity = cartItemData[0].Quantity;
            cartItem.TotalVat = cartItemData[0].TotalVat;
            cartItem.ShippingCost = cartItemData[0].ShippingCost;
            cartItem.TotalPrice = cartItemData[0].TotalPrice;
            cart.TotalAmount += cartItemData[0].TotalPrice; 
            cart.TotalShippingCost += cartItemData[0].ShippingCost;
            return cartItem;
        }
        foreach(var cartSearch in cartItems)
        {
            if (cartSearch.ProductId == cartItem.ProductId)
            {
                var beforeAdd = cartSearch.TotalPrice;
                cartSearch.Quantity += cartItemData[0].Quantity;
                cartSearch.Price += cartItemData[0].Price;
                cartSearch.TotalVat += cartItemData[0].TotalVat; 
                cartSearch.TotalPrice += cartItemData[0].TotalPrice;
                cartSearch.ShippingCost += cartItemData[0].ShippingCost;
                var afterAdd = cartSearch.TotalPrice;
                var addedValue = afterAdd - beforeAdd;
                cart.TotalAmount += addedValue; 
                cart.TotalShippingCost += Convert.ToInt32(cartItemData[0].ShippingCost);
                return cartSearch;
            }
            
        }
        cartItem.Product = product;
        cartItem.cart = cart;
        cartItem.Price = cartItemData[0].Price;
        cartItem.TotalVat += cartItemData[0].TotalVat;
        cartItem.ShippingCost = cartItemData[0].ShippingCost;
        cartItem.TotalPrice = cartItemData[0].TotalPrice;
        cart.TotalAmount += cartItem.TotalPrice; 
        cart.TotalShippingCost += cartItemData[0].ShippingCost;
        return cartItem;

    }
     private async Task<bool> CartItemRemover(CartItemRemoveDto cartItem, Cart cart)
        {
            var product =  await _unitOfWork.Products.FindAsync(q =>q.Id == cartItem.ProductId);
            var store =  await _unitOfWork.Stores.FindAsync(q => q.Id == product.StoreId);
            var cartItemData = new []
            {   new{
                Quantity = cartItem.Quantity,
                Price = product.Price*cartItem.Quantity,
                vat = product.VAT,
                TotalVat = product.Price * product.VAT * cartItem.Quantity,
                ShippingCost = store.ShippingCost * cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity + product.Price * product.VAT * cartItem.Quantity + store.ShippingCost * cartItem.Quantity
                }
            };
            if (product == null || store == null || cart == null)
            {
                return false;
            }else if(cartItemData[0].Quantity < 0)
            {
                return false;
            }else if(cartItemData[0].Quantity > 0)
            {
                var cartItemReal = await _unitOfWork.CartItems.FindAsync(q => q.ProductId == cartItem.ProductId);
                var beforeRemove = cartItemReal.TotalPrice;
                cartItemReal.Quantity -= cartItemData[0].Quantity;
                cartItemReal.Price -= cartItemData[0].Price;
                cartItemReal.TotalVat -= cartItemData[0].TotalVat; 
                cartItemReal.TotalPrice -= cartItemData[0].TotalPrice;
                cartItemReal.ShippingCost -= cartItemData[0].ShippingCost;
                var afterRemove = cartItemReal.TotalPrice;
                var removedValue = beforeRemove - afterRemove;
                cart.TotalAmount -= removedValue; 
                cart.TotalShippingCost -= Convert.ToInt32(cartItemData[0].ShippingCost);
                await _unitOfWork.SaveAsync();
                var cartItems = await _unitOfWork.CartItems.FindAllAsync(q => q.CartId == cart.CartId);
                List<CartItem> cartItemsList = cartItems.ToList();
                if(cartItemReal.Quantity == 0)
                {
                    _unitOfWork.CartItems.Delete(cartItemReal);
                    cartItemsList.Remove(cartItemReal);
                    _unitOfWork.Carts.Update(cart);
                    await _unitOfWork.SaveAsync();
                }
                
                if(cartItemsList.Count == 0)
                {
                    _unitOfWork.Carts.Delete(cart);
                    await _unitOfWork.SaveAsync();
                }
                return true;
            }else{
                return false;
            }
        }
    }
}