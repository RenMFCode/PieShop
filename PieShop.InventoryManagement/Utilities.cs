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
                    ShowSettingsMenu();
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

        private static void ShowSettingsMenu()
        {
            string? userSelection;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Settings");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("What do you want to do? ");
                Console.ResetColor();

                Console.WriteLine("1. Change stock threshold");
                Console.WriteLine("0. Back to main menu");

                Console.Write("Your selection");
                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangeStockThreshold();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            }
            while (userSelection != "0");
        }

        private static void ShowChangeStockThreshold()
        {
            Console.WriteLine($"Enter the new stock threshold (current value: {Product.StockThreshold} This applies to all products)");
            Console.WriteLine("New value");
            int newValue = int.Parse(Console.ReadLine() ?? "0");
            Product.StockThreshold = newValue;
            Console.WriteLine($"New stock threshold {Product.StockThreshold}");

            foreach (var product in inventory)
            {
                product.UpdateLowStock();
            }
            Console.ReadLine();
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection = string.Empty;

            do
            {
                Console.ResetColor();
                Console.Clear();
                Console.WriteLine("Select an action");
                Console.WriteLine("1. Open order overview");
                Console.WriteLine("2. Add new order");
                Console.WriteLine("0. Back to main menu");

                Console.Write("Your selection");

                switch (userSelection)
                {
                    case "1":
                        ShowOpenOrderOverview();
                        break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }

            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowAddNewOrder()
        {
            Order newOrder = new Order();
            string? selectedProductId = string.Empty;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Creating a new order");
            Console.ResetColor();

            do
            {
                ShowAllProductsOverview();

                Console.WriteLine("Which product do you want to order? Enter 0 to stop adding");
                Console.WriteLine("Enter the ID of product: ");

                selectedProductId = Console.ReadLine();
                if (selectedProductId != "0")
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                    if (selectedProduct != null)
                    {
                        Console.WriteLine("How many do you want to order? ");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                        OrderItem orderItem = new OrderItem
                        {
                            ProductId = selectedProduct.Id,
                            ProductName = selectedProduct.Name,
                            AmountOrdered = amountOrdered
                        };
                    }
                }
            } while (selectedProductId != "0");

            Console.WriteLine("Creating order...");
            orders.Add(newOrder);

            Console.WriteLine("Order created.");
            Console.ReadLine();

        }

        private static void ShowInventoryManagementMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Inventory management");

            ShowAllProductsOverview();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nWhat do you want to do?");
            Console.ResetColor();

            Console.WriteLine("1. View details");
            Console.WriteLine("2. Add new product");
            Console.WriteLine("3. Clone product");
            Console.WriteLine("4. View products with low stock");
            Console.WriteLine("0. Back");

            Console.Write("Your selection: ");

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
                    ShowProductsLowOnStock();
                    break;
                case "0":
                    break; // It will not enter in anything and just close the app
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private static void ShowProductsLowOnStock()
        {
            List<Product> lowOnStockProducts = inventory.Where(p => p.IsBelowStockThreshold).ToList();
            if (lowOnStockProducts.Count > 0)
            {
                Console.WriteLine("The following items are low on stock!");
                foreach (var product in lowOnStockProducts)
                {
                    Console.WriteLine(product.DisplayDetailsShort());
                    Console.WriteLine();
                }
            }
        }

        private static void ShowDetailsAndUseProduct()
        {
            string? userSelection = String.Empty; 
            Console.Write("Enter the ID of product");
            string? selectedProductId = Console.ReadLine();
            if (selectedProductId != null)
            {
                Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                if (selectedProduct != null)
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

        private static void ShowOpenOrderOverview()
        {
            // Check to handle fulfilled orders
            ShowFulfilledOrders();

            if (orders.Count > 0)
            {
                Console.WriteLine("Open orders: ");
                foreach(var order in orders)
                {
                    Console.WriteLine(order.ShowOrderDetails());
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Tehre are no open orders");
            }
        }

        private static void ShowFulfilledOrders()
        {
            Console.WriteLine("Checking fulfilled orders.");
            foreach (var order in orders)
            {
                if (!order.Fulfilled && order.OrderFulfilmentDate < DateTime.Now) //fulfill the order
                {
                    foreach (var orderItem in order.OrderItems)
                    {
                        Product? selectedProduct = inventory.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();
                        if (selectedProduct != null)
                            selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                    }
                    order.Fulfilled = true;
                }
            }
            orders.RemoveAll(o => o.Fulfilled);
            Console.WriteLine("Fulfilled order checked");
        }
    }
}
