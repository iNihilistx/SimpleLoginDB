using System;
using ConsoleApp1.Models;
using MySqlConnector;

namespace ConsoleApp1.Services
{
    public class DatabaseService
    {
        private static MySqlConnection _connection;

        public static void InitializeConnection(string server, string database, string username, string password)
        {
            string connectionString = $"Server={server};Database={database};User={username};Password={password}";
            _connection = new MySqlConnection(connectionString);

            try
            {
                _connection.Open();
                Console.WriteLine("Connection to the database established successfully.");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine($"Error connecting to the database: {ex.Message}");
            }
        }

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                throw new InvalidOperationException("Database connection is not initialized. Call InitializeConnection first.");
            }
            return _connection;
        }
    }
}