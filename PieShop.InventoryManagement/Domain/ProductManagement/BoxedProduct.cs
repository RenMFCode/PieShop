using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    public class BoxedProduct : Product
    {
        private int amountPerBox;

        public BoxedProduct(int id, string name, string description, Price price, /* REMOVED -> UnitType unitType,*/ int maxAmountInStock, /*new field -> */ int amountPerBox) :
            base(id, name, description, price, /*unitType -> */ UnitType.PerBox, maxAmountInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public string DisplayBoxedProductDetails()
        {
            StringBuilder sb = new();
            sb.Append("Boxed Product\n");
            sb.Append($"{Id} - {Name} \n {Description}\n{AmountInStock}\n {Price}\n{AmountInStock} Item(s) in stock  ...");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n STOCK LOW!!!");
            }
            return sb.ToString();
        }

        /* public void UseBoxedProduct(int items)
        {    
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = smallestMultiple * AmountPerBox;
                    break;
                }
            }
            UseProduct(batchSize); //use base method  -> Override in the method below
        } */

        public override void UseProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while (true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = smallestMultiple * AmountPerBox;
                    break;
                }
            }

            base.UseProduct(batchSize);// base (superclass)
        }

        public override string DisplayDetailsFull()
        {
            StringBuilder sb = new();
            sb.Append("Boxed Product\n"); // this is the difference between the methods
            sb.Append($"{Id} - {Name} \n {Description}\n{AmountInStock} ...");
            if (IsBelowStockThreshold)
            {
                sb.Append("\n STOCK LOW!!!");
            }
            return sb.ToString();
        }

        public override void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;
            if (newStock <= maxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = maxItemsInStock;
                Log("More items than expected, set the max number");
            }
            if (AmountInStock > StockThreshold)
            {
                IsBelowStockThreshold = false;
            }
            base.IncreaseStock();
        }

        public int AmountPerBox {
            get { return amountPerBox; }
            set { amountPerBox = value; } 
        }
    }
}
