using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.model
{
    public class OrderItem
    {
        public Product Product { get; }

        public OrderItem(Product product)
        {
            Product = product;
        }
    }
}
