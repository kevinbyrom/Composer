using System;
using System.Linq;


namespace Composer.Signals
{
    public class NoiseSignal : ISignalSource
    {
        public Random Rng = new Random();

        public double GetValue(double time, double freq)
        {
            return (Rng.NextDouble() - Rng.NextDouble());
        }
    }
}
