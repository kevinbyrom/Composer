using System;

namespace Composer.Effects
{
    public class LowPassFilter : ISampleTransform
    {
        public double Min { get; private set; }

        public LowPassFilter(float min)
        {
            this.Min = min;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            sample.Left = Math.Max(sample.Left, this.Min);
            sample.Right = Math.Max(sample.Right, this.Min);

            return sample;
        }
    }
}