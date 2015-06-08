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
            Product product = new Product();
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

    }
}
