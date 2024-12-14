using Shop.Controller;
using Shop.model;
using Shop.service;

namespace Shop
{
    public class App
    {
        public static void Main(string[] args)
        {
            var products = new List<Product>
            {
                new Product("Laptop", 2500),
                new Product("Klawiatura", 120),
                new Product("Mysz", 90),
                new Product("Monitor", 1000),
                new Product("Kaczka debuggująca", 66)
            };

            var order = new Order();
            var orderService = new OrderService(order);
            var controller = new ShopController(orderService);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Order Management ---");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. View Order");
                Console.WriteLine("4. View Totals");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nAvailable Products:");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {products[i].Name}, Price: {products[i].Price:C}");
                        }

                        Console.Write("Enter the number of the product to add: ");
                        if (int.TryParse(Console.ReadLine(), out int productIndex) &&
                            productIndex >= 1 && productIndex <= products.Count)
                        {
                            controller.AddProduct(products[productIndex - 1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid product number!");
                        }
                        break;

                    case "2":
                        controller.PrintOrder();
                        Console.Write("Enter the number of the product to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int removeIndex))
                        {
                            controller.RemoveProduct(removeIndex);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;

                    case "3":
                        controller.PrintOrder();
                        break;

                    case "4":
                        controller.PrintTotals();
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Exiting the application...");
                        break;

                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

    }
}
