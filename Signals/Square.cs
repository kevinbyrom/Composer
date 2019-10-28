using System;
using System.Linq;


namespace Composer.Signals
{

    public class SquareWaveSignal : ISignalSource
    {
        public double GetValue(double time, double freq)
        {
            return Math.Sin(freq * time * 2 * Math.PI) >= 0 ? 1 : -1;
        }
    }
}
