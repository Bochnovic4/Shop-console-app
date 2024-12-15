using Shop.model;

namespace Shop.service
{
    public class OrderService
    {
        private readonly Order order;

        public OrderService(Order order)
        {
            this.order = order;
        }

        public void AddProduct(Product product)
        {
            order.OrderItems.Add(new OrderItem(product));
            Console.WriteLine($"Added: {product.Name}, Price: {product.Price:C}");
        }

        public void RemoveProduct(int index)
        {
            if (index < 1 || index > order.OrderItems.Count)
            {
                Console.WriteLine("Invalid product number!");
                return;
            }

            Console.WriteLine($"Removed: {order.OrderItems[index - 1].Product.Name}");
            order.OrderItems.RemoveAt(index - 1);
        }

        public double GetTotalWithoutDiscounts()
        {
            return order.OrderItems.Sum(item => item.Product.Price);
        }

        public double GetDiscountAmount()
        {
            double discount = 0;
            var prices = order.OrderItems.Select(item => item.Product.Price).OrderByDescending(price => price).ToList();
            
            if (prices.Count == 2)
            {
                discount += prices[1] * 0.1;
            }

            if (prices.Count >= 3)
            {
                discount = prices[2] * 0.2;
            }

            double totalWithoutDiscounts = GetTotalWithoutDiscounts();
            if (totalWithoutDiscounts > 5000)
            {
                discount += totalWithoutDiscounts * 0.05;
            }

            return discount;
        }

        public double GetTotalWithDiscounts()
        {
            return GetTotalWithoutDiscounts() - GetDiscountAmount();
        }

        public List<OrderItem> GetOrderItems()
        {
            return order.OrderItems;
        }

    }
}
