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

        public override Sample GetValue(double time)
        {
            double period = 1.0 / this.Frequency;
            double timeModulusPeriod = time - Math.Floor(time / period) * period;
            double phase = timeModulusPeriod / period;

            if (phase <= DutyCycle)
                return new Sample(1);
            else
                return new Sample(-1);
        }
    }
}
