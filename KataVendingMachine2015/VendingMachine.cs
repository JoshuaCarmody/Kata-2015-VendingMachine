using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class VendingMachine
    {
        private readonly List<Product> Inventory = new List<Product>();
        private readonly List<ValuedCoin> CoinBank = new List<ValuedCoin>();
        private readonly List<Coin> ReturnedCoins = new List<Coin>();
        private readonly List<Product> ProductTray = new List<Product>();
        private static readonly List<ValuedCoin> AcceptableCoins = new List<ValuedCoin>() { ValuedCoin.Quarter, ValuedCoin.Dime, ValuedCoin.Nickel };
        private static readonly string CreditMessageFormat = "CREDIT: ${0:0.00}";

        private int CreditInUsCents = 0;

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
            if (CreditInUsCents > 0)
            {
                return String.Format(CreditMessageFormat, CreditInUsCents / 100.0);
            }
            
            return "Insert Coins";
        }

        public void InsertCoin(Coin coin)
        {
            ValuedCoin valuedCoin = ValuedCoin.FromCoin(coin, AcceptableCoins);

            if(valuedCoin.ValueInUsCents > 0)
            {
                CreditInUsCents += valuedCoin.ValueInUsCents;
                BankCoin(valuedCoin);
                return;
            }

            ReturnCoin(coin);
        }

        private void BankCoin(ValuedCoin valuedCoin)
        {
            CoinBank.Add(valuedCoin);
        }

        private void ReturnCoin(Coin coin)
        {
            ReturnedCoins.Add(coin);
        }

        public IEnumerable<Coin> GetReturnedCoins()
        {
            var returnValue = new List<Coin>(ReturnedCoins);
            ReturnedCoins.RemoveRange(0, ReturnedCoins.Count);
            return returnValue;
        }

        public void SelectProduct(ProductType productType)
        {
            if(productType.PriceInUsCents > CreditInUsCents)
            {
                return;
            }

            var foundProduct = Inventory.FirstOrDefault(p => p.ProductType == productType);

            if(foundProduct != null)
            {
                DispenseProduct(foundProduct);
            }
        }

        private void DispenseProduct(Product foundProduct)
        {
            ProductTray.Add(foundProduct);
            Inventory.Remove(foundProduct);
        }

        public IEnumerable<Product> GetProductsFromTray()
        {
            return new List<Product>(ProductTray);
        }
    }
}
