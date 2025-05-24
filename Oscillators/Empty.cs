using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class EmptyOscillator : OscillatorBase
    {
        public EmptyOscillator(double freq) : base(freq)
        { 
        }

        public EmptyOscillator(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            return Signal.None;
        }
    }
}
