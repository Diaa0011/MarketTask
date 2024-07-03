using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Store
{
    public class StoreDeleteDto
    {
        [Required]
        public int Id { get; set; }
    }
}