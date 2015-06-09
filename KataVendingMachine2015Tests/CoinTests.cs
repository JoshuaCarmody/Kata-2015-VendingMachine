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

            ValuedCoin result = ValuedCoin.FromCoin(testNickel, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(5, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_10_Cents_When_Called_On_A_Coin_With_The_Physical_Properties_Of_A_Dime()
        {
            Coin testDime = new Coin(ValuedCoin.Dime.DiameterInMms, ValuedCoin.Dime.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testDime, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(10, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_25_Cents_When_Called_On_A_Coin_With_The_Physical_Properties_Of_A_Quarter()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(25, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_25_Cents_When_Called_On_A_Coin_Slightly_Off_In_Weight_From_A_Quarter()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams - 16);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(25, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_25_Cents_When_Called_On_A_Coin_Slightly_Off_In_Size_From_A_Quarter()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms + 0.1, ValuedCoin.Quarter.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(25, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_0_Cents_When_Called_On_A_Coin_Significantly_Off_In_Size_From_A_Quarter()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms + 1, ValuedCoin.Quarter.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(0, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_0_Cents_When_Called_On_A_Coin_Significantly_Off_In_Weight_From_A_Quarter()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams - 200);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });

            Assert.AreEqual(0, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Return_A_Coin_Worth_0_Cents_Given_A_Quarter_And_Quarters_Arent_Acceptable()
        {
            Coin testQuarter = new Coin(ValuedCoin.Quarter.DiameterInMms, ValuedCoin.Quarter.WeightInMilligrams);

            ValuedCoin result = ValuedCoin.FromCoin(testQuarter, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime });

            Assert.AreEqual(0, result.ValueInUsCents);
        }

        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Throw_ArgumentNullException_Given_Null_As_First_Argument()
        {
            Exception result = null;

            try
            {
                ValuedCoin.FromCoin(null, new List<ValuedCoin> { ValuedCoin.Nickel, ValuedCoin.Dime, ValuedCoin.Quarter });
            }
            catch(ArgumentNullException e)
            {
                result = e;
            }

            Assert.AreNotEqual(null, result);
        }


        [TestMethod]
        public void ValuedCoin_FromCoin_Should_Throw_ArgumentNullException_Given_Null_As_Second_Argument()
        {
            Exception result = null;

            try
            {
                ValuedCoin.FromCoin(new Coin(50, 50), null);
            }
            catch (ArgumentNullException e)
            {
                result = e;
            }

            Assert.AreNotEqual(null, result);
        }
    }
}
