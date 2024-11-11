using Market.Dtos.CartItemDto;
using Market.Model;

namespace Market.Dtos.Cart
{
    public class CartReadDto
    {
        public int CartId { get; set; }
        public IEnumerable<CartItemInCartDto> CartItems { get; set; }

        public int NumberOfItems { get { return CartItems?.Count() ?? 0; } }
        public int TotalShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
