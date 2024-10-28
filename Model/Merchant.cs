using System.ComponentModel.DataAnnotations;
using Market.Models;
using Microsoft.AspNetCore.Identity;

namespace Market.Model
{
    public class Merchant{
        [Key]
        public int Id { get; set; }
        public User user { get; set; }
        public List<Store> Stores { get; set; } = new List<Store>();
    }
}