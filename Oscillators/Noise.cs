
using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class NoiseOscillator: OscillatorBase
    {
        public Random Rng = new Random();

        public NoiseOscillator(double freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            var rndVal = Rng.NextDouble() - Rng.NextDouble();

            return new Signal(rndVal);
        }
    }
}
