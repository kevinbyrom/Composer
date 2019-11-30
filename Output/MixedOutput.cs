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
        
        public MixedOutput(ISampleTarget target)
        {
            this.target = target;
        }

        public void Write(SampleTime time, Sample sample)
        {
            this.target.Write(time, sample);
        }

        public void Write(SampleTime time, IEnumerable<Sample> samples)
        {
            if (samples.Count() == 0)
            {
                this.target.Write(time, Sample.Zero);
                return;        
            }

            
            // Combine the samples and take an average
            // This code will likely need to be changed as 
            // I am hearing an audible shift when dead voices drop off

            double left = 0;
            double right = 0;
            int numSamples = 0;

            foreach (var sample in samples)
            {
                left += sample.Left;
                right += sample.Right;

                if (sample.Left != 0 && sample.Right != 0)
                    numSamples++;
            }

            Sample mixed = new Sample();

            mixed.Left = left / (float)numSamples;
            mixed.Right = right / (float)numSamples;


            //Console.WriteLine($"MixedSample Left = {mixed.Left}, Right = {mixed.Right}");

            this.target.Write(time, mixed);
        }
    }
}
