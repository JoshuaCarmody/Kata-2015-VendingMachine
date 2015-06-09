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

            Assert.AreEqual("Insert Coins", result);
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

            Assert.AreEqual("Insert Coins", result);
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
    }
}
