using PieShop.InventoryManagement.Domain.General;
using System;

namespace PieShop.InventoryManagement
{
    class Program
    {
        static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome Palmeirense!");
            Console.ResetColor();
            Console.WriteLine("Press enter key to start logging in");
            Console.ReadLine();
            Console.Clear();
        }
        static void Main(string[] args)
        {         
            PrintWelcome();
            Utilities.InitializeStock();
            Utilities.ShowMainMenu();
            Console.WriteLine("Application shutting down ...");
            Console.ReadLine();
        }
    }
}
