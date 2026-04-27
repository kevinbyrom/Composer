using System;
using System.Collections.Generic;
using System.Text;

namespace Composer.Waves
{
    public abstract class WaveBase : IWave
    {
        public Func<double> Frequency { get; set; }

        
        public WaveBase(double freqVal)
        {
            this.Frequency = () => { return freqVal; };
        }
        public WaveBase(Func<double> freq)
        {
            this.Frequency = freq;
        }

        public abstract Signal GetValue(double time);
    }
}
