using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class SawtoothWaveOscillator : OscillatorBase
    {
        public SawtoothWaveOscillator(double freq) : base(freq)
        {
        }

        public SawtoothWaveOscillator(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            double val = 2 * (time * this.Frequency() - Math.Floor(time * this.Frequency() + 0.5));

            return new Signal(val);
        }
    }
}
