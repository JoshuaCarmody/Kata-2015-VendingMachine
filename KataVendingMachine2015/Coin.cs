using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KataVendingMachine2015
{
    public class Coin
    {
        public readonly double WeightInMilligrams;
        public readonly double DiameterInMms;
        
        protected Coin()
        {

        }

        public Coin(double diameterInMms, double weightInMilligrams)
        {
            DiameterInMms = diameterInMms;
            WeightInMilligrams = weightInMilligrams;
        }
    }
}
