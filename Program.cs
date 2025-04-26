using System;
using ConsoleApp1.Services;
using ConsoleApp1.Models;

class App
{
    private static DatabaseModel _dbModel = new DatabaseModel();
    private static UserModel _userModel = new UserModel();
    private static UserService _userService;
    public static void Main(string[] args)
    {
        _dbModel.Server = "localhost";
        _dbModel.Database = "test_database";
        _dbModel.Username = "root";
        _dbModel.Password = "root";

        DatabaseService.InitializeConnection
          (
            _dbModel.Server,
            _dbModel.Database,
            _dbModel.Username,
            _dbModel.Password
         );
        _userService = new UserService(DatabaseService.GetConnection());
        DisplayMenu();
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Database connection initialized. You can now register or login.");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.Write("Choose an option: ");
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice)
        {
            case 1:
                RegisterUser();
                break;
            case 2:
                LoginUser();
                break;
            case 3:
                DatabaseService.CloseConnection();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    private static UserModel GetCredentials()
    {
        Console.Clear();
        Console.Write("Enter Username: ");
        _userModel.LoginUsername = Console.ReadLine();
        Console.Write("Enter Password: ");
        _userModel.LoginPassword = Console.ReadLine();
        return _userModel;
    }

    private static void RegisterUser()
    {
        UserModel user = GetCredentials();
        _userService.RegisterUser(_userModel.LoginUsername, _userModel.LoginPassword);
        DisplayMenu();
    }

    private static void LoginUser()
    {
        UserModel user = GetCredentials();
        _userService.Login(_userModel.LoginUsername, _userModel.LoginPassword);
        DisplayMenu();
    }
}