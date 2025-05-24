
using System;
using System.Linq;


namespace Composer.Waves
{
    public class NoiseWave: WaveBase
    {
        public Random Rng = new Random();

        public NoiseWave(double freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            var rndVal = Rng.NextDouble() - Rng.NextDouble();

            return new Signal(rndVal);
        }
    }
}
