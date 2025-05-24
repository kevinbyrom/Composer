using System;
using System.Linq;


namespace Composer.Waves
{
    public class EmptyWave : WaveBase
    {
        public EmptyWave(double freq) : base(freq)
        { 
        }

        public EmptyWave(Func<double> freq) : base(freq)
        {
        }

        public override Signal GetValue(double time)
        {
            return Signal.None;
        }
    }
}
