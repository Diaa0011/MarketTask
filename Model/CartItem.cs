using Market.Models;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Model
{
    public class CartItem
    {
        [Key]
        public int CartItemId {  get; set; }
        public int CartItemIdHelper { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalVat { get; set; }

        public decimal TotalPrice { get; set; }

        public int ShippingCost { get; set; }

        public Product product { get; set; }

        public Cart cart { get; set; }

    }
}
