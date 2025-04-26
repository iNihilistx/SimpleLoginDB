using System;
using ConsoleApp1.Models;
using ConsoleApp1.Helpers;
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
            string hashedPassword = PasswordHasher.HashPassword(password);
            var command = new MySqlCommand("INSERT INTO login_table (LoginUsername, LoginPassword) VALUES (@username, @password)", _connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", hashedPassword);

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
            var command = new MySqlCommand("SELECT LoginPassword FROM login_table WHERE LoginUsername = @username", _connection);
            command.Parameters.AddWithValue("@username", username);

            using var reader = command.ExecuteReader();
            bool success = false;
            try
            {
                if(reader.Read())
                {
                    string storedHash = reader.GetString("LoginPassword");
                    if(PasswordHasher.VerifyPassword(password, storedHash))
                    {
                        Console.WriteLine("Login successful.");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password.");
                        success = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid username or password.");
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return success;
        }
    }
}