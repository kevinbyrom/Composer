using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class SawtoothWaveOscillator : OscillatorBase
    {
        public SawtoothWaveOscillator(double freq) : base(freq)
        {
        }

        public override Sample GetValue(double time)
        {
            return new Sample(2 * (time * this.Frequency - Math.Floor(time * this.Frequency + 0.5)));
        }
    }
}
