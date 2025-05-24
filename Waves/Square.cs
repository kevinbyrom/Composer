using System;
using System.Linq;


namespace Composer.Waves
{

    public class SquareWave : WaveBase
    {
        public SquareWave(double freq) : base(freq)
        {
        }

        public SquareWave(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            return new Signal(Math.Sin(this.Frequency() * time * 2 * Math.PI) >= 0 ? 1 : -1);
        }
    }
}
