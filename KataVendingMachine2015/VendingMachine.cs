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
        private static readonly List<ValuedCoin> AcceptableCoins = new List<ValuedCoin>() { ValuedCoin.Quarter, ValuedCoin.Dime, ValuedCoin.Nickel };
        private static readonly string CreditMessageFormat = "CREDIT: ${0:0.00}";

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
            int creditInCents = CoinBank.Sum(c => c.ValueInUsCents);
            
            if(creditInCents > 0)
            {
                return String.Format(CreditMessageFormat, creditInCents / 100.0);
            }
            
            return "Insert Coins";
        }

        public void InsertCoin(Coin coin)
        {
            ValuedCoin valuedCoin = ValuedCoin.FromCoin(coin, AcceptableCoins);

            if(valuedCoin.ValueInUsCents > 0)
            {
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
    }
}
