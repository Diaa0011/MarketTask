using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketTask.Domain.Entites
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int TotalShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItem> CartItems{get;set;} = new List<CartItem>();
        public string ClientId { get; set; }
        public Client client { get; set; }
    }
}
