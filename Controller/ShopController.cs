using Shop.model;
using Shop.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Controller
{
    public class ShopController
    {
        private readonly OrderService orderService;

        public ShopController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public void AddProduct(Product product)
        {
            orderService.AddProduct(product);
        }

        public void RemoveProduct(int index)
        {
            orderService.RemoveProduct(index);
        }

        public void PrintOrder()
        {
            var items = orderService.GetOrderItems();
            if (items.Count == 0)
            {
                Console.WriteLine("The order is empty.");
                return;
            }

            Console.WriteLine("Current Order:");
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Product.Name}, Price: {items[i].Product.Price:C}");
            }
        }

        public void PrintTotals()
        {
            Console.WriteLine($"Total Without Discounts: {orderService.GetTotalWithoutDiscounts():C}");
            Console.WriteLine($"Discount Amount: {orderService.GetDiscountAmount():C}");
            Console.WriteLine($"Total With Discounts: {orderService.GetTotalWithDiscounts():C}");
        }
    }
}
