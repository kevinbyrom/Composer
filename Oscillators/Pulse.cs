using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class PulseOscillator : OscillatorBase
    {
        public double DutyCycle;

        public PulseOscillator(double freq) : base(freq)
        {
        }

        public PulseOscillator(Func<double> freq) : base(freq)
        {
        }


        public override Signal GetValue(double time)
        {
            double period = 1.0 / this.Frequency();
            double timeModulusPeriod = time - Math.Floor(time / period) * period;
            double phase = timeModulusPeriod / period;

            if (phase <= DutyCycle)
                return Signal.Max;
            else
                return Signal.Min; 
        }
    }
}
