using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.OrderManagement
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<OrderItem> OrderItems { get; }
        public bool Fulfilled { get; set; } = false;

        public string ShowOrderDetails()
        {
            StringBuilder orderDetails = new StringBuilder();
            orderDetails.Append($"Order ID: {Id}");
            orderDetails.Append($"Order fulfilment date: {OrderFulfilmentDate.ToShortTimeString()}");

            if (OrderItems != null)
            {
                foreach (OrderItem item in OrderItems)
                {
                    orderDetails.AppendLine($"{item.ProductId}. {item.ProductName} : {item.AmountOrdered}");
                }
            }
            
            return orderDetails.ToString();
        }
    }
}
