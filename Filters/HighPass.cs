using System;

namespace Composer.Effects
{
    public class HighPassFilter : ISampleTransform
    {
        float max;

        public HighPassFilter(float max)
        {
            this.max = max;
        }

        public Sample Transform(Sample sample)
        {
            sample.Left = Math.Min(sample.Left, max);
            sample.Right = Math.Min(sample.Right, max);

            return sample;
        }
    }
}