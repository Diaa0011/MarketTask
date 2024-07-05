using AutoMapper;
using Market.Dtos.Cart;
using Market.Dtos.CartItemDto;
using Market.Dtos.Product;
using Market.Model;
using Market.Services.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

        }

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

            var cartsShow = _mapper.Map<List<CartReadDto>>(carts);

            return Ok(cartsShow);
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(int cartId)
        {
            var cart = await _unitOfWork.Carts.GetByIdAsync(cartId);

            var cartShow = _mapper.Map<CartReadDto>(cart);
            return Ok(cartShow);
        }

        [HttpPost("{cartId}/add")]
        public async Task<IActionResult> AddProductToCart(int cartId, ProductToCartDto productToCart)
        {
            var cart = await _unitOfWork.Carts.GetCartByIdAsync(cartId);

            if (cart == null)
            {
                return NotFound("---> Cart not found");
            }

            var product = await _unitOfWork.Products.GetProductByIdAsync(productToCart.Id);
            var store = await _unitOfWork.Stores.FindAsync(c => c.Id == product.store.Id);
            if (_unitOfWork.Carts.HasCartItems(cartId))
            {
                var cartItem = await _unitOfWork.CartItems.FindAsync(c => c.product.Id == product.Id);
                if (product.VAT == 0)
                {
                    product.VAT = product.Price * store.VATPercent;
                }
                if (cartItem == null)
                {
                    
                    var newCartItem = new CartItemCreateDto
                    {
                        Quantity = productToCart.Quantity,
                        Price = productToCart.Quantity * product.Price,
                        TotalVat = productToCart.Quantity * product.VAT,
                        TotalPrice = (productToCart.Quantity * product.Price)+(productToCart.Quantity * product.VAT)


                    };
                    var cartToAdd = _mapper.Map<CartItem>(newCartItem);
                    cartToAdd.product = product;
                    cartToAdd.cart = cart;
                    await _unitOfWork.CartItems.AddAsync(cartToAdd);

                }
                else
                {
                    if (cartItem.Quantity != productToCart.Quantity)
                    {
                        cartItem.Quantity = productToCart.Quantity;
                        cartItem.Price = productToCart.Quantity * product.Price;
                        cartItem.TotalVat = productToCart.Quantity * product.VAT;
                        cartItem.TotalPrice = (productToCart.Quantity * product.Price) + (productToCart.Quantity * product.VAT);
                    }
                }
            }
            else
            {
                
                var newCartItem = new CartItemCreateDto
                {
                    Quantity = productToCart.Quantity,
                    Price = productToCart.Quantity * product.Price,
                    TotalVat = productToCart.Quantity * product.VAT,
                    TotalPrice = (productToCart.Quantity * product.Price) + (productToCart.Quantity * product.VAT)
                };
                var cartToAdd = _mapper.Map<CartItem>(newCartItem);
                cartToAdd.product = product;
                cartToAdd.cart = cart;
                await _unitOfWork.CartItems.AddAsync(cartToAdd);

            }

            await _unitOfWork.SaveAsync();
            return Ok("Items Addes Successfuly");
        }

    }
}
