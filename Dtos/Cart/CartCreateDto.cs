using Market.Model;

namespace Market.Dtos.Cart
{
    public class CartCreateDto
    {
        public decimal TotalAmount { get; set; }
        public int TotalShippingCost { get; set; }
        public DateTime CreatedAt => DateTime.Now;
        public string ClientId { get; set; }
    }
}
