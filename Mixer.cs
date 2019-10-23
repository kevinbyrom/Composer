using System.Collections.Generic;
using System.Linq;


namespace Composer
{
    /*public struct MixerInput
    {
        public ISampleSource Source;
        public float LeftBalance;
        public float RightBalance;
    }*/


    public class Mixer : ISampleTarget
    {
        private ISampleTarget target;
        private IEnumerable<ISampleSource> sources;

        public Mixer(ISampleTarget target)
        {
            this.target = target;
        }

        public void Write(Sample sample)
        {
            this.target.Write(sample);
        }

        public void Write(IEnumerable<Sample> samples)
        {
            if (samples.Count() == 0)
            {
                this.target.Write(Sample.Zero);
                return;        
            }

            Sample mixed = new Sample();

            foreach (var sample in samples)
            {
                mixed += sample;
            }

            this.target.Write(mixed);
        }
    }
}
