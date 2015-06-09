using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class ProductType
    {
        public string DisplayName { get; private set; }
        public int PriceInUsCents { get; private set; }

        public static readonly ProductType Chips = new ProductType { DisplayName = "Chips", PriceInUsCents = 50 };
        public static readonly ProductType Cola = new ProductType { DisplayName = "Cola", PriceInUsCents = 100 };
        public static readonly ProductType Candy = new ProductType { DisplayName = "Candy", PriceInUsCents = 65 };

        private ProductType()
        {

        }
    }
}
