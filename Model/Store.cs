using System.ComponentModel.DataAnnotations;
using Market.Model;

namespace Market.Models
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Address { get; set; }
        public decimal VATPercent { get; set; }
        public int ShippingCost { get; set; }
        public int merchantId { get; set; }
        public Merchant Merchant { get; set; }  
        public List<Product> Products { get; set; } = new List<Product>();
    }
}