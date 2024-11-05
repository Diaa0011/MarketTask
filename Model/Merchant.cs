using System.ComponentModel.DataAnnotations;
using Market.Model;
using Market.Services.Service;
using Microsoft.AspNetCore.Identity;

namespace Market.Model
{
    public class Merchant:User{

        public List<Store> Stores { get; set; } = new List<Store>();

    }
}