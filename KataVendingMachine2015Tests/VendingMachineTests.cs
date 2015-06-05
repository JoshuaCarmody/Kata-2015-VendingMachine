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

    }
}
