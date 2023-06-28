using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    internal class FreshProduct : BoxedProduct
    {
        public DateTime ExpiryDateTime { get; set; }
        public string? StorageInstructions { get; set; }
        
        public FreshProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock, int amountPerBox) :
            base(id, name, description, price,  maxAmountInStock, amountPerBox)
        {
        }   

        //public void usefreshboxedproduct(int items)
        //{
        //    useboxedproduct(3); //sample invocation
        //}
        public override void UseProduct(int items )
        {

        }
        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new(); 
            sb.Append($"{Id} - {Name} \n {Description}\n{AmountInStock} ...");
            if (IsBelowStockThreshold)
            {
                sb.Append("\n STOCK LOW!!!");
            }
            // extra code
            sb.AppendLine("Storage Instructions: " + StorageInstructions);
            sb.AppendLine("Expiry date: " + ExpiryDateTime.ToShortDateString());

            return sb.ToString();
        }
    }
}
