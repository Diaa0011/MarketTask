using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Product
{
    public class ProductEditDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }       
        [Required]
        public decimal Price { get; set; }
        public decimal? VAT { get; set; }

    }
}