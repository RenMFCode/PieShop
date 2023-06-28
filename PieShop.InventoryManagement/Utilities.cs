using PieShop.InventoryManagement.Domain.General;
using PieShop.InventoryManagement.Domain.OrderManagement;
using PieShop.InventoryManagement.Domain.ProductManagement;
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
            BoxedProduct bp = new BoxedProduct(6, "Eggs", "Lorem cenas", new Price(10, Currency.Euro), 100, 6);
            bp.IncreaseStock(100);
            bp.UseProduct(10);

            inventory.Add(bp);

            ProductRepository productRepository = new();
            inventory = productRepository.LoadProductsFromFile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loaded {inventory.Count} products!");

            Console.WriteLine("Press enter to continue!");
            Console.ResetColor();
            Console.ReadLine();
        }

        internal static void ShowMainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine("Main Menu");
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
                    ShowCreateNewProduct();
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

        private static void ShowCreateNewProduct()
        {
            UnitType unitType = UnitType.PerItem; //default
            int numberInBox = 0;
            int newId = 0;

            Console.WriteLine("What kind of product?");
            Console.WriteLine("1. Regular\n 2. Bulk\n 3. Fresh\n 4. Boxed");
            Console.Write("Your Selection: ");

            var productType = Console.ReadLine();

            if (productType != "1" && productType != "2" && productType != "3" && productType != "4")
            {
                Console.WriteLine("Invalid selection");
                return;
            }

            Product? newProd = null;

            Console.WriteLine("Enter the name of the product: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter the price :");
            double price = double.Parse(Console.ReadLine() ?? "0.0");

            ShowAllCurrencies();

            Console.WriteLine("Select the currency: ");
            Currency currency = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine() ?? "1");

            Console.WriteLine("Description: ");
            string description = Console.ReadLine() ?? String.Empty;

            if (productType == "1")
            {
                ShowAllUnitTypes();
                Console.Write("Select the unit type");
                unitType = (UnitType)Enum.Parse(typeof(UnitType), Console.ReadLine() ?? "1");
            }
            
            Console.WriteLine("Enter the maximum number in stock for this product: ");
            int maxInStock = int.Parse(Console.ReadLine() ?? "0");

            if (inventory.Count != 0)
                newId = inventory.Max(p => p.Id) + 1; // Find the highest and Increment 1 

            switch (productType)
            {
                case "1":
                    newProd = new Product(newId, name, description, new Price(price, currency), unitType, maxInStock) ;
                    break;
                case "2":
                    newProd = new BulkProduct(newId++, name, description, new Price(price, currency), maxInStock);
                    break;
                case "3":
                    Console.Write("Enter the number of items per box: ");
                    numberInBox = int.Parse(Console.ReadLine() ?? "0");
                    newProd = new FreshProduct(newId++, name, description, new Price(price, currency), unitType, maxInStock, numberInBox);
                    break;
                case "4":
                    Console.Write("Enter the number of items per box: ");
                    numberInBox = int.Parse(Console.ReadLine() ?? "0");
                    newProd = new BoxedProduct(newId++, name, description, new Price(price, currency), maxInStock, numberInBox);
                    break;
                default:
                    Console.WriteLine("Invalid product");
                    break;
            }
            if (newProd != null)
                inventory.Add(newProd);
        }

        private static void ShowAllUnitTypes()
        {
            int i = 1;
            foreach (string name in Enum.GetNames(typeof(UnitType))) 
            { 
                Console.WriteLine($"{i}. {name}");
                i++;
            }
        }

        private static void ShowAllCurrencies()
        {
            int i = 1;
            foreach (string name in Enum.GetNames(typeof(Currency)))
            {
                Console.WriteLine($"{i}. {name}");
                i++;
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
            ShowAllProductsOverview();

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
