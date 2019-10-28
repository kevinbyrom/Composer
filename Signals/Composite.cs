using System;
using System.Linq;


namespace Composer.Signals
{
    public class CompositeSignal : ISignalSource
    {
        public ISignalSource[] Signals;

        public double GetValue(double time, double freq)
        {
            return Signals.Select(s => s.GetValue(time, freq)).Sum();
        }
    }
}
