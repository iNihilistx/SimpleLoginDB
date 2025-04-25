using System;
using ConsoleApp1.Models;
using MySqlConnector;

namespace ConsoleApp1.Services
{
    public class UserService
    {
        private readonly MySqlConnection _connection;

        public UserService(MySqlConnection connection)
        {
            _connection = connection;
        }

        public void RegisterUser(string username, string password)
        {
            var command = new MySqlCommand("INSERT INTO login_table (LoginUsername, LoginPassword) VALUES (@username, @password)", _connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("User registered successfully.");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine($"Error registering user: {ex.Message}");
                Console.WriteLine("Something went wrong registering user.");
            }
        }

        public bool Login(string username, string password)
        {
            var command = new MySqlCommand("SELECT * FROM login_table WHERE LoginUsername = @username AND LoginPassword = @password", _connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            using var reader = command.ExecuteReader();
            bool Success = reader.HasRows;

            if(Success)
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Login failed. Invalid username or password.");
            }
            return Success;
        }
    }
}