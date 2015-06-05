using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KataVendingMachine2015;
using System.Collections.Generic;

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

    }
}
