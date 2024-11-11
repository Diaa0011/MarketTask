﻿using System.Security.Claims;
using AutoMapper;
using Market.Dtos.Cart;
using Market.Dtos.CartItemDto;
using Market.Dtos.Product;
using Market.Model;
using Market.Services.Repository.IRepository;
using Market.Services.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Service.IService;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="client")]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;

        public CartController(IUnitOfWork unitOfWork, IMapper mapper,
        ICartItemService cartItemService,
        ICartService cartService)
        {
           /*_unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));*/

            _cartItemService = cartItemService ??
                throw new ArgumentNullException(nameof(cartItemService));
            _cartService = cartService ??
                throw new ArgumentNullException(nameof(cartService));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartItemCreateDto cartItemCreateDto)
        {
            //var cartItem = _mapper.Map<CartItem>(cartItemCreateDto);

            //var cart = await _unitOfWork.Carts.GetCartByClientIdAsync(cartItemCreateDto.ClientId);
            string clientId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var cartItem = cartItemCreateDto;

            var result = await _cartItemService.AddToCart(clientId, cartItem);

            if (result)
            {
                return Ok("Item added to cart successfully");
            }else
            {
                return BadRequest("Failed to add item to cart");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            string clientId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var cart = await _cartService.GetCart(clientId);

            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            return Ok(cart);
        }
        
        /*
        [HttpPost]
        public async Task<IActionResult> CreateCart(CartCreateDto newCart)
        {
            var cart = _mapper.Map<Cart>(newCart);

            await _unitOfWork.Carts.AddAsync(cart);

            await _unitOfWork.SaveAsync();

            var cartReadDto = _mapper.Map<CartReadDto>(cart);

            return Ok(cartReadDto);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _unitOfWork.Carts.GetAllCartsAsync();

            var cartsShow = _mapper.Map<IEnumerable<CartReadDto>>(carts);

            return Ok(cartsShow);
        }


        
        [HttpGet("items")]
        public async Task<IActionResult> GetAllCartItems()
        {
            var cartItems = await _unitOfWork.CartItems.GetAllAsync();

            var cartItemsDto = _mapper.Map<IEnumerable<CartItemReadDto>>(cartItems);
            return Ok(cartItemsDto);
        }
        [HttpPost("{cartId}/add")]
        public async Task<IActionResult> ProductToCartUpSert(int cartId, ProductToCartDto productToCart)
        {
            var cart = await _unitOfWork.Carts.GetCartByIdAsync(cartId);

            if (cart == null)
            {
                return NotFound("---> Cart not found");
            }

            var product = await _unitOfWork.Products.GetProductByIdAsync(productToCart.Id);
            var store = await _unitOfWork.Stores.FindAsync(c => c.Id == product.store.Id);
            var cartItem = await _unitOfWork.CartItems.FindAsync(c => c.CartItemIdHelper == product.Id);
            var vatset = 0;
            if (product.VAT == 0)
            {
                vatset = (int)(product.Price * store.VATPercent);
            }
            else
            {
                vatset = (int)(product.VAT);
            }
            if (cartItem == null)
            {
                var newCartItem = new CartItemCreateDto
                {
                    CartItemIdHelper = product.Id,
                    Quantity = productToCart.Quantity,
                    Price = productToCart.Quantity * product.Price,
                    TotalVat = productToCart.Quantity * vatset,
                    ShippingCost = store.ShippingCost,
                    TotalPrice = (productToCart.Quantity * product.Price) + (productToCart.Quantity * vatset) +(store.ShippingCost)


                };
                var cartToAdd = _mapper.Map<CartItem>(newCartItem);
                cartToAdd.product = product;
                cartToAdd.cart = cart;

                await _unitOfWork.CartItems.AddAsync(cartToAdd);
            }
            else
            {

                cartItem.Quantity = productToCart.Quantity;
                cartItem.Price = productToCart.Quantity * product.Price;
                cartItem.TotalVat = productToCart.Quantity * vatset;
                cartItem.TotalPrice = (productToCart.Quantity * product.Price) + (productToCart.Quantity * vatset) + (store.ShippingCost);

                _unitOfWork.CartItems.Update(cartItem);
            }
            cart.TotalAmount = cartCalculator(cart);
            await _unitOfWork.SaveAsync();
            return Ok("Items Added/updated Successfuly");
        }

        private decimal cartCalculator(Cart cart)
        {
            decimal TotalAmount = 0;
            int TotalShipping = 0;
            var cartItems = cart.CartItems;
            foreach(var cartiItem in cartItems)
            {
                TotalShipping += cartiItem.ShippingCost;
                TotalAmount += (decimal)cartiItem.TotalPrice;
            }
            return TotalAmount;
        }
        */
    }
}
