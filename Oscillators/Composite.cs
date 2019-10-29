using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class CompositeOscillator : OscillatorBase
    {
        public OscillatorBase[] Oscillators;

        public CompositeOscillator(double freq) : base(freq)
        {
        }

        public override Sample GetValue(double time)
        {
            throw new NotImplementedException();
            //return Signals.Select(s => s.GetValue(time, freq)).Sum();
        }
    }
}
