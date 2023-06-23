using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.General
{
    public class Price
    {
        public Price(double itemPrice, Currency currency)
        {
            ItemPrice = itemPrice;
            Currency = currency;
        }

        public double ItemPrice { get; set; }
        public Currency Currency { get; set; }
        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }


    }
}
