namespace MarketTask.Application.Dtos.CartItemDto
{
    public class CartItemEditDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal TotalVat { get; set; }
    }
}
