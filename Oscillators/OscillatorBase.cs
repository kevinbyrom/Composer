using System;
using System.Collections.Generic;
using System.Text;

namespace Composer.Oscillators
{
    public abstract class OscillatorBase : IOscillator
    {
        public Func<double> Frequency { get; set; }

        
        public OscillatorBase(double freqVal)
        {
            this.Frequency = () => { return freqVal; };
        }
        public OscillatorBase(Func<double> freq)
        {
            this.Frequency = freq;
        }

        public abstract Sample GetValue(double time);
    }
}
