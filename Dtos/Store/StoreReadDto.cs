using System.ComponentModel.DataAnnotations;
using Market.Dtos.Product;

namespace Market.Dtos.Store
{
    public class StoreReadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }       
        public string Address { get; set; }

        public List<ProductReadDto> Products { get; set; }
    }
}