using System.ComponentModel.DataAnnotations;
using Market.Model;

namespace Model.Client
{
    public class Client:User{
        public string ClientAddress { get; set; }

        public string ClientCity { get; set; }

        public string ClientState { get; set; }

        public Cart cart { get; set; }
    }
}