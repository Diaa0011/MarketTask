using System.Security.Claims;
using MarketTask.Application.Dtos.CartItemDto;
using MarketTask.Application.Services.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketTask.Presentation.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="client")]
    public class CartController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly ICartService _cartService;

        public CartController(
        ICartItemService cartItemService,
        ICartService cartService)
        {
            _cartItemService = cartItemService ??
                throw new ArgumentNullException(nameof(cartItemService));
            _cartService = cartService ??
                throw new ArgumentNullException(nameof(cartService));
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(CartItemCreateDto cartItemCreateDto)
        {
            
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {
                return Unauthorized("Client ID not found");
            }
            string clientId = claim.Value;
            
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
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized("Client ID not found");
            }

            string clientId = claim.Value;

            var cart = await _cartService.GetCart(clientId);

            if (cart == null)
            {
                return NotFound("Client has no cart, try to add items to cart"); 
            }

            return Ok(cart);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromCart(CartItemRemoveDto cartItemRemoveDto)
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null)
            {
                return Unauthorized("Client ID not found");
            }
            string clientId = claim.Value;

            var result = await _cartItemService.RemoveFromCart(clientId, cartItemRemoveDto);

            if (result)
            {
                return Ok("Item removed from cart successfully");
            }
            else
            {
                return BadRequest("Failed to remove item from cart");
            }
        }

    }
}
