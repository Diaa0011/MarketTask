using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? VAT { get; set; }

        [Required]
        public int StoreId { get; set; }

    }
}