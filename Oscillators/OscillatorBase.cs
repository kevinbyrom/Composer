using System;
using System.Collections.Generic;
using System.Text;

namespace Composer.Oscillators
{
    public abstract class OscillatorBase : ISampleSource
    {
        public double Frequency { get; protected set; }

        
        public OscillatorBase(double freq)
        {
            this.Frequency = freq;
        }

        public abstract Sample GetValue(double time);
    }
}
