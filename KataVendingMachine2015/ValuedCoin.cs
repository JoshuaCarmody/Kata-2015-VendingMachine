using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class ValuedCoin : Coin
    {
        public readonly int ValueInUsCents;

        private static readonly double CoinDiameterToleranceInMms = 0.5;
        private static readonly double CoinWeightToleranceInMilligrams = 50;

        // Coin diameters and weights taken from http://www.usmint.gov/about_the_mint/?action=coin_specifications as of 2015/06/08
        public static readonly ValuedCoin Quarter = new ValuedCoin(24.26, 5670, 25);
        public static readonly ValuedCoin Dime = new ValuedCoin(17.91, 2268, 10);
        public static readonly ValuedCoin Nickel = new ValuedCoin(21.21, 5000, 5);
        public static readonly ValuedCoin Penny = new ValuedCoin(19.05, 2500, 1);

        public ValuedCoin(double diameterInMms, double weightInMilligrams, int value)
            : base(diameterInMms, weightInMilligrams)
        {
            ValueInUsCents = value;
        }

        public static ValuedCoin FromCoin(Coin coin, IEnumerable<ValuedCoin> acceptableCoins)
        {
            int valueInCents = 0;

            foreach(var acceptableCoin in acceptableCoins)
            {
                if (WithinTolerance(coin, acceptableCoin))
                {
                    valueInCents = Math.Max(valueInCents, acceptableCoin.ValueInUsCents);
                }

            }

            return new ValuedCoin(coin.DiameterInMms, coin.WeightInMilligrams, valueInCents);
        }

        private static bool WithinTolerance(Coin coin, ValuedCoin acceptableCoin)
        {
            return Math.Abs(acceptableCoin.DiameterInMms - coin.DiameterInMms) <= CoinDiameterToleranceInMms
                                && Math.Abs(acceptableCoin.WeightInMilligrams - coin.WeightInMilligrams) <= CoinWeightToleranceInMilligrams;
        }         
    }
}
