namespace Market.Dtos.CartItemDto
{
    public class CartItemCreateDto
    {
        public int Quantity { get; set; }
        public decimal Price { get;set; }
        public decimal TotalPrice { get; set; }

        public decimal TotalVat { get; set; }

    }
}
