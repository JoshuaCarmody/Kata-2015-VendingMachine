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

        private static readonly string MessageCreditFormat = "CREDIT: ${0:0.00}";
        private static readonly string MessagePriceFormat = "PRICE: ${0:0.00}";
        private static readonly string MessageInsertCoins = "INSERT COINS";
        private static readonly string MessageThankYou = "THANK YOU";

        private int CreditInUsCents = 0;
        private string DisplayText = MessageInsertCoins;

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
            if(String.IsNullOrWhiteSpace(DisplayText))
            {
                if (CreditInUsCents > 0)
                {
                    return String.Format(MessageCreditFormat, CreditInUsCents / 100.0);
                }
                return MessageInsertCoins;
            }

            string returnValue = DisplayText;
            DisplayText = String.Empty;

            return returnValue;
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
            CreditInUsCents += valuedCoin.ValueInUsCents;
            AddCoin(valuedCoin);
            DisplayText = String.Format(MessageCreditFormat, CreditInUsCents / 100.0);
        }

        public void AddCoin(ValuedCoin valuedCoin)
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
                DisplayText = String.Format(MessagePriceFormat, productType.PriceInUsCents / 100.0);
                return;
            }

            var foundProduct = Inventory.FirstOrDefault(p => p.ProductType == productType);

            if(foundProduct != null)
            {
                PurchaseProduct(foundProduct);
            }
        }

        private void PurchaseProduct(Product foundProduct)
        {
            CreditInUsCents -= foundProduct.ProductType.PriceInUsCents;
            ProductTray.Add(foundProduct);
            Inventory.Remove(foundProduct);
            DisplayText = MessageThankYou;
            MakeChange();
        }

        public void MakeChange()
        {
            MakeChange(25);
            MakeChange(10);
            MakeChange(5);
            CreditInUsCents = 0;
        }

        private void MakeChange(int coinValue)
        {
            // TODO: This extra searching is inefficient. Should combined .Any and .FirstOrDefault checks. And probably each coin type should have its own bank.            
            while (CreditInUsCents >= coinValue && CoinBank.Any(c => c.ValueInUsCents == coinValue))
            {
                var returnCoin = CoinBank.FirstOrDefault(c => c.ValueInUsCents == coinValue);
                CoinBank.Remove(returnCoin);
                ReturnCoin(returnCoin);
                CreditInUsCents -= coinValue;
            }
        }

        public IEnumerable<Product> GetProductsFromTray()
        {
            return new List<Product>(ProductTray);
        }
    }
}
