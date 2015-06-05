using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class VendingMachine
    {
        private IEnumerable<Product> Inventory = new List<Product>();

        public VendingMachine()
        {
        }

        public IEnumerable<Product> GetInventory()
        {
            return Inventory;
        }
    }
}
