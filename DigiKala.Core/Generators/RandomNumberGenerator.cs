using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiKala.Core.Generators
{
    public class RandomNumberGenerator
    {
        public static int RandomIntegerGenerator(int minValue,int maxValue)
        {
            var random = new System.Random();
            if (minValue == 0)
                minValue = 100000;
            if (maxValue == 0)
                maxValue = 99999;
            return random.Next(minValue,maxValue);
        }
    }
}
