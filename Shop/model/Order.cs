using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.model
{
    public class Order
    {
        public List<OrderItem> OrderItems { get; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
