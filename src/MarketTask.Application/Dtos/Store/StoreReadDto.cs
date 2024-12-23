using MarketTask.Application.Dtos.Product;

namespace MarketTask.Application.Dtos.Store
{
    public class StoreReadDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ShippingCost { get;set; }
        public List<ProductReadDto> Products { get; set; }
    }
}