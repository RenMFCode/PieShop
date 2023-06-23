using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    // It's like the same class Product but splitted
    public partial class Product
    {
        private void Log(string message)
        {
            Console.WriteLine(message);
        }

     /*   private string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        } */
    }
}
