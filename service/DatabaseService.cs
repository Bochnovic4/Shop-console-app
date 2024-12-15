using Npgsql;
using Shop.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.service
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool ConnectToDatabase()
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = "SELECT name, price FROM product;";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product(reader.GetString(0), reader.GetDouble(1)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
            }
            return products;
        }

        public void AddProductToDatabase(Product product)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = "INSERT INTO product (name, price) VALUES (@name, @price);";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("name", product.Name);
                        cmd.Parameters.AddWithValue("price", product.Price);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine($"Product '{product.Name}' added to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product to database: {ex.Message}");
            }
        }

        public void RemoveProductFromDatabase(string productName)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = "DELETE FROM product WHERE name = @name;";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("name", productName);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Product '{productName}' removed from the database.");
                        else
                            Console.WriteLine($"No product named '{productName}' found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing product: {ex.Message}");
            }
        }

        public void UpdateProductInDatabase(int productId, string newName, double newPrice)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    var sql = "UPDATE product SET name = @newName, price = @newPrice WHERE id = @id;";
                    using (var cmd = new NpgsqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("id", productId);
                        cmd.Parameters.AddWithValue("newName", newName);
                        cmd.Parameters.AddWithValue("newPrice", newPrice);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Product with ID '{productId}' updated successfully.");
                        else
                            Console.WriteLine($"No product with ID '{productId}' found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
            }
        }
    }
}
