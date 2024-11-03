using System.ComponentModel.DataAnnotations;

namespace Market.Dtos.Store
{
    //records better for DTOs which has chanageable data such as edit/upate
    public class StoreEditDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal VATPercent { get; set; }
        public int ShippingCost { get; set; }     
        
    }
}