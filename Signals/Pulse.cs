using System;
using System.Linq;


namespace Composer.Signals
{
    public class PulseSignal : ISignalSource
    {
        public double DutyCycle;

        public double GetValue(double time, double freq)
        {
            double period = 1.0 / freq;
            double timeModulusPeriod = time - Math.Floor(time / period) * period;
            double phase = timeModulusPeriod / period;

            if (phase <= DutyCycle)
                return 1;
            else
                return -1;
        }
    }
}
