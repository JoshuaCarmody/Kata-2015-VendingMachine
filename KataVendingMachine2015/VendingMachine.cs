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
        private static readonly string MessageSoldOut = "SOLD OUT";
        private static readonly string MessageExactChange = "EXACT CHANGE ONLY";

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
                if(!IsGuaranteedToMakeChange())
                {
                    return MessageExactChange;
                }
                return MessageInsertCoins;
            }

            string returnValue = DisplayText;
            DisplayText = String.Empty;

            return returnValue;
        }

        private bool IsGuaranteedToMakeChange()
        {
            // TODO: Make this dynamic, in case we ever want to change the denominations we accept.

            /* Because candy is $0.65, we can never guarantee correct change for 7 dimes 
             * if we don't have a nickel. */
            if(!CoinBank.Any(c => c.ValueInUsCents == 5))
            {
                return false;
            }
            /* The most anyone can overpay without putting in extra coins is $0.20, because the largest
             * coin we accept is a quarter. If anyone overpays by $0.25 or more, we can just give the
             * extra coins they inserted back to them. */
            if(CoinBank.Where(c => c.ValueInUsCents <= 10).Sum(c => c.ValueInUsCents) < 20)
            {
                return false;
            }
            return true;
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
            var foundProduct = Inventory.FirstOrDefault(p => p.ProductType == productType);

            if (foundProduct == null)
            {
                DisplayText = MessageSoldOut;
                return;
            }

            if (foundProduct.ProductType.PriceInUsCents > CreditInUsCents)
            {
                DisplayText = String.Format(MessagePriceFormat, productType.PriceInUsCents / 100.0);
                return;
            }

            PurchaseProduct(foundProduct);
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
            var returnValue = new List<Product>(ProductTray);
            ProductTray.RemoveRange(0, ProductTray.Count);
            return returnValue;
        }
    }
}
