using System.ComponentModel.DataAnnotations;
using Market.Model;

namespace Model.Client
{
    public class Client{
        [Key]
        public int Id { get; set; }
        public User user { get; set; }
    }
}