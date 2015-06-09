using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class Product
    {
        public ProductType ProductType { get; private set; }

        public Product(ProductType productType)
        {
            this.ProductType = productType;
        }
    }
}
