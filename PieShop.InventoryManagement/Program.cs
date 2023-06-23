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
            Product.ChangeStockThreshold(15);
            
            /* Price samplePrice = new Price(10,Currency.Euro);
            Product p1 = new Product(1,"Sugar", "Lorem cenas", samplePrice, UnitType.PerKg, 100);
            p1.IncreaseStock(10);
            p1.Description = "Descrip";

            var p2 = new Product(2, "Cake", "cenas", samplePrice, UnitType.PerItem, 20); */
            
            PrintWelcome();
            Utilities.InitializeStock();
            Utilities.ShowMainMenu();

        }
    }
}
