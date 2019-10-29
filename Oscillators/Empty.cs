using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class EmptyOscillator : OscillatorBase
    {
        public EmptyOscillator(double freq) : base(freq)
        { 
        }

        public override Sample GetValue(double time)
        {
            return Sample.Zero;
        }
    }
}
