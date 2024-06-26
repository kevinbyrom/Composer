using System;
using System.Linq;


namespace Composer.Oscillators
{

    public class SquareWaveOscillator : OscillatorBase
    {
        public SquareWaveOscillator(double freq) : base(freq)
        {
        }

        public SquareWaveOscillator(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            return new Signal(Math.Sin(this.Frequency() * time * 2 * Math.PI) >= 0 ? 1 : -1);
        }
    }
}
