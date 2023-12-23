using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class SineWaveOscillator : OscillatorBase
    {
        public SineWaveOscillator(double freq) : base(freq)
        {
        }

        public SineWaveOscillator(Func<double> freq) : base(freq)
        {
        }


        public override Sample GetValue(double time)
        {
            return new Sample(Math.Sin(this.Frequency() * time * 2 * Math.PI));
        }
    }
}
