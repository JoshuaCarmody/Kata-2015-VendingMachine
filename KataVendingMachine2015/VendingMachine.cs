using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class VendingMachine
    {
        private List<Product> Inventory = new List<Product>();

        public VendingMachine()
        {
        }

        public IEnumerable<Product> GetInventory()
        {
            return new List<Product>(Inventory);
        }

        public void AddProduct(Product product)
        {
            Inventory.Add(product);
        }

        public string GetDisplayText()
        {
            return "Insert Coins";
        }
    }
}
