using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MarketTask.Domain.Entites
{
    public class Merchant:User{

        public List<Store> Stores { get; set; } = new List<Store>();

    }
}