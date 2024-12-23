namespace MarketTask.Application.Dtos.CartItemDto
{
    public class CartItemReadDto
    {
        public int CartItemId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get;set; }
        
        public decimal TotalVat { get; set; }
        public int ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }

        public int cartId { get; set; }


    }
}
