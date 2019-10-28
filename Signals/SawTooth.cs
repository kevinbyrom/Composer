using System;
using System.Linq;


namespace Composer.Signals
{
    public class SawtoothWaveSignal : ISignalSource
    {
        public double GetValue(double time, double freq)
        {
            return 2 * (time * freq - Math.Floor(time * freq + 0.5));
        }
    }
}
