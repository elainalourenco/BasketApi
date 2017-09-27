using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataContract
{
    public class Basket
    {
        public int Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public Basket()
        {
            this.Items = new List<BasketItem>();
        }
    }
}
