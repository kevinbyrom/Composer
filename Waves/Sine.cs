using System;
using System.Linq;


namespace Composer.Waves
{
    public class SineWave : WaveBase
    {
        public SineWave(double freq) : base(freq)
        {
        }

        public SineWave(Func<double> freq) : base(freq)
        {
        }


        public override Signal GetValue(double time)
        {
            return new Signal(Math.Sin(this.Frequency() * time * 2 * Math.PI));
        }
    }
}
