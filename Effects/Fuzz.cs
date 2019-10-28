using System;

namespace Composer.Effects
{
    public class FuzzEffect : ISampleTransform
    {
        float maxFuzz;
        Random rnd = new Random();

        public FuzzEffect(float maxFuzz)
        {
            this.maxFuzz = maxFuzz;
        }

        public Sample Transform(Sample sample)
        {
            float fuzz = (float)rnd.NextDouble() * maxFuzz;

            sample.Left += fuzz;
            sample.Right += fuzz;

            return sample;
        }
    }
}