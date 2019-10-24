using System;

namespace Composer.Effects
{
    public class LowPassFilterEffect : IEffect
    {
        float min;

        public LowPassFilterEffect(float min)
        {
            this.min = min;
        }

        public Sample Apply(Sample sample)
        {
            sample.Left = Math.Max(sample.Left, min);
            sample.Right = Math.Max(sample.Right, min);

            return sample;
        }
    }
}