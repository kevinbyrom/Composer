using System;

namespace Composer.Effects
{
    public class FuzzEffect : IEffect
    {
        float maxFuzz;
        Random rnd = new Random();

        public FuzzEffect(float maxFuzz)
        {
            this.maxFuzz = maxFuzz;
        }

        public Sample Apply(Sample sample)
        {
            float fuzz = (float)rnd.NextDouble() * maxFuzz;

            sample.Left += fuzz;
            sample.Right += fuzz;

            return sample;
        }
    }
}