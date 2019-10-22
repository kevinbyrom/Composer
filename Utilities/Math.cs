using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Composer.Utilities
{
    public static class MathUtil
    {
        public static double Lerp(this double min, double max, double pct)
        {
            return min * (1 - pct) + max * pct;
        }
    }
}
