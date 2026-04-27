using System;
using System.Linq;


namespace Composer.Waves
{
    public class CompositeWave : WaveBase
    {
        public IWave[] Waves;

        public CompositeWave(double freq) : base(freq)
        {
        }

        public CompositeWave(Func<double> freq) : base(freq)
        {
        }


        public override Signal GetValue(double time)
        {
            throw new NotImplementedException();
            //return Signals.Select(s => s.GetValue(time, freq)).Sum();
        }
    }
}
