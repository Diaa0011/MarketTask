using System.ComponentModel.DataAnnotations;

namespace MarketTask.Domain.Entites
{
    public class Client:User{
        public string ClientAddress { get; set; }

        public string ClientCity { get; set; }

        public string ClientState { get; set; }

        public Cart cart { get; set; }
    }
}