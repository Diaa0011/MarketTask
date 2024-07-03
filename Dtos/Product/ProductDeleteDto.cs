using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Product
{
    public class ProductDeleteDto
    {
        [Required]
        public int Id { get; set; }
    }
}