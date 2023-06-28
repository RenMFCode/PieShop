using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.IO;


namespace PieShop.InventoryManagement
{
    internal class ProductRepository
    {
        private string directory = @"C:\Renato\PieShop.InventoryManagement\storage\";
        private string productsFileName = "products.txt";

        public List<Product> LoadProductsFromFile()
        {
            List<Product> products = new List<Product>();

            string path = $"{directory}{productsFileName}";
            
            try
            {
                CheckForExistingProductFile();

                string[] productsAsString = File.ReadAllLines(path);
                for (int i = 0; i < productsAsString.Length; i++)
                {
                    string[] productSplits = productsAsString[i].Split(';');

                    bool sucess = int.TryParse(productSplits[0], out int produtId);
                    if (!sucess)
                        produtId = 0;

                    string name = productSplits[1];
                    string description = productSplits[2];

                    sucess = int.TryParse(productSplits[3], out int maxItemsInStock);
                    if (!sucess)
                        maxItemsInStock = 100; //default value
                
                    sucess = double.TryParse(productSplits[4], out double itemPrice);
                    if (!sucess)
                        itemPrice = 0; //default value

                    sucess = Enum.TryParse(productSplits[5], out Currency currency);
                    if (!sucess)
                        currency = Currency.Dollar; //default value

                    sucess = Enum.TryParse(productSplits[6], out UnitType unitType);

                    Product product = new Product(produtId, name, description, new Price(itemPrice, currency), unitType, maxItemsInStock );

                    products.Add(product);

                }
            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(fnfex.Message);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return products;

        }

        private void CheckForExistingProductFile()
        {
            string path = $"{directory}{productsFileName}";

            bool existingFileFound = File.Exists(path);
            if (! existingFileFound)
            {
                // Create the directory
                if (! Directory.Exists(path))
                    Directory.CreateDirectory(directory);

                // Create the empty file
                using FileStream fd = File.Create(path);
            }
        }
    }
}
