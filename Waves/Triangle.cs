using System;
using System.Linq;


namespace Composer.Waves
{
    public class TriangleWave : WaveBase
    {
        public TriangleWave(double freq) : base(freq)
        {
        }

        public TriangleWave(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            return new Signal(Math.Abs(2 * (time * this.Frequency() - Math.Floor(time * this.Frequency() + 0.5))) * 2 - 1);
        }
    }
}
