using System;
using System.Collections.Generic;
using System.Linq;


namespace Composer.Output
{
    /*public struct MixerInput
    {
        public ISampleSource Source;
        public float LeftBalance;
        public float RightBalance;
    }*/


    public class MixedOutput : ISampleTarget
    {
        private ISampleTarget target;
        private IEnumerable<ISampleSource> sources;

        public MixedOutput(ISampleTarget target)
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

            
            // Add all the samples

            float left = 0; 
            float right = 0;
        
            foreach (var sample in samples)
            {
                left += sample.Left;
                right += sample.Right;
            }


            // Take the average of all the samples

            Sample mixed = new Sample();

            mixed.Left = left / (float)samples.Count();
            mixed.Right = right / (float)samples.Count();

            //Console.WriteLine($"MixedSample Left = {mixed.Left}, Right = {mixed.Right}");

            this.target.Write(mixed);
        }
    }
}
