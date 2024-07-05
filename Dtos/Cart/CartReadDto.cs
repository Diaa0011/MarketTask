using Market.Model;

namespace Market.Dtos.Cart
{
    public class CartReadDto
    {
        public int CartId { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }

        public int NumberOfItems { get { return CartItems?.Count() ?? 0; } }

        public decimal TotalAmount { get; set; }
    }
}
