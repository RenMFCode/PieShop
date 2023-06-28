using PieShop.InventoryManagement.Domain.General;
using System;
using System.Text;

namespace PieShop.InventoryManagement
{
    public partial class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description; // ? might be empty in the future
        private Price price;

        protected int maxItemsInStock = 0;

        public static int StockThreshold = 5;

        public Product(int id) : this(id, string.Empty)
        {
            this.Id = id;
        }

        public Product(int id, string name)
        {
            this.Id = id;
            this.name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock) : this(id, name)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;
            Price = price;

            maxItemsInStock = maxAmountInStock;

            UpdateLowStock();

        }

        // DELETE IT, WE CAN USE THE PROPERTIES INSTEAD (getters & setters)
        /*
        private UnitType unitType;
        private int amountInStock = 0;
        private bool isBelowStockThreshold = false;
        */

        // Generate getters/setters Ctrl R, CTRL E in the selected field
        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }
        public string Description {
            get { return description; }
            set 
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..] : value;
                }
            } 
        }

        public UnitType UnitType { get; set; }
        public bool IsBelowStockThreshold { get; protected set; } 
        public int AmountInStock { get; protected set; }
        public int Id { get; set; }
        internal Price Price { get; set; }

        public static void ChangeStockThreshold(int newStockThreshold)
        {
            if (newStockThreshold > 0)
                StockThreshold = newStockThreshold;
        }

        public /*virtual - to override in the subclass*/ virtual void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
                UpdateLowStock();
                Log($"Amount stock updated! Now {AmountInStock} items in stock.");
            }
            else
            {
                Log($"Not enough items on stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }


        public virtual void IncreaseStock()
        {
            AmountInStock++;
        }

        public virtual void IncreaseStock(int amount)
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
        }

        protected virtual void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();
            Log(reason);
        }

        public virtual void UpdateLowStock()
        {
            if (AmountInStock < StockThreshold) // for now fixed value
            {
                IsBelowStockThreshold = true;
            }
        }

        public virtual string DisplayDetailsShort()
        {
            return $"\n{Id}. {Name} \n{AmountInStock} items in stock.";
        }

        public virtual string DisplayDetailsFull()
        {
            // we can comment this code, it's reused in the method in below - Overload
            /*   StringBuilder sb = new();
               sb.Append($"{id} - {Name} \n {Description}\n{AmountInStock} ...");
               if (IsBelowStockThreshold)
               {
                   sb.Append("\n STOCK LOW!!!");
               }
               return sb.ToString();
            */
            return DisplayDetailsFull("");
        }

        public virtual string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();
            sb.Append($"{Id} - {Name} \n {Description}\n{AmountInStock}\n {Price}  ...");

            sb.Append(extraDetails);
            if (IsBelowStockThreshold)
            {
                sb.Append("\n STOCK LOW!!!");
            }
            return sb.ToString();
        }

        protected void Log(string message)
        {
            Console.WriteLine(message);
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }
    }
}
