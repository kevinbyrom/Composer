using System;

namespace Composer.Effects
{
    public class FuzzEffect : EffectBase
    {
        public Func<double> Max { get; set; }
        Random rnd = new Random();

        public FuzzEffect(double maxFuzz)
        {
            this.Max = () => { return maxFuzz; };
        }

        public FuzzEffect(Func<double> maxFuzz)
        {
            this.Max = maxFuzz;
        }

        public Sample GetValue(SampleTime time)
        {
            double fuzz = rnd.NextDouble() * this.Max();

            var sample = InputSource.GetValue(time);
            sample.Left += fuzz;
            sample.Right += fuzz;

            return sample;
        }

        public void Release()
        {
        }
    }
}