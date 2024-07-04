using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Product
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }       
        public decimal Price { get; set; }
        public decimal? VAT { get; set; }
        public int StoreId { get; set; }

    }
}