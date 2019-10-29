using System;

namespace Composer.Effects
{
    public class HighPassFilter : ISampleTransform
    {
        public double Max { get; private set; }

        public HighPassFilter(float max)
        {
            this.Max = max;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            sample.Left = Math.Min(sample.Left, this.Max);
            sample.Right = Math.Min(sample.Right, this.Max);

            return sample;
        }
    }
}