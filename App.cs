using Shop.Controller;
using Shop.model;
using Shop.service;

namespace Shop
{
    public class App
    {
        public static void Main(string[] args)
        {
            var connectionString = "Host=localhost;" +
                "Username=postgres;" +
                "Password=admin;" +
                "Database=Shop";

            var databaseService = new DatabaseService(connectionString);
            var isConnected = databaseService.ConnectToDatabase();
            if (isConnected)
            {
                db(databaseService);
            }
            else
            {
                local();
            }
        }
        private static void local()
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
                        var items = orderService.GetOrderItems();
                        if (items.Count == 0)
                        {
                            Console.WriteLine("The order is empty. Nothing to remove.");
                            break;
                        }
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

        private static void db(DatabaseService dbService)
        {
            var products = dbService.GetAllProducts();
            var order = new Order();
            var orderService = new OrderService(order);
            var controller = new ShopController(orderService);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n--- Database-Backed Order Management ---");
                Console.WriteLine("1. Add Product to Order");
                Console.WriteLine("2. Remove Product from Order");
                Console.WriteLine("3. View Order");
                Console.WriteLine("4. View Totals");
                Console.WriteLine("5. Add Product to Database");
                Console.WriteLine("6. Remove Product from Database");
                Console.WriteLine("7. Update Product in Database");
                Console.WriteLine("8. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("\nAvailable Products in Database:");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {products[i].Name}, Price: {products[i].Price:C}");
                        }
                        Console.Write("Enter the product number to add to the order: ");
                        if (int.TryParse(Console.ReadLine(), out int productIndex) &&
                            productIndex >= 1 && productIndex <= products.Count)
                        {
                            controller.AddProduct(products[productIndex - 1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid product number.");
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
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case "3":
                        controller.PrintOrder();
                        break;

                    case "4":
                        controller.PrintTotals();
                        break;

                    case "5":
                        Console.Write("Enter product name: ");
                        var name = Console.ReadLine();
                        Console.Write("Enter product price: ");
                        if (double.TryParse(Console.ReadLine(), out double price))
                        {
                            dbService.AddProductToDatabase(new Product(name, price));
                            products = dbService.GetAllProducts();
                        }
                        break;

                    case "6":
                        Console.Write("Enter the name of the product to remove: ");
                        var removeName = Console.ReadLine();
                        dbService.RemoveProductFromDatabase(removeName);
                        products = dbService.GetAllProducts();
                        break;

                    case "7":
                        Console.WriteLine("\nAvailable Products:");
                        for (int i = 0; i < products.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. ID: {i + 1}, {products[i].Name}, Price: {products[i].Price:C}");
                        }

                        Console.Write("Enter the ID of the product to update: ");
                        if (int.TryParse(Console.ReadLine(), out int productId) && productId >= 1 && productId <= products.Count)
                        {
                            Console.Write("Enter new product name: ");
                            var newName = Console.ReadLine();

                            Console.Write("Enter new product price: ");
                            if (double.TryParse(Console.ReadLine(), out double newPrice))
                            {
                                dbService.UpdateProductInDatabase(productId, newName, newPrice);
                                products = dbService.GetAllProducts();
                            }
                            else
                            {
                                Console.WriteLine("Invalid price entered!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid product ID!");
                        }
                        break;


                    case "8":
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
