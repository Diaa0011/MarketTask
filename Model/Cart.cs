using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Model
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public IEnumerable<CartItem> CartItems{get;set;}
        public int TotalShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public Client client { get; set; }
    }
}
