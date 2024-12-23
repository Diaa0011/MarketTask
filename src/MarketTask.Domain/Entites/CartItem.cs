
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketTask.Domain.Entites
{
    public class CartItem
    {
        [Key]
        public int CartItemId {  get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalVat { get; set; }

        public decimal TotalPrice { get; set; }

        public int ShippingCost { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart cart { get; set; }

    }
}
