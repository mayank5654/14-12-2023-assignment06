using System;
using System.Data;
using System.Data.SqlClient;

namespace Assignment5

{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-LVRAQ1E;Database=productinventorydb;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connected to the database");

                while (true)
                {
                    Console.WriteLine("1. View Product Inventory");
                    Console.WriteLine("2. Add New Product");
                    Console.WriteLine("3. Update Product Quantity");
                    Console.WriteLine("4. Remove Product");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter the option number: ");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            ViewProductInventory(connection);
                            break;
                        case 2:
                            AddNewProduct(connection);
                            break;
                        case 3:
                            UpdateProductQuantity(connection);
                            break;
                        case 4:
                            RemoveProduct(connection);
                            break;
                        case 5:
                            Console.WriteLine("Exiting the application");
                            return;
                        default:
                            Console.WriteLine("Invalid option");
                            break;
                    }
                }
            }
        }

        private static void ViewProductInventory(SqlConnection connection)
        {
            string query = "SELECT * FROM Products";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine("\nProduct ID\tProduct Name\tPrice\tQuantity\tManufacturing Date\tExpiry Date");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ProductId"]}\t\t{reader["ProductName"]}\t \t{reader["Price"]}\t {reader["Quantity"]}\t \t{reader["MfDate"]}\t {reader["ExpDate"]}");
            }
            reader.Close();
        }

        private static void AddNewProduct(SqlConnection connection)
        {
            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();
            Console.Write("Enter Price: ");
            float price = Convert.ToSingle(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Manufacturing Date (YYYY-MM-DD): ");
            DateTime mfDate = Convert.ToDateTime(Console.ReadLine());
            Console.Write("Enter Expiry Date (YYYY-MM-DD): ");
            DateTime expDate = Convert.ToDateTime(Console.ReadLine());

            string query = "INSERT INTO Products (ProductName, Price, Quantity, MfDate, ExpDate) VALUES (@ProductName, @Price, @Quantity, @MfDate, @ExpDate)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductName", productName);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Quantity", quantity);
            command.Parameters.AddWithValue("@MfDate", mfDate);
            command.Parameters.AddWithValue("@ExpDate", expDate);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("New product added successfully");
            }
            else
            {
                Console.WriteLine("Failed to add new product");
            }
        }

        private static void UpdateProductQuantity(SqlConnection connection)
        {
            Console.Write("Enter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Quantity: ");
            int newQuantity = Convert.ToInt32(Console.ReadLine());

            string query = "UPDATE Products SET Quantity = @NewQuantity WHERE ProductId = @ProductId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NewQuantity", newQuantity);
            command.Parameters.AddWithValue("@ProductId", productId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Product quantity updated successfully");
            }
            else
            {
                Console.WriteLine("Failed to update product quantity");
            }
        }

        private static void RemoveProduct(SqlConnection connection)
        {
            Console.Write("Enter Product ID: ");
            int productId = Convert.ToInt32(Console.ReadLine());

            string query = "DELETE FROM Products WHERE ProductId = @ProductId";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ProductId", productId);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Product removed successfully");
            }
            else
            {
                Console.WriteLine("Failed to remove product");
            }
        }
    }
}