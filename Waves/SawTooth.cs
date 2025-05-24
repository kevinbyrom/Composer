using System;
using System.Linq;


namespace Composer.Waves
{
    public class SawtoothWave : WaveBase
    {
        public SawtoothWave(double freq) : base(freq)
        {
        }

        public SawtoothWave(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            double val = 2 * (time * this.Frequency() - Math.Floor(time * this.Frequency() + 0.5));

            return new Signal(val);
        }
    }
}
