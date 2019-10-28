using System;

namespace Composer.Effects
{
    public class LowPassFilter : ISampleTransform
    {
        float min;

        public LowPassFilter(float min)
        {
            this.min = min;
        }

        public Sample Transform(Sample sample)
        {
            sample.Left = Math.Max(sample.Left, min);
            sample.Right = Math.Max(sample.Right, min);

            return sample;
        }
    }
}