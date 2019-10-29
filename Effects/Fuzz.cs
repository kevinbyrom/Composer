using System;

namespace Composer.Effects
{
    public class FuzzEffect : ISampleTransform
    {
        public double MaxFuzz { get; private set; }
        Random rnd = new Random();

        public FuzzEffect(double maxFuzz)
        {
            this.MaxFuzz = maxFuzz;
        }

        public Sample Transform(SampleTime time, Sample sample)
        {
            double fuzz = rnd.NextDouble() * this.MaxFuzz;

            sample.Left += fuzz;
            sample.Right += fuzz;

            return sample;
        }
    }
}