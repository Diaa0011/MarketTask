namespace MarketTask.Application.Dtos.CartItemDto
{
    public class CartItemInCartDto
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get;set; }
        
        public decimal TotalVat { get; set; }
        public int ShippingCost { get; set; }
        public decimal TotalPrice { get; set; }
    }
}