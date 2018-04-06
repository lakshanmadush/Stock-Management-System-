using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginForm.Models
{
    public class CartItem
    {
        public string Item_name { get; set; }
        public int Quantity { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal TotalAmount { get; set; }
        
    }
}
