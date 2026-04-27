using System;
using System.Linq;


namespace Composer.Waves
{
    public class PulseWave : WaveBase
    {
        public double DutyCycle;

        public PulseWave(double freq) : base(freq)
        {
        }

        public PulseWave(Func<double> freq) : base(freq)
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
