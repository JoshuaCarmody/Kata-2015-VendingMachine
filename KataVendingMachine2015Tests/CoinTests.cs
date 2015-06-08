using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using KataVendingMachine2015;

namespace KataVendingMachine2015Tests
{
    [TestClass]
    public class CoinTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_5_Cents_When_Called_On_A_Coin_With_The_Physical_Properties_Of_A_Nickel()
        {
            Coin testNickel = new Coin(ValuedCoin.Nickel.DiameterInMms, ValuedCoin.Nickel.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testNickel, new List<ValuedCoin> { ValuedCoin.Nickel });

            Assert.AreEqual(5, result.ValueInUsCents);
        }
    }
}
