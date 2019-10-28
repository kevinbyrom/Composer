using System;
using System.Linq;


namespace Composer.Signals
{
    public class TriangleWaveSignal : ISignalSource
    {
        public double GetValue(double time, double freq)
        {
            return Math.Abs(2 * (time * freq - Math.Floor(time * freq + 0.5))) * 2 - 1;
        }
    }
}
