using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KataVendingMachine2015;
using System.Collections.Generic;
using System.Linq;

namespace KataVendingMachine2015Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        private VendingMachine vm = null;

        [TestInitialize]
        public void Init()
        {
            vm = new VendingMachine();

            for (int i = 0; i < 20; i++)
            {
                vm.AddProduct(new Product(ProductType.Chips));
                vm.AddProduct(new Product(ProductType.Candy));
                vm.AddProduct(new Product(ProductType.Cola));
                vm.AddCoin(ValuedCoin.Quarter);
                vm.AddCoin(ValuedCoin.Dime);
                vm.AddCoin(ValuedCoin.Nickel);
            }
        }

        [TestMethod]
        public void Vending_Machine_Should_Have_Products()
        {
            IEnumerable<Product> result = vm.GetInventory();

            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void AddProduct_Should_Add_A_Product_To_The_Iventory()
        {
            Product product = new Product(ProductType.Chips);
            vm.AddProduct(product);
            var result = vm.GetInventory();
            Assert.IsTrue(result.Contains(product));
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_Insert_Coins_If_No_Coins_Inserted()
        {
            string result = vm.GetDisplayText();

            Assert.AreEqual("INSERT COINS", result);
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_0_25_After_Quarter_Inserted()
        {
            Coin quarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams);

            vm.InsertCoin(quarter);
            string result = vm.GetDisplayText();

            Assert.AreEqual("CREDIT: $0.25", result);
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_0_50_After_Two_Quarters_Inserted()
        {
            Coin quarter1 = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams);
            Coin quarter2 = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams);

            vm.InsertCoin(quarter1);
            vm.InsertCoin(quarter2);
            string result = vm.GetDisplayText();

            Assert.AreEqual("CREDIT: $0.50", result);
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_Insert_Coins_After_Bad_Coin_Inserted()
        {
            Coin badCoin = new Coin(123, 25); // Arbitrary numbers

            vm.InsertCoin(badCoin);
            string result = vm.GetDisplayText();

            Assert.AreEqual("INSERT COINS", result);
        }

        [TestMethod]
        public void GetReturnedCoins_Should_Contain_The_Inserted_Coin_After_Bad_Coin_Inserted()
        {
            Coin fakeQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams - 1000);

            vm.InsertCoin(fakeQuarter);
            var result = vm.GetReturnedCoins();

            Assert.IsTrue(result.Contains(fakeQuarter));
        }

        [TestMethod]
        public void GetReturnedCoins_Should_Return_An_Empty_List_After_Being_Called_Once()
        {
            Coin fakeQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams - 1000);

            vm.InsertCoin(fakeQuarter);
            vm.GetReturnedCoins();
            var result = vm.GetReturnedCoins();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void SelectProduct_Should_Dispense_Chips_Given_Chips_When_Enough_Money_Is_Inserted()
        {
            for (int i = 0; i < 2; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Chips);
            var result = vm.GetProductsFromTray();

            Assert.IsTrue(result.Any(p => p.ProductType == ProductType.Chips));
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void SelectProduct_Should_Not_Dispense_2_Chips_If_User_Only_Paid_For_One()
        {
            for (int i = 0; i < 2; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Chips);
            vm.SelectProduct(ProductType.Chips);
            var result = vm.GetProductsFromTray();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void SelectProduct_Should_Not_Dispense_Chips_Given_Chips_When_Insufficient_Money_Is_Inserted()
        {
            vm.InsertCoin(ValuedCoin.Quarter);

            vm.SelectProduct(ProductType.Chips);
            var result = vm.GetProductsFromTray();

            Assert.IsFalse(result.Any(p => p.ProductType == ProductType.Chips));
            Assert.AreEqual(0, result.Count());
        }


        [TestMethod]
        public void GetDisplayText_Should_Return_Price_0_50_Chips_Selected_With_Insufficient_Credit()
        {
            vm.InsertCoin(ValuedCoin.Quarter);

            vm.SelectProduct(ProductType.Chips);
            var result = vm.GetDisplayText();

            Assert.AreEqual("PRICE: $0.50", result);
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_Thank_You_After_A_Purchase()
        {
            for (int i = 0; i < 4; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);                
            }

            vm.SelectProduct(ProductType.Cola);
            var result = vm.GetDisplayText();

            Assert.AreEqual("THANK YOU", result);
        }

        [TestMethod]
        public void GetDisplayText_Should_Return_Insert_Coins_When_Checked_After_Thank_You()
        {
            for (int i = 0; i < 4; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Cola);
            vm.GetDisplayText();
            var result = vm.GetDisplayText();

            Assert.AreEqual("INSERT COINS", result);
        }

        [TestMethod]
        public void No_Credit_Should_Be_Left_After_Calling_SelectProduct()
        {
            for (int i = 0; i < 8; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Cola);
            vm.GetDisplayText();
            var resultText = vm.GetDisplayText();

            Assert.AreEqual("INSERT COINS", resultText);

            vm.SelectProduct(ProductType.Cola);
            var resultProducts = vm.GetProductsFromTray();

            Assert.AreEqual(1, resultProducts.Count());
        }

        [TestMethod]
        public void VendingMachine_Should_Return_25_Cents_When_5_Quarters_Are_Inserted_And_Cola_Is_Purchased()
        {
            for (int i = 0; i < 5; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Cola);
            var result = vm.GetReturnedCoins();

            Assert.AreEqual(25, result.Sum(c => (c as ValuedCoin).ValueInUsCents));
        }

        [TestMethod]
        public void VendingMachine_Should_Return_10_Cents_When_3_Quarters_Are_Inserted_And_Candy_Is_Purchased()
        {
            for (int i = 0; i < 3; i++)
            {
                vm.InsertCoin(ValuedCoin.Quarter);
            }

            vm.SelectProduct(ProductType.Candy);
            var result = vm.GetReturnedCoins();

            Assert.AreEqual(10, result.Sum(c => (c as ValuedCoin).ValueInUsCents));
        }

        [TestMethod]
        public void VendingMachine_Should_Return_5_Cents_When_11_Nickels_Are_Inserted_And_Chips_Are_Purchased()
        {
            for (int i = 0; i < 11; i++)
            {
                vm.InsertCoin(ValuedCoin.Nickel);
            }

            vm.SelectProduct(ProductType.Chips);
            var result = vm.GetReturnedCoins();

            Assert.AreEqual(5, result.Sum(c => (c as ValuedCoin).ValueInUsCents));
        }
    }
}
