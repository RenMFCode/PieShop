using PieShop.InventoryManagement.Domain.General;
using PieShop.InventoryManagement.Domain.OrderManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();

        internal static void InitializeStock() //Mock implementation
        {
            Product.ChangeStockThreshold(15);
            Price samplePrice = new Price(10, Currency.Euro);
            Product p1 = new Product(1, "Sugar", "Lorem cenas", samplePrice, UnitType.PerKg, 100);
            p1.IncreaseStock(10);
            p1.Description = "Descrip";

            var p2 = new Product(2, "Cake", "cenas", samplePrice, UnitType.PerItem, 20);

            inventory.Add(p1);
            inventory.Add(p2);
        }

        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("1. Inventory");
            Console.WriteLine("2. Order");
            Console.WriteLine("3. Settings");
            Console.WriteLine("4. Save");
            Console.WriteLine("0. Close");

            Console.Write("Your selection");

            string? userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    //ShowSettingsMenu();
                    break;
                case "4":
                    // SaveAllData();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static void ShowOrderManagementMenu()
        {
            throw new NotImplementedException();
        }

        private static void ShowInventoryManagementMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Inventory management");

            ShowAllProductsOverview();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("What do you want to do?");
            Console.ResetColor();

            Console.WriteLine("1. View details");
            Console.WriteLine("2. Add new product");
            Console.WriteLine("3. Clone product");
            Console.WriteLine("4. View products with low stock");
            Console.WriteLine("0. Back");

            Console.Write("Your selection");

            string? userSelection = Console.ReadLine();

            switch (userSelection)
            {
                case "1":
                    ShowDetailsAndUseProduct();
                    break;
                case "2":
                    //ShowCreateNewProduct();
                    break;
                case "3":
                    //ShowCloneExistingProduct();
                    break;
                case "4":
                    //ShowProductsLowOnStock();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static void ShowDetailsAndUseProduct()
        {
            string? userSelection = String.Empty; 
            Console.Write("Enter the ID of product");
            string? selectedProductId = Console.ReadLine();
            if (selectedProductId is not null)
            {
                Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                if (selectedProduct is not null)
                {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());
                    Console.WriteLine("1. Use");
                    Console.WriteLine("2. Back");

                    userSelection = Console.ReadLine();

                    if (userSelection == "1")
                    {
                        Console.WriteLine("How many produts to use???");
                        int amount = int.Parse(Console.ReadLine() ?? "0");
                        selectedProduct.UseProduct(amount);

                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Non-existing product");
            }
        }

        private static void ShowAllProductsOverview()
        {

            foreach(var product in inventory)
            {
                Console.Write(product.DisplayDetailsShort());
            }
            

        }
    }
}
