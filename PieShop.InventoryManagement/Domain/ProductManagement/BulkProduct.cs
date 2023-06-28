using PieShop.InventoryManagement.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PieShop.InventoryManagement.Domain.ProductManagement
{
    internal class BulkProduct : Product
    {
        public BulkProduct(int id, string name, string description, Price price, int maxAmountInStock) : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
        {
        }
    }
}
