using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Store
{
    public class StoreEditDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }       
        
    }
}