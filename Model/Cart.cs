using System.ComponentModel.DataAnnotations;

namespace Market.Model
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public IEnumerable<CartItem> CartItems{get;set;}
        public int TotalShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        //public User user {get;set;}
    }
}
