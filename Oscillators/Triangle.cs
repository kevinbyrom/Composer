using System;
using System.Linq;


namespace Composer.Oscillators
{
    public class TriangleWaveOscillator : OscillatorBase
    {
        public TriangleWaveOscillator(double freq) : base(freq)
        {
        }

        public TriangleWaveOscillator(Func<double> freq) : base(freq)
        {
        }

        public override Sample GetValue(double time)
        {
            return new Sample(Math.Abs(2 * (time * this.Frequency() - Math.Floor(time * this.Frequency() + 0.5))) * 2 - 1);
        }
    }
}
