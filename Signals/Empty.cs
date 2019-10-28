using System;
using System.Linq;


namespace Composer.Signals
{
    public class EmptySignal : ISignalSource
    {
        public double GetValue(double time, double freq)
        {
            return 0;
        }
    }
}
