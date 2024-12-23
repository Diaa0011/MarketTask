using System.ComponentModel.DataAnnotations;

namespace MarketTask.Application.Dtos.Store
{
    public class StoreCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int ShippingCost { get; set; }
        [Required]
        public decimal VATPercent { get; set; }


    }
}