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
        Console.Write("Enter Username: ");
        _userModel.LoginUsername = Console.ReadLine();
        Console.Write("Enter Password: ");
        _userModel.LoginPassword = Console.ReadLine();

        _userService.Login
           (
            _userModel.LoginUsername,
            _userModel.LoginPassword
           );
    }
}