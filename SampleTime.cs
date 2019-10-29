using System;
using System.Collections.Generic;
using System.Text;

namespace Composer
{
    public struct SampleTime
    {
        public double Current;
        public double Elapsed;
        public int Rate;

        public SampleTime(int rate)
        {
            this.Current = 0;
            this.Elapsed = 0;
            this.Rate = rate;
        }
    }
}
