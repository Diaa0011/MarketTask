using System.ComponentModel.DataAnnotations;

namespace Market.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }       

        public decimal VAT { get; set; }

        public int StoreId { get; set; }
        public Store store { get; set; }

    }
}